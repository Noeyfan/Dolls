using UnityEngine;
using System.Collections;

public class KeyController : MonoBehaviour {

	bool played = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision c) {
		if(c.gameObject.tag == "Hand") {
			GameObject.Find("GameController").SendMessage("GetKey");
			GameObject.Find("SoundSets").SendMessage("PlaySound",4);

			GameObject.FindGameObjectWithTag ("Phone").SendMessage ("ShowWithDelay", 2);
			Destroy(gameObject);
		}
	}
}