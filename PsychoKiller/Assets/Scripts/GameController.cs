using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	// Use this for initialization
	bool showingNotes;
	public bool hasKey;
	SoundController sc;

	GameObject currentNote;
	GameObject partyMusic;
	bool isCurrentNoteNull = true;
	public bool isMakeyMakeyActive = true;

	private Vector3 pMusicPos = new Vector3(14.34863f, 1.388297f, 2.568963f );
	private Vector3 pMusicPosNew = new Vector3(12.70374f, -1.295909f, -0.9008411f );

	enum soundName{
		whereiseveryone,
		someonedown,
		screaming
	};

	void Start () {
		ShowNotes(false);
		initSound();
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

	void initSound() {
		sc = GameObject.Find("SoundSets").GetComponent<SoundController>();
		partyMusic  = GameObject.Find("PartyMusic");
		partyMusic.transform.parent = null;
		partyMusic.transform.position = (pMusicPos);
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
	public void ChangeSoundPos() {
		partyMusic.transform.position = pMusicPosNew;
		partyMusic.GetComponent<AudioLowPassFilter>().enabled = false;
	}
}
