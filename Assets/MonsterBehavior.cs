using UnityEngine;
using System.Collections;

public class MonsterBehavior : MonoBehaviour {


	public float speed = 1f;
	public bool isWalking = true;

	public AudioClip Sound1;
	public AudioClip Sound2;

	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource.clip = Sound1;
		audioSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isWalking)
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision collision)
	{
		isWalking = false;
		//Turn dead animation switch on
		//Kill object after a few seconds?
	}

	void initSpeed(){
		speed = 1f;
	}
}
