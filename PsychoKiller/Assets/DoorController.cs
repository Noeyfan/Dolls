using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	public enum DoorType {RIGHT, LEFT};
	public enum DoorFace {NORMAL, SIDE};
	public enum DoorState {IDLE, INSIDE, OUTSIDE};

	public DoorType doorType;
	public DoorFace doorFace;
	protected DoorState doorState = DoorState.IDLE;

	// Use this for initialization
	void Start () {
		// set first rotation
		switch (doorFace) {
		case DoorFace.NORMAL :
			transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, 0f, transform.localRotation.eulerAngles.z);
			break;
		case DoorFace.SIDE :
			transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, 90f, transform.localRotation.eulerAngles.z);
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other) {
		Debug.Log (other.gameObject.tag);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Hand") {

		}
	}
}
