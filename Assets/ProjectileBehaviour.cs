using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter( Collision collision){
		Debug.Log ("hello");
		if (collision.other.tag == "Floor")
			//this.renderer.enabled = false;
			Destroy (this);
	}
}
