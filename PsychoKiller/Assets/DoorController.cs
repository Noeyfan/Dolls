using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {
	bool isOpen;
	Animator a;
	// Use this for initialization
	void Start () {
		a = gameObject.GetComponent<Animator>();
		if(gameObject.name.Contains("Right")) {
			a.SetBool("isRight", true);
		}else if(gameObject.name.Contains("Left")) {
			a.SetBool("isRight", false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision c) {
		Collider collider = c.collider;
		Vector3 contactPoint = c.contacts[0].point;
		Vector3 center = collider.bounds.center;

		if(center.z > contactPoint.z) {
			// is outside
			a.SetBool("isInside", false);
		}else if(center.z < contactPoint.z) {
			// is inside
			a.SetBool("isInside", true);
		}

		if(c.gameObject.tag == "Player") {
			if(!isOpen) {
				print("hit");
				//a.SetBool("isInside", true);
				a.SetBool("isOpen", true);
				isOpen = true;
			}else {
				//a.SetBool("isInside", true);
				//a.SetBool("isRight", false);
				a.SetBool("isOpen", false);
				isOpen = false;
			}
		}
	}
}
