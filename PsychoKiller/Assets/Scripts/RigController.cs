using UnityEngine;
using System.Collections;

public class RigController : MonoBehaviour {
	FirstPersonCharacter fpc;
	// Use this for initialization
	void Start () {
		fpc = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonCharacter>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider c) {
		print("OnRig");
		fpc.SendMessage("OnRig");
	}

	void OnTriggerExit() {
		print("Back to Normal");
		fpc.SendMessage("BackToNormalFloor");
	}
}