using UnityEngine;
using System.Collections;

public class PhoneController : MonoBehaviour
{
	private Vector3 initLocalPosition, hidePosition;

	// animation attribute
	private bool isAnimating = false;
	private float elapsedTime = 0f;
	private float totalTimeAnimation = 0.3f;
	private Vector3 firstPosition, targetPosition;

	// Use this for initialization
	void Start ()
	{
		initLocalPosition = transform.localPosition;

		// hide from camera
		hidePosition = new Vector3 (transform.localPosition.x, transform.localPosition.y - 0.4f, transform.localPosition.z);
		transform.localPosition = hidePosition;
	}

	// Update is called once per frame
	void Update ()
	{
		if (isAnimating) {
			elapsedTime += Time.deltaTime;

			transform.localPosition = Vector3.Slerp(firstPosition, targetPosition, elapsedTime / totalTimeAnimation);

			if (elapsedTime >= totalTimeAnimation) {
				// init
				isAnimating = false;
				elapsedTime = 0f;
			}
		}
	}

	void Show() {
		if (!isAnimating) {
			isAnimating = true;

			firstPosition = transform.localPosition;
			targetPosition = initLocalPosition;

			// sound
			audio.Play();
		}
	}

	void Hide() {
		if (!isAnimating) {
			isAnimating = true;

			firstPosition = transform.localPosition;
			targetPosition = hidePosition;
		}
	}
}
