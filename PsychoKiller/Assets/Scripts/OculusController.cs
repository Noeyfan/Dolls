using UnityEngine;
using System.Collections;

public class OculusController : MonoBehaviour {
	
	public GameObject leftOculusCamera, rightOculusCamera;

	public bool isOculusActive = true;

	// Use this for initialization
	void Start () {
		SetEnableOculus (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (isOculusActive) {
			// update transform direction of player
			Vector3 forward = rightOculusCamera.transform.forward;
			forward.y = 0f;
			
			transform.forward = forward;
		}
	}

	public void SetEnableOculus(bool value) {
		isOculusActive = value;

		// oculus
		leftOculusCamera.GetComponent<OVRCamera> ().isUpdateRotation = isOculusActive;
		rightOculusCamera.GetComponent<OVRCamera> ().isUpdateRotation = isOculusActive;
	}
}
