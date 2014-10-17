using UnityEngine;
using System.Collections;

public class Direction : MonoBehaviour {

	public Transform oculusCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// update transform direction of player
		Vector3 forward = oculusCamera.forward;
		forward.y = 0f;

		transform.forward = forward;
	}
}
