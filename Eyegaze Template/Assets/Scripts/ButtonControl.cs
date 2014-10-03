using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {
	float timeRecord = 0;
	// Use this for initialization
	void Start () {
		if(name == "Approve") {
			//set user chose true;
		}
		if(name == "Reject"){
			//set user chose false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D() {
		timeRecord ++;
		if(timeRecord > 100) {
			print("NextOne");
			GameObject.Find("GameController").SendMessage("NextOne");
			timeRecord = 0;
		}
	}

	void OnTriggerExit2D()  {
		timeRecord = 0;
	}
}
