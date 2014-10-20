using UnityEngine;
using System.Collections;

public class EnterDoorScript : MonoBehaviour {
	bool played;
	SoundController sc;

	// Use this for initialization
	void Start () {
		sc = GameObject.Find("SoundSets").GetComponent<SoundController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c) {
		if(c.tag == "Player") {
			if(!played) {
				sc.PlaySound(0);
				played = true;
			}
		}
	}
}
