using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {
    AudioSource[] sources;
    public AudioClip[] clips;
    public float[] times;

	// Use this for initialization
	void Start () {
        sources = GetComponents<AudioSource>();
        StartCoroutine(playOverlap());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator playOverlap()
    {
        for (int i = 0; i < times.Length; i++)
        {
            yield return new WaitForSeconds(times[i]);
            sources[i].clip = clips[i];
            sources[i].Play();
        }
    }
}
