using UnityEngine;
using System.Collections;

public class BurnScript : MonoBehaviour {
    float BurnTime = 20.0f;
    public ParticleSystem party;
	// Use this for initialization
	void Start () {
        party.Stop();
        StartCoroutine(burnbaby());
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator burnbaby()
    {
        party.Play();
        Vector3 pos = party.gameObject.transform.localPosition;
        for (float i = 0; i < BurnTime; i += Time.deltaTime)
        {
            Debug.Log("Boooo");
            this.renderer.material.SetFloat("_BurnAmount", i / BurnTime);
            //pos.x -= i / (BurnTime * 300);
            pos.y += i / (BurnTime * 250);
            party.startSize += (i / BurnTime)/1000.0f;
            party.startLifetime += (i / BurnTime) / 75.0f;
            party.gameObject.transform.localPosition = pos;
            yield return null;
        }

    }
}
