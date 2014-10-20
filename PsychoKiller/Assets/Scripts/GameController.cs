using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	// Use this for initialization
	bool showingNotes;
	public bool hasKey;
	SoundController sc;

	GameObject currentNote;
	bool isCurrentNoteNull = true;
	public bool isMakeyMakeyActive = true;

	enum soundName{
		whereiseveryone,
		someonedown,
		screaming
	};

	void Start () {
		ShowNotes(false);
		sc = GameObject.Find("SoundSets").GetComponent<SoundController>();
		//Invoke("PlayBeg", 1f);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void ShowNotes (bool b) {
		showingNotes = true; //used for exit reading notes
	}

	void PlayBeg() {
		sc.PlaySound((int)soundName.whereiseveryone);
	}

	void GetKey() {
		hasKey = true;
	}

	public void SetCurrentNote(GameObject note) {
		currentNote = note;
		isCurrentNoteNull = false;
	}

	public void SetCurrentNoteNull() {
		isCurrentNoteNull = true;
	}

	public void RemoveCurrentNote() {
		if (!isCurrentNoteNull) {
			currentNote.SendMessage("GoBack");
		}
	}
}
