using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {
	float timeRecord = 0;
	//GameControl gc;
	// Use this for initialization
	void Start () {
		//gc = GameObject.Find("GameController").GetComponent<GameControl>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D() {
		timeRecord ++;
		if(timeRecord > 100) {
			if(name == "Approve") {
				//set user chose true;
				GameObject.Find("GameController").SendMessage("NextOne", true);
			}
			if(name == "Reject"){
				//set user chose false;
				GameObject.Find("GameController").SendMessage("NextOne", false);
			}
			print("NextOne");
			timeRecord = 0;
		}
	}

	void OnTriggerExit2D()  {
		timeRecord = 0;
	}
}
