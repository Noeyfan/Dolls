using UnityEngine;
using System.Collections;

public class NoteController : MonoBehaviour {

	public enum AnimationType {NULL, SHOWUP, GOBACK};

	private AnimationType animationType = AnimationType.NULL;
	private Vector3 initPosition, firstPosition, targetPosition;
	private Quaternion initRotation, firstRotation, targetRotation;
	private GameObject oculusCameraRight, oculusCameraLeft;
	private Shader initShader, targetShader;
	private GameObject gameController;

	// animation attribute
	private bool isAnimating = false;
	private float animationTime = 0.5f;
	private float elapsedTimeAnimation = 0f;

	// Use this for initialization
	void Start () {
		initRotation = transform.rotation;
		initPosition = transform.position;

		initShader = renderer.material.shader;
		targetShader = Shader.Find ("Unlit/Transparent");

		oculusCameraRight = GameObject.Find ("CameraRight");
		oculusCameraLeft = GameObject.Find ("CameraLeft");
		gameController = GameObject.FindGameObjectWithTag ("GameController");
	}
	
	// Update is called once per frame
	void Update () {
		if (isAnimating) {
			elapsedTimeAnimation += Time.deltaTime;

			switch (animationType) {
			case AnimationType.SHOWUP :
				this.transform.localPosition = Vector3.Slerp(firstPosition, targetPosition, elapsedTimeAnimation / animationTime);
				this.transform.localRotation = Quaternion.Slerp(firstRotation, targetRotation, elapsedTimeAnimation / animationTime);
				break;
			case AnimationType.GOBACK : 
				this.transform.position = Vector3.Slerp(firstPosition, targetPosition, elapsedTimeAnimation / animationTime);
				this.transform.rotation = Quaternion.Slerp(firstRotation, targetRotation, elapsedTimeAnimation / animationTime);
				break;
			default:
				Debug.LogError("animationType undefined");
				break;
			}

			if (elapsedTimeAnimation >= animationTime) {
				// set as currentNote
				if (animationType == AnimationType.SHOWUP) {
					gameController.SendMessage("SetCurrentNote", this.gameObject);
				}

				// init
				elapsedTimeAnimation = 0f;				
				isAnimating = false;				
				animationType = AnimationType.NULL;
			}
		}

		// debug note
		//if (Input.GetKeyDown (KeyCode.Q)) ShowUp ();		
		//else if (Input.GetKeyDown (KeyCode.A)) GoBack ();
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
			
			// set target
			targetRotation = Quaternion.Euler (Vector3.zero);
			targetPosition = new Vector3 (0f, 0f, 0.2f);

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

			// set target
			targetRotation = initRotation;
			targetPosition = initPosition;

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
