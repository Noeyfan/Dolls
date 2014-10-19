using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	// Use this for initialization
	bool showingNotes;
	public bool hasKey;
	GameObject notes;
	SoundController sc;

	enum soundName{
		whereiseveryone,
		someonedown,
	};

	void Start () {
		notes = GameObject.Find("Notes");
		hasKey = false;
		ShowNotes(false);
		sc = GameObject.Find("SoundSets").GetComponent<SoundController>();

		Invoke("playBeg", 1f);
		//sc.PlaySound(0);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void ShowNotes (bool b) {
		notes.SetActive(b);
		showingNotes = true; //used for exit reading notes
	}

	void playBeg() {
		sc.PlaySound((int)soundName.whereiseveryone);
	}
}
