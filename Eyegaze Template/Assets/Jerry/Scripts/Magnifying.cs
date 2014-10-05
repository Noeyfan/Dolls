﻿using UnityEngine;
using System.Collections;

public class Magnifying : MonoBehaviour 
{
	public float Speed=1.0f;
	public float DeltaFrame=0.08f;
	public float ShootGap=0.1f;
	GameObject  MainCam;
	Vector2 Innitial;
	Vector2 Root;
	float HorizontalDisp;
	float VerticalDisp;

	private int Focusing=0;
	//private int AnimationFinished=0;

	// Use this for initialization
	void Start () 
	{
		MainCam = GameObject.FindGameObjectWithTag("MainCamera");
		Innitial= new Vector2(Input.mousePosition.x , Input.mousePosition.y) ;
		Root = new Vector2 (MainCam.transform.position.x, MainCam.transform.position.y);
		rigidbody2D.position = Root;
		Focusing = 0;

		HorizontalDisp = 0.0f;
		VerticalDisp = 0.0f;

		for(int i=0; i<12; i++)
		{
			transform.GetChild(i).GetComponent <SpriteRenderer>().enabled =false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		UnAnimate ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
	
		Focusing = 1;
		StartCoroutine (Animate (other));

	}
	

	void OnTriggerExit2D(Collider2D other)
	{
		Focusing = 0;
	}

	IEnumerator Animate(Collider2D OTHER)
	{ 
		for(int i=0; i<12; i++)
		{
			if(Focusing ==1)
			{
				yield return new WaitForSeconds (DeltaFrame);
				transform.GetChild(i).GetComponent <SpriteRenderer>().enabled =true;
			}
		}

	}

	void UnAnimate()
	{
		if(Focusing ==0)
		{
			for(int i=0; i<12; i++)
			{
				transform.GetChild(i).GetComponent <SpriteRenderer>().enabled =false;
			}
		}
	}
}
