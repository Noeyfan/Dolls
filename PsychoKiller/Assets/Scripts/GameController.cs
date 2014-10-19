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
		screaming
	};

	void Start () {
		notes = GameObject.Find("Notes");
		ShowNotes(false);
		sc = GameObject.Find("SoundSets").GetComponent<SoundController>();
		Invoke("PlayBeg", 1f);

	}
	
	// Update is called once per frame
	void Update () {
	}

	void ShowNotes (bool b) {
		notes.SetActive(b);
		showingNotes = true; //used for exit reading notes
	}

	void PlayBeg() {
		sc.PlaySound((int)soundName.whereiseveryone);
	}

	void GetKey() {
		hasKey = true;
	}
}
