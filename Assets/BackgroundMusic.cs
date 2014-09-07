using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

	public AudioClip AmbiClip;
	public AudioClip IntenseClip;
	public AudioClip HeartClip;

	public AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource.clip = HeartClip;
		audioSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
