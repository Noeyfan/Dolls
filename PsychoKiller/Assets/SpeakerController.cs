﻿using UnityEngine;
using System.Collections;

public class SpeakerController : MonoBehaviour {
	public GameObject partySound;
	private SoundController sc;
	private float recordtime;
	private float interve;
	// Use this for initialization
	void Start () {
		interve = 1f;
		sc = GameObject.Find("SoundSets").GetComponent<SoundController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c) {
		if(c.tag == "Hand") {
			toggleSound();
		}
	}

	void toggleSound() {
		if(recordtime <= Time.time - interve) {
			recordtime = Time.time;
			sc.PlaySound(4);
			if(partySound.GetComponent<AudioSource>().isPlaying) {
				partySound.audio.Stop();
			}else {
				partySound.audio.Play();
			}
		}
	}
}