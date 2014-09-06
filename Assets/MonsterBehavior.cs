using UnityEngine;
using System.Collections;

public class MonsterBehavior : MonoBehaviour {


	public float speed = 1f;
	public bool isWalking = true;

	// Use this for initialization
	void Start () {
	
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
