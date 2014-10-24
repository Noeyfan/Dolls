using UnityEngine;
using System.Collections;

public class PianoKeys : MonoBehaviour {

	public enum DirectionType {IDLE, PRESS, RELEASE};
	public bool isBlack = false;

	private DirectionType directionType = DirectionType.IDLE;

	private GameObject hitObject;
	private bool isHitting = false;

	private float maxRotation;

	private bool isReleased = false;
	private float isAnimating = false;
	private float elapsedTimeAnimation = false;
	private float timeAnimation = 0.3f;
	private Quaternion firstRotation, targetRotation;

	// Use this for initialization
	void Start () {
		maxRotation = isBlack ? 8 : 4;
	}
	
	// Update is called once per frame
	void Update () {
		if (isAnimating) {
			elapsedTimeAnimation += Time.deltaTime;

			transform.localRotation = Quaternion.Slerp(firstRotation, targetRotation, elapsedTimeAnimation / timeAnimation);

			// onComplete
			if (elapsedTimeAnimation >= timeAnimation) {
				if (directionType == DirectionType.PRESS) {
					if (isReleased) {
						Release();
					}
				} else if (directionType == DirectionType.RELEASE) {
					isHitting = false;
				}

				// init
				directionType = DirectionType.IDLE;
				isAnimating = false;
				elapsedTimeAnimation = 0f;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (!isHitting && other.gameObject.tag == "Hand") {
			hitObject = other.gameObject;
			audio.Play ();

			Hit();
		}
	}

	void OnTriggerExit(Collider other) {
		if (isHitting && other.gameObject.tag == "Hand" && hitObject.Equals (other.gameObject)) {
			if (isAnimating) {
				isReleased = true;
			} else {
				Release();
			}
		}
	}

	void Hit() {
		if (!isAnimating && directionType == directionType.IDLE) {
			isAnimating = true;
			firstRotation = transform.localRotation;
			targetRotation = Quaternion.Euler (transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z + maxRotation);
			directionType = DirectionType.PRESS;

			isHitting = true;
			isReleased = false;
		}
	}

	void Release() {
		if (!isAnimating && directionType == DirectionType.IDLE) {
			isAnimating = true;
			firstRotation = transform.localScale;
			targetRotation = Quaternion.Euler (transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z - maxRotation);
			directionType = DirectionType.RELEASE;
		}
	}
}
