using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	public enum DoorType {RIGHT, LEFT};
	public enum DoorFace {NORMAL, SIDE};
	public enum DoorSide {NULL, FRONT, BACK};
	public enum DoorState {IDLE, INSIDE, OUTSIDE};

	public DoorType doorType;
	public DoorFace doorFace;
	public bool isBasementDoor;
	protected DoorState doorState = DoorState.IDLE;

	protected int side;

	// animation attribute
	protected bool isAnimating = false;
	protected float animationTime = 0.4f;
	protected float elapsedTimeAnimation = 0f;
	protected Quaternion firstRotation, targetRotation;

	GameController gc;

	// Use this for initialization
	void Start () {
		// close all doors
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		CloseDoor ();

		if (GetComponent<BoxCollider> ().center.x < 0f) side = -1;		
		else side = 1;
	}
	
	// Update is called once per frame
	void Update () {
		// animation
		if (isAnimating) {
			elapsedTimeAnimation += Time.deltaTime;

			transform.localRotation = Quaternion.Slerp(firstRotation, targetRotation, elapsedTimeAnimation / animationTime);

			if (elapsedTimeAnimation >= animationTime) {
				isAnimating = false;
				elapsedTimeAnimation = 0f;
			}
		}
	}

	void OnCollisionEnter(Collision c) {
		if(c.gameObject.tag == "Hand") {
			if(!isBasementDoor || (isBasementDoor && gc.hasKey)) {

				//AnimateDoor(CheckSideCollision(contactPoint));
				AnimateDoor(CheckSideCollision(GameObject.FindGameObjectWithTag("Player").transform.position));
			}
		}
	}

	void OnTriggerEnter(Collider other) {

	}

	int ConvertDoorType() {
		return (doorType == DoorType.RIGHT ? 1 : -1);
	}

	protected void AnimateDoor(DoorSide doorSide) {
		if (!isAnimating) {
			switch(doorState) {
			case DoorState.IDLE :
				if (doorSide == DoorSide.FRONT) {
					StartAnimation(Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + (90f * ConvertDoorType()), transform.localRotation.eulerAngles.z));
					doorState = DoorState.INSIDE;
				} else if (doorSide == DoorSide.BACK) {
					StartAnimation(Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + (-90f * ConvertDoorType()), transform.localRotation.eulerAngles.z));
					doorState = DoorState.OUTSIDE;
				}
				break;
			case DoorState.INSIDE :
				if (doorSide == DoorSide.BACK) {
					StartAnimation(Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + (-90f * ConvertDoorType()), transform.localRotation.eulerAngles.z));
					doorState = DoorState.IDLE;
				}
				break;
			case DoorState.OUTSIDE :
				if (doorSide == DoorSide.FRONT) {
					StartAnimation(Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + (90 * ConvertDoorType()), transform.localRotation.eulerAngles.z));
					doorState = DoorState.IDLE;
				}
				break;
			}
		}
	}

	protected void StartAnimation(Quaternion targetRotation) {
		this.firstRotation = transform.localRotation;
		this.targetRotation = targetRotation;
		isAnimating = true;

		// sound
		GameObject.Find("SoundSets").SendMessage("PlaySound", 3);
	}

	protected void CloseDoor() {
		switch (doorFace) {
		case DoorFace.NORMAL :
			transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, 0f, transform.localRotation.eulerAngles.z);
			break;
		case DoorFace.SIDE :
			transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, 90f, transform.localRotation.eulerAngles.z);
			break;
		}
	}

	protected DoorSide CheckSideCollision(Vector3 position) {
		Vector3 relativePosition = transform.InverseTransformPoint (position);

		if (doorType == DoorType.RIGHT) {
			if (relativePosition.y >= 0) {
				if (side == 1) {
					return DoorSide.BACK;
				} else if (side == -1) {
					return DoorSide.FRONT;
				}
			} else { // relativePosition.y < 0
				if (side == 1) {
					return DoorSide.FRONT;
				} else if (side == -1) {
					return DoorSide.BACK;
				}
			}	
		} else if (doorType == DoorType.LEFT) {
			if (relativePosition.y >= 0) {
				if (side == 1) {
					return DoorSide.FRONT;
				} else if (side == -1) {
					return DoorSide.BACK;
				}
			} else { // relativePosition.y < 0
				if (side == 1) {
					return DoorSide.BACK;
				} else if (side == -1) {
					return DoorSide.FRONT;
				}
			}	
		}

		Debug.LogError("Check side error");
		return DoorSide.NULL;
	}
}
