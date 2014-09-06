using UnityEngine;
using System.Collections;
using System;

public class PSMoveExample : MonoBehaviour {
	

	public GameObject torch;
	public string ipAddress = "128.2.237.237";
	public string port = "7899";
		
	public GameObject gem, handle;
	
	public bool isMirror = true;
	
	public float zOffset = 20;
	Quaternion temp = new Quaternion(0,0,0,0);
	
	
	#region GUI Variables
	string cameraStr = "Camera Switch On";
	string rStr = "0", gStr = "0", bStr = "0";
	string rumbleStr = "0";
	#endregion
	
	
	
	// Use this for initialization
	void Start () {
		PSMoveInput.Connect(ipAddress, int.Parse(port));
	}
		
	
	void Update() {
		if(PSMoveInput.IsConnected && PSMoveInput.MoveControllers[0].Connected) {
			print("connect");
			Vector3 gemPos, handlePos;
			MoveData moveData = PSMoveInput.MoveControllers[0].Data;
			gemPos = moveData.Position;
			handlePos = moveData.HandlePosition;
			if(isMirror) {
				gem.transform.localPosition = gemPos;
				handle.transform.localPosition = handlePos;
				handle.transform.localRotation = Quaternion.Euler(moveData.Orientation);
			}
			else {
				gemPos.z = -gemPos.z + zOffset;
				handlePos.z = -handlePos.z + zOffset;
				gem.transform.localPosition = gemPos;
				handle.transform.localPosition = handlePos;
				handle.transform.localRotation = Quaternion.LookRotation(gemPos - handlePos);
				handle.transform.Rotate(new Vector3(0,0,moveData.Orientation.z));
				
				/* using quaternion rotation directly
			 * the rotations on the x and y axes are inverted - i.e. left shows up as right, and right shows up as left. This code fixes this in case 
			 * the object you are using is facing away from the screen. Comment out this code if you do want an inversion along these axes
			 * 
			 * Add by Karthik Krishnamurthy*/
				
				temp = moveData.QOrientation;
				temp.x = -moveData.QOrientation.x;
				temp.y = -moveData.QOrientation.y;
				handle.transform.localRotation = temp;
			}
			if(moveData.GetButtons(MoveButton.T)) {
				print("pressed");
			}
		}
		if(PSMoveInput.IsConnected && PSMoveInput.MoveControllers[1].Connected) {
			MoveData moveDataL = PSMoveInput.MoveControllers[1].Data;
			if(moveDataL.GetButtons(MoveButton.T)) {
				torch.SetActive(false);
				print("CloseTorch");
			}
		}
	}
	
	// Update is called once per frame

	
	private void Reset() {
		cameraStr = "Camera Switch On";
		rStr = "0"; 
		gStr = "0"; 
		bStr = "0";
		rumbleStr = "0";
	}

	private bool MoveThrow() {
		MoveData moveData = PSMoveInput.MoveControllers[1].Data;
		//if(moveData.Acceleration) {
		return true;
		//}else {
		//	return false;
		//}
	}
}
