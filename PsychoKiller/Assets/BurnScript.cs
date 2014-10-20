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
            float step = (Time.deltaTime) / BurnTime;
            pos.y += step;
            party.startSize += (step * 0.3f);
            party.startLifetime += (step * 1.5f);
            party.gameObject.transform.localPosition = pos;
            yield return null;
        }

    }
}
