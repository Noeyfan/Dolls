using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	// Use this for initialization
	bool showingNotes;
	bool hasKey;
	GameObject notes;

	void Start () {
		notes = GameObject.Find("Notes");
		hasKey = false;
		ShowNotes(false);

	}
	
	// Update is called once per frame
	void Update () {
	}

	void ShowNotes (bool b) {
		notes.SetActive(b);
		showingNotes = true; //used for exit reading notes
	}

	void Run() {
	}
}
