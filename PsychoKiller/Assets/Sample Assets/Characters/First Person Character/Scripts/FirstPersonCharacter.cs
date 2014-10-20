using UnityEngine;
using System.Collections;

public class FirstPersonCharacter : MonoBehaviour
{
	[SerializeField] private float runSpeed = 8f;                                       // The speed at which we want the character to move
	[SerializeField] private float strafeSpeed = 4f;                                    // The speed at which we want the character to be able to strafe
	[SerializeField] private float jumpPower = 5f;                                      // The power behind the characters jump. increase for higher jumps
	#if !MOBILE_INPUT
	[SerializeField] private bool walkByDefault = true;									// controls how the walk/run modifier key behaves.
	[SerializeField] private float walkSpeed = 3f;                                      // The speed at which we want the character to move
	#endif
	[SerializeField] private AdvancedSettings advanced = new AdvancedSettings();        // The container for the advanced settings ( done this way so that the advanced setting are exposed under a foldout
	[SerializeField] private bool lockCursor = true;
	[SerializeField] public bool hasControl = true;
    [SerializeField] AudioClip[] footstepSounds;
	
	Vector3 vec;
	GameObject camera;
	int rotateBody = 0;
	GameObject exit;
	GameObject blood;
	OculusController oc;
	GameObject sc;
	private bool dead;
	
	
	[System.Serializable]
	public class AdvancedSettings                                                       // The advanced settings
	{
		public float gravityMultiplier = 1f;                                            // Changes the way gravity effect the player ( realistic gravity can look bad for jumping in game )
		public PhysicMaterial zeroFrictionMaterial;                                     // Material used for zero friction simulation
		public PhysicMaterial highFrictionMaterial;                                     // Material used for high friction ( can stop character sliding down slopes )
		public float groundStickyEffect = 5f;											// power of 'stick to ground' effect - prevents bumping down slopes.
	}
	
	private CapsuleCollider capsule;                                                    // The capsule collider for the first person character
	private const float jumpRayLength = 0.7f;                                           // The length of the ray used for testing against the ground when jumping
	public bool grounded { get; private set; }
	private Vector2 input;
	private IComparer rayHitComparer;
    public bool useMakeyMakey = true;

    private float velocity = 0;
    private float damping = 0.97f;
    private float acc = 0;
    private int no_acc_count = 0;

	private GameObject gameController;

	void Awake ()
	{
		// Set up a reference to the capsule collider.
		capsule = collider as CapsuleCollider;
		grounded = true;
		Screen.lockCursor = lockCursor;
		rayHitComparer = new RayHitComparer();
		camera = GameObject.FindWithTag("MainCamera");
		exit = GameObject.Find("RunToPoint");
		blood = GameObject.Find("Blood");

	}
	
	void Start() {
		gameController = GameObject.FindGameObjectWithTag("GameController");
		oc = gameObject.GetComponent<OculusController>();
		sc = GameObject.Find("SoundSets");
		blood.SetActive(false);
		dead = false;
		//if(triggerRunAnim) {
		//disable camera
		//StartCoroutine(PlayRunAnim());
		//}
	}
	
