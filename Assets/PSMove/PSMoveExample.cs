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

	private bool startThrowFlag = false;
	Quaternion temp = new Quaternion(0,0,0,0);
	

	
	private float throwTime;
	public Rigidbody projectile;
	private int itemCount;
	private RaycastHit hit;
	private GameObject item;
	private bool torchStatus;

	public GameObject player;
	
	
	// Use this for initialization
	void Start () {
		torchStatus = false;
		torch.SetActive(torchStatus);
		PSMoveInput.Connect(ipAddress, int.Parse(port));
		throwTime = Time.time;
		itemCount = 0;
	}
	
	
	void Update() {
		Rgrab();
		
		if(PSMoveInput.IsConnected && PSMoveInput.MoveControllers[0].Connected) {
			Vector3 gemPos, handlePos;
			MoveData moveData = PSMoveInput.MoveControllers[0].Data;
			gemPos = moveData.Position;
			handlePos = moveData.HandlePosition;
			//print("connect");

			if(isMirror) {
				gem.transform.localPosition = gemPos;
				handle.transform.localPosition = handlePos;
				handle.transform.localRotation = Quaternion.Euler(moveData.Orientation);
			}
			else {
				//gemPos.z = -gemPos.z + zOffset;
				//handlePos.z = -handlePos.z + zOffset;
				//gem.transform.localPosition = gemPos;
				//handle.transform.localPosition = handlePos;
				//handle.transform.localRotation = Quaternion.LookRotation(gemPos - handlePos);
				//handle.transform.Rotate(new Vector3(0,0,moveData.Orientation.z));
				
				/* using quaternion rotation directly
			 * the rotations on the x and y axes are inverted - i.e. left shows up as right, and right shows up as left. This code fixes this in case 
			 * the object you are using is facing away from the screen. Comment out this code if you do want an inversion along these axes
			 * 
			 * Add by Karthik Krishnamurthy*/
				
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

	}
	
	// Update is called once per frame
	
	void Rgrab() {
		MoveData moveData = PSMoveInput.MoveControllers[0].Data;
		//Ray ray = Camera.main.ScreenPointToRay(moveData.Position);
		Debug.DrawLine(handle.transform.position, handle.transform.forward * 18, Color.red);
		if (Physics.Raycast (handle.transform.position, handle.transform.forward * 18, out hit)) {
						print ("Hit something");
						print (hit.collider.tag);
						//print (moveData.Position);
						if (itemCount == 0 && hit.collider.tag == "item" && moveData.GetButtonsDown (MoveButton.T)) {
								//if( hit.collider.tag == "item" && Input.GetKeyDown(KeyCode.F)) {
								itemCount++;
								item = hit.collider.gameObject;
								//Transform tempTransform = item.transform;
								item.transform.parent = handle.transform;
								item.transform.position = transform.position + transform.forward;
								print ("grab you!");
								print (itemCount);
						}
				}
		//}else if(itemCount >0 && Input.GetKeyDown(KeyCode.F)){
			if(itemCount >0){
			//if (moveData.Acceleration.x > 100 && moveData.GetButtons(MoveButton.T)){
			//	startThrowFlag = true;
			//}
			//else if (moveData.Acceleration.x < 10 && moveData.GetButtons(MoveButton.T) && startThrowFlag)
				if (moveData.Acceleration.x >100 && moveData.GetButtons(MoveButton.T))
					{
					if (Time.time > throwTime+1)
					{
						Debug.Log("Throw!");
						throwTime = Time.time;
						item.transform.parent = null;
						//Rigidbody clone;
						Vector3 ProjectilePos = transform.position;
						ProjectilePos.y += 1;
						ProjectilePos.x += 0.5f;
						//clone = Instantiate(projectile, ProjectilePos, transform.rotation) as Rigidbody;
						Vector3 throwDirection = player.transform.forward;
						throwDirection.y = 0;
						item.rigidbody.AddForce(throwDirection * 2000);
						item.rigidbody.angularVelocity = (new Vector3(UnityEngine.Random.Range (0,50), UnityEngine.Random.Range (0,100), UnityEngine.Random.Range (0,50)));
						itemCount--;
					}
				}
				//print ("throw you!");
				//item.transform.position = transform.position + transform.forward*10;
			}
		}
	}
