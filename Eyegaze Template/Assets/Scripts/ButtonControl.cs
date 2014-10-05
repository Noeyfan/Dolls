using UnityEngine;
using System.Collections;

public class ButtonControl : MonoBehaviour {
	float timeRecord = 0;
	//GameControl gc;
	// Use this for initialization
	void Start () {
		//GameControl gc = GameObject.Find("GameController").GetComponent<GameControl>();
		//print(gc.people[0].nameD);
		//gc = GameObject.Find("GameController").GetComponent<GameControl>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D c) {
		if(c.tag == "Magnify") {
			timeRecord ++;
			if(timeRecord > 100) {
				timeRecord = 0;
				if(name == "Approve") {
					//set user chose true;
					GameObject.Find("GameController").SendMessage("NextOne", true);
				}
				if(name == "Reject"){
					//set user chose false;
					GameObject.Find("GameController").SendMessage("NextOne", false);
				}
				print("NextOne");
			}
		}
	}

	void OnTriggerExit2D()  {
		timeRecord = 0;
	}
}
