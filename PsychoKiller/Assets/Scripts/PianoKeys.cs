using UnityEngine;
using System.Collections;

public class PianoKeys : MonoBehaviour {

	private GameObject hitObject;
	private bool isHitting = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (!isHitting && other.gameObject.tag == "Hand") {
			isHitting = true;
			hitObject = other.gameObject;
			audio.Play ();
		}
	}

	void OnTriggerExit(Collider other) {
		if (isHitting && other.gameObject.tag == "Hand" && hitObject.Equals (other.gameObject)) {
			isHitting = false;
		}
	}
}
