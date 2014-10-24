using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class TvController : MonoBehaviour {
	//public MovieTexture movT;
	// Use this for initialization
	protected bool isPlaying;
	void Start () {
		MovieTexture movT =  renderer.material.mainTexture as MovieTexture;
		audio.clip = movT.audioClip;
		audio.Play();
		movT.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PlayTv() {
		isPlaying = true;
	}
}
