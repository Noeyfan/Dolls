﻿using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {
	
	public enum DoorType {RIGHT, LEFT};
	public enum DoorFace {NORMAL, SIDE};
	public enum DoorSide {NULL, FRONT, BACK};
	public enum XAxisPos {X_INSIDE, X_OUTSIDE};
	public enum DoorState {IDLE, INSIDE, OUTSIDE};
	
	public DoorType doorType;
	public DoorFace doorFace;
	public XAxisPos xAxisPos;
	public bool isParent = true;
	public bool isBasementDoor;
	public bool isLocked;
	public bool isFinalDoor;

	
	protected DoorState doorState = DoorState.IDLE;
	
	protected int side;
	
	// animation attribute
	protected bool isAnimating = false;
	protected float animationTime = 0.7f;
	protected float elapsedTimeAnimation = 0f;
	protected Quaternion firstRotation, targetRotation;
	
	private GameController gc;
	private FirstPersonCharacter fc;
	private bool startkill;

	private Transform doorTransform;

	// Use this for initialization
	void Start () {
		if (isParent)
			doorTransform = transform.parent.transform;
		else
			doorTransform = transform;

		// close all doors
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		fc = GameObject.FindWithTag("Player").GetComponent<FirstPersonCharacter>();
		CloseDoor ();

		//if (GetComponent<BoxCollider> ().center.x < 0f) side = -1;		
		//else side = 1;
		if (xAxisPos == XAxisPos.X_OUTSIDE) side = -1;
		else if (xAxisPos == XAxisPos.X_INSIDE) side = 1;
	}
	
	// Update is called once per frame
	void Update () {
		// animation
		if (isAnimating) {
			elapsedTimeAnimation += Time.deltaTime;
			
			doorTransform.localRotation = Quaternion.Slerp(firstRotation, targetRotation, elapsedTimeAnimation / animationTime);
			
			if (elapsedTimeAnimation >= animationTime) {
				isAnimating = false;
				elapsedTimeAnimation = 0f;
			}
		}
	}

	void OnCollisionEnter(Collision c) {
		if(c.gameObject.tag == "Hand") {
			if(!isLocked) {
				if(!isBasementDoor || (isBasementDoor && gc.hasKey)) {
					
					//AnimateDoor(CheckSideCollision(contactPoint));
					AnimateDoor(CheckSideCollision(GameObject.FindGameObjectWithTag("Player").transform.position));
				}
			}else if(isFinalDoor){
				//if(!GameObject.Find("SoundSets").GetComponent<AudioSource>().isPlaying) {
				//	GameObject.Find("SoundSets").SendMessage("PlaySound", 5);
				//}// Lock Sound
				GameObject.Find("SoundSets").SendMessage("PlaySound", 5);
				if(!startkill) {
					fc.Kill();
					startkill = true;
				}
			}else {
				GameObject.Find("SoundSets").SendMessage("PlaySound", 7);
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
					StartAnimation(Quaternion.Euler(doorTransform.localRotation.eulerAngles.x, doorTransform.localRotation.eulerAngles.y + (90f * ConvertDoorType()), doorTransform.localRotation.eulerAngles.z));
					doorState = DoorState.INSIDE;
				} else if (doorSide == DoorSide.BACK) {
					StartAnimation(Quaternion.Euler(doorTransform.localRotation.eulerAngles.x, doorTransform.localRotation.eulerAngles.y + (-90f * ConvertDoorType()), doorTransform.localRotation.eulerAngles.z));
					doorState = DoorState.OUTSIDE;
				}
				break;
			case DoorState.INSIDE :
				if (doorSide == DoorSide.BACK) {
					StartAnimation(Quaternion.Euler(doorTransform.localRotation.eulerAngles.x, doorTransform.localRotation.eulerAngles.y + (-90f * ConvertDoorType()), doorTransform.localRotation.eulerAngles.z));
					doorState = DoorState.IDLE;
				}
				break;
			case DoorState.OUTSIDE :
				if (doorSide == DoorSide.FRONT) {
					StartAnimation(Quaternion.Euler(doorTransform.localRotation.eulerAngles.x, doorTransform.localRotation.eulerAngles.y + (90 * ConvertDoorType()), doorTransform.localRotation.eulerAngles.z));
					doorState = DoorState.IDLE;
				}
				break;
			}
		}
	}
	
	protected void StartAnimation(Quaternion targetRotation) {
		this.firstRotation = doorTransform.localRotation;
		this.targetRotation = targetRotation;
		isAnimating = true;
		
		// sound
		GameObject.Find("SoundSets").SendMessage("PlaySound", 3);
	}
	
	protected void CloseDoor() {
		switch (doorFace) {
		case DoorFace.NORMAL :
			doorTransform.localRotation = Quaternion.Euler(doorTransform.localRotation.eulerAngles.x, 0f, doorTransform.localRotation.eulerAngles.z);
			break;
		case DoorFace.SIDE :
			doorTransform.localRotation = Quaternion.Euler(doorTransform.localRotation.eulerAngles.x, 90f, doorTransform.localRotation.eulerAngles.z);
			break;
		}
	}
	
	protected DoorSide CheckSideCollision(Vector3 position) {
		Vector3 relativePosition = doorTransform.InverseTransformPoint (position);
		
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
