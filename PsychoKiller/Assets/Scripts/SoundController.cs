using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
	public AudioClip[] ac;
	private AudioSource audioSource;

	enum soundName{
		whereiseveryone,
		someonedown,
		screaming,
		doorSound
	};

	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySound(int n) {
		audioSource.clip = ac[n];
		audioSource.Play();
	}
}