	void OnDisable()
	{
		Screen.lockCursor = false;
	}
	
	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Screen.lockCursor = lockCursor;
		}
	}
	
	
	public void FixedUpdate ()
	{
		float speed = runSpeed;
		
		// Read input
		#if CROSS_PLATFORM_INPUT
		float h = CrossPlatformInput.GetAxis("Horizontal");
		float v = CrossPlatformInput.GetAxis("Vertical");
		bool jump = CrossPlatformInput.GetButton("Jump");
		#else
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		bool jump = Input.GetButton("Jump");
		#endif
		
		#if !MOBILE_INPUT
		
		// On standalone builds, walk/run speed is modified by a key press.
		// We select appropriate speed based on whether we're walking by default, and whether the walk/run toggle button is pressed:
		bool walkOrRun =  Input.GetKey(KeyCode.LeftShift);
		speed = walkByDefault ? (walkOrRun ? runSpeed : walkSpeed) : (walkOrRun ? walkSpeed : runSpeed);
		
		// On mobile, it's controlled in analogue fashion by the v input value, and therefore needs no special handling.
		
		
		#endif
		if(hasControl) {

            if (useMakeyMakey && (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) && gameController.GetComponent<GameController>().isMakeyMakeyActive)
            {
                acc = 50f;
                velocity += acc * Time.fixedDeltaTime;
                velocity = Mathf.Min(300f, velocity);
                if (velocity < 0.001f) velocity = 0;
                int n = Random.Range(1,footstepSounds.Length);

                audio.PlayOneShot(footstepSounds[n-1]);        
				gameController.SendMessage("RemoveCurrentNote");
            }

            velocity *= damping;
            
            
            input = new Vector2( h, v );
			
			// normalize input if it exceeds 1 in combined length:
			if (input.sqrMagnitude > 1) input.Normalize();

            speed = velocity;
			// Get a vector which is desired move as a world-relative direction, including speeds
			Vector3 desiredMove = useMakeyMakey? transform.forward * speed :
                                    transform.forward * input.y * speed + transform.right * input.x * strafeSpeed;
			
			// preserving current y velocity (for falling, gravity)
			float yv = rigidbody.velocity.y;
			
			// add jump power
			if (grounded && jump) {
				yv += jumpPower;
				grounded = false;
			}
			
			// Set the rigidbody's velocity according to the ground angle and desired move
			rigidbody.velocity = desiredMove + Vector3.up * yv;
			
			// Use low/high friction depending on whether we're moving or not
			if (desiredMove.magnitude > 0 || !grounded)
			{
				collider.material = advanced.zeroFrictionMaterial;
			} else {
				collider.material = advanced.highFrictionMaterial;
			}
			
			
			// Ground Check:
			
			// Create a ray that points down from the centre of the character.
			Ray ray = new Ray(transform.position, -transform.up);
			
			// Raycast slightly further than the capsule (as determined by jumpRayLength)
			RaycastHit[] hits = Physics.RaycastAll(ray, capsule.height * jumpRayLength );
			System.Array.Sort (hits, rayHitComparer);
			
			
			if (grounded || rigidbody.velocity.y < jumpPower * .5f)
			{
				// Default value if nothing is detected:
				grounded = false;
				// Check every collider hit by the ray
				for (int i = 0; i < hits.Length; i++)
				{
					// Check it's not a trigger
					if (!hits[i].collider.isTrigger)
					{
						// The character is grounded, and we store the ground angle (calculated from the normal)
						grounded = true;
						
						// stick to surface - helps character stick to ground - specially when running down slopes
						//if (rigidbody.velocity.y <= 0) {
						rigidbody.position = Vector3.MoveTowards (rigidbody.position, hits[i].point + Vector3.up * capsule.height*.5f, Time.deltaTime * advanced.groundStickyEffect);
						//}
						rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
						break;
					}
				}
			}
			
			Debug.DrawRay(ray.origin, ray.direction * capsule.height * jumpRayLength, grounded ? Color.green : Color.red );
			
			
			// add extra gravity
			rigidbody.AddForce(Physics.gravity * (advanced.gravityMultiplier - 1));
		}else {
			//Fall down
			var lookPos = exit.transform.position - transform.position;
			lookPos.x = 0;
			var rotation = Quaternion.LookRotation(lookPos);
			rotation *= Quaternion.Euler(0, 0 , 90); // this add a 90 degrees Y rotation
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.time * 0.5f);

			/*rigidbody.velocity =  vec;

			if(Vector3.Distance(exit.transform.position , gameObject.transform.position) > 1f && (exit.transform.position - gameObject.transform.position).z < 0) {
				vec = (exit.transform.position - gameObject.transform.position);
			}else {
				vec  =  -Vector3.forward * runSpeed;
			}

			rotateBody++;
			if(rotateBody > 60 && rotateBody < 150 && Vector3.Angle(gameObject.transform.forward, exit.transform.forward) < 160) {
				gameObject.transform.Rotate(gameObject.transform.up * 10);
			}
			print(Vector3.Angle(gameObject.transform.forward, exit.transform.forward));
			if(rotateBody > 150 && Vector3.Angle(gameObject.transform.forward, exit.transform.forward) > 5) {
				gameObject.transform.Rotate(-gameObject.transform.up * 5);
			}*/
		}
	}
	
	//used for comparing distances
	class RayHitComparer: IComparer
	{
		public int Compare(object x, object y)
		{
			return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
		}	
	}
	
	IEnumerator PlayRunAnim() {
		hasControl = false;
		//oc.SetEnableOculus(false);
		oc.isUsingMouse = false;
		yield return new WaitForSeconds(3f);
		//RUN
		hasControl = true;
		//oc.SetEnableOculus(true);
		oc.isUsingMouse = true;
		camera.transform.forward = gameObject.transform.forward;
	}
	
	public void Run() {
		StartCoroutine (PlayRunAnim());
	}

	public void Kill() {
		StartCoroutine(KillerKillYou());
	}

	IEnumerator KillerKillYou () {
		if(!dead) {
			//sc.SendMessage("PlaySound", 5);
			yield return new WaitForSeconds(4f);
			//Fall down
			sc.SendMessage("PlaySound", 6);
			hasControl = false;
			oc.isUsingMouse = false;
			blood.SetActive(true);
			dead = true;
		}
	}
}
