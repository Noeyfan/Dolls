﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class TvController : MonoBehaviour {
	//public MovieTexture movT;
	// Use this for initialization
	protected bool isPlaying;

	private OculusController oc;
	private int cnt;
	private bool isOn;
	MovieTexture[] movT;

	public int channelAmount = 2;

	void Start () {
		oc = GameObject.FindGameObjectWithTag("Player").GetComponent<OculusController>();
	}

	void OnCollisionEnter(Collision c) {
		if(c.gameObject.tag == "Hand") {
			if(!isOn) {
				isOn = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(oc.isUsingMouse){
			if(Input.GetKey(KeyCode.A)) {
				ChangeChannel();
			}
		}
	}

	void PlayTv() {
		isPlaying = true;
	}

	void ChangeChannel() {
		if(cnt < movT.Length) {
		}
	}

	void initTV() {
		movT = new MovieTexture[channelAmount];
		for(int i = 0; i < channelAmount; i++) {
			string loadurl = "MovieTexture/Video/mv" + i;
			movT[i] = Resources.Load(loadurl) as MovieTexture;
		}
		renderer.material.mainTexture = movT[0];
		movT[0].Play();
		audio.Play();
	}
}
