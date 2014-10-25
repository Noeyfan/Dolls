using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	// Use this for initialization
	bool showingNotes;
	public bool hasKey;
	SoundController sc;

	GameObject currentNote;
	GameObject partyMusic;
	GameObject player;
	bool isCurrentNoteNull = true;
	public bool isMakeyMakeyActive = true;

	private Vector3 pMusicPos = new Vector3(14.34863f, 1.388297f, 2.568963f );
	private Vector3 pMusicPosNew = new Vector3(12.70374f, -1.295909f, -0.9008411f );
	private Vector3 decreasePoint = new Vector3(12.70374f, -1.295909f, -0.9008411f );
	private Vector3 wallInside = new Vector3(4.014453f, 0.56f, 4.086f );

	enum soundName{
		whereiseveryone,
		someonedown,
		screaming
	};

	void Start () {
		player = GameObject.FindWithTag("Player");
		ShowNotes(false);
		initSound();
		//Invoke("PlayBeg", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		//print(Vector3.Distance(player.transform.position, decreasePoint));
		//print((player.transform.position - wallInside).x);
		//print((player.transform.position - decreasePoint).z);
		if((((player.transform.position - decreasePoint).z > 5f) && ((player.transform.position - wallInside).x > 2.9f))  || (Vector3.Distance(player.transform.position, decreasePoint) > 9.5f)) {
			//print("enterroom");
			partyMusic.audio.volume -= Time.deltaTime;
			//enther room
		}
		else{
			partyMusic.audio.volume += Time.deltaTime;
		}
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
		partyMusic.audio.volume = 0f;
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
