using UnityEngine;
using System.Collections;
using System;

public class LeftHandControl : MonoBehaviour {
	
	public GameObject torch;
	public string ipAddress = "128.2.239.45";
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
	
	private float throwTime;
	private int itemCount;
	private RaycastHit hit;
	private GameObject item;
	private bool torchStatus;
	
	
	// Use this for initialization
	void Start () {
		torchStatus = false;
		torch.SetActive(torchStatus);
		PSMoveInput.Connect(ipAddress, int.Parse(port));
		throwTime = Time.time;
		itemCount = 0;
	}
	
	
	void Update() {
		
		if(PSMoveInput.IsConnected && PSMoveInput.MoveControllers[1].Connected) {
			Vector3 gemPos, handlePos;
			MoveData moveData = PSMoveInput.MoveControllers[1].Data;
			gemPos = moveData.Position;
			handlePos = moveData.HandlePosition;
			//print("connect");	
			if(isMirror) {
				gem.transform.localPosition = gemPos;
				handle.transform.localPosition = handlePos;
				handle.transform.localRotation = Quaternion.Euler(moveData.Orientation);
			}
			else {			
				temp = moveData.QOrientation;
				temp.x = -moveData.QOrientation.x;
				temp.y = -moveData.QOrientation.y;
				handle.transform.rotation = temp;
			}
			if(moveData.GetButtons(MoveButton.T)) {
				//use for get something
				//print("pressed");
			}
		}
		if(PSMoveInput.IsConnected && PSMoveInput.MoveControllers[1].Connected) {
			MoveData moveDataL = PSMoveInput.MoveControllers[1].Data;
			if(moveDataL.GetButtonsDown(MoveButton.T)) {
				if(torchStatus == true) {
					torch.SetActive(false);
					print("Toggle Torch off");
					torchStatus= !torchStatus;
				}else {
					torch.SetActive(true);
					torchStatus= !torchStatus;
					print("Toggle Torch on");
				}
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
}