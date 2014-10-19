using UnityEngine;
using System.Collections;

public class TriggerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if(c.tag == "Player") {
			//trigger running
			c.SendMessage("Run");
		}
		print ("enter");
	}
}
