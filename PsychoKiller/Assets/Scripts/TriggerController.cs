﻿using UnityEngine;
using System.Collections;

public class TriggerController : MonoBehaviour {
	bool played;
	public GameObject finalDoor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if(c.tag == "Player") {
			//trigger running
			//c.SendMessage("Run");
			finalDoor.SendMessage("CloseDoor");
			finalDoor.GetComponent<DoorController>().isLocked = true;
			if(!played) {
				GameObject.Find("SoundSets").SendMessage("PlaySound", 2);
				played = true;
			}
		}
	}
}