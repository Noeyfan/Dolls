using UnityEngine;
using System.Collections;

public class BasementLightControl : MonoBehaviour {
	Light[] lights;
	public int blinkTimes = 5;
	private bool isblinking;
	// Use this for initialization
	void Start () {
		lights = gameObject.GetComponentsInChildren<Light>();
		//StartCoroutine( BlinkLight(lights[0]));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if(!isblinking) {
			StartCoroutine(SelectBlink());
			isblinking = true;
		}
	}

	IEnumerator SelectBlink() {
		int chose = Random.Range(0, lights.Length - 4);
		print( "chose is " + chose);
		for(int i = 0; i < chose; i++) {
			int ran = Random.Range(0, lights.Length);
			StartCoroutine(BlinkLight(lights[ran]));
			print("Blink");

		}
		yield return new WaitForSeconds(4f);
		isblinking = false;

	}

	IEnumerator BlinkLight(Light l) {
		for(int i =0; i < blinkTimes; i++) {
			float n = Random.Range(0.1f,0.6f);
			l.enabled = false;
			yield return new WaitForSeconds(n);
			l.enabled = true;
			yield return new WaitForSeconds(n);
		}
	}
}
