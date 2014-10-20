using UnityEngine;
using System.Collections;

public class NoteController : MonoBehaviour {

	public enum AnimationType {NULL, SHOWUP, GOBACK};

	private AnimationType animationType = AnimationType.NULL;
	private Vector3 initPosition, firstPosition, targetPosition;
	private Quaternion initRotation, firstRotation, targetRotation;
	private Vector3 initScale, firstScale, targetScale, fullscreenScale;
	private GameObject oculusCameraRight, oculusCameraLeft;
	private Shader initShader, targetShader;
	private GameObject gameController;

	// animation attribute
	private bool isAnimating = false;
	private float animationTime = 0.5f;
	private float elapsedTimeAnimation = 0f;

	// sound attribute
	private float elapsedTimeSFX = 0f;
	private bool isWaitingSFX = false;
	private float durationSFX = 0f;

	public Texture texture;
	public AudioClip SFX;
	private AudioClip paperPickupSFX;

	// Use this for initialization
	void Start () {
		// material
		renderer.material = new Material (Shader.Find("Transparent/Diffuse"));
		renderer.material.mainTexture = texture;

		initShader = renderer.material.shader;
		targetShader = Shader.Find ("Unlit/Transparent");

		paperPickupSFX = audio.clip;

		initRotation = transform.rotation;
		initPosition = transform.position;
		initScale = transform.localScale;

		float maxScaling = 0.4f;
		float multiplier = maxScaling / ((initScale.x > initScale.y ? initScale.x : initScale.y));
		fullscreenScale = new Vector3 (initScale.x * multiplier, initScale.y * multiplier, initScale.z);

		oculusCameraRight = GameObject.Find ("CameraRight");
		oculusCameraLeft = GameObject.Find ("CameraLeft");
		gameController = GameObject.FindGameObjectWithTag ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		if (isAnimating) {
			elapsedTimeAnimation += Time.deltaTime;

			// animation
			this.transform.localPosition = Vector3.Slerp(firstPosition, targetPosition, elapsedTimeAnimation / animationTime);
			this.transform.localRotation = Quaternion.Slerp(firstRotation, targetRotation, elapsedTimeAnimation / animationTime);
			this.transform.localScale = Vector3.Slerp(firstScale, targetScale, elapsedTimeAnimation / animationTime);

			if (elapsedTimeAnimation >= animationTime) {
				// set as currentNote
				if (animationType == AnimationType.SHOWUP) {
					gameController.SendMessage("SetCurrentNote", this.gameObject);
				}

				// play SFX
				if (animationType == AnimationType.SHOWUP) PlaySFX();

				// init
				elapsedTimeAnimation = 0f;				
				isAnimating = false;				
				animationType = AnimationType.NULL;
			}
		}

		if (isWaitingSFX) {
			elapsedTimeSFX += Time.deltaTime;
			if (elapsedTimeSFX >= durationSFX) {
				// init
				isWaitingSFX = false;
				elapsedTimeSFX = 0f;
				audio.clip = paperPickupSFX;
				// enable makey makey
				GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().isMakeyMakeyActive = true;
			}
		}
	}

	protected void PlaySFX() {
		if (SFX != null) {
			durationSFX = SFX.length;
			isWaitingSFX = true;

			audio.clip = SFX;
			audio.Play();

			// disable makey makey
			GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().isMakeyMakeyActive = false;
		}
	}

	protected void ShowUp() {
		if (!isAnimating && animationType != AnimationType.SHOWUP) {
			isAnimating = true;
			animationType = AnimationType.SHOWUP;

			// change parent
			transform.parent = oculusCameraRight.transform;

			// change shader
			renderer.material.shader = targetShader;

			// set first
			firstPosition = transform.localPosition;
			firstRotation = transform.localRotation;
			firstScale = transform.localScale;

			// set target
			targetRotation = Quaternion.Euler (Vector3.zero);
			targetPosition = new Vector3 (0f, 0f, 0.2f);
			targetScale = fullscreenScale;

			// sound
			audio.Play();
		}
	}

	protected void GoBack() {
		if (!isAnimating && animationType != AnimationType.GOBACK) {
			isAnimating = true;
			animationType = AnimationType.GOBACK;

			// change parent
			transform.parent = null;

			// change shader
			renderer.material.shader = initShader;

			// set first
			firstPosition = transform.position;
			firstRotation = transform.rotation;
			firstScale = transform.localScale;

			// set target
			targetRotation = initRotation;
			targetPosition = initPosition;
			targetScale = initScale;

			gameController.SendMessage("SetCurrentNoteNull");

			// sound
			audio.Play();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Hand") {
			ShowUp();
		}
	}
}
