using UnityEngine;
using System.Collections;

public class CutSceneScript : MonoBehaviour {
	public FirstPersonCharacter fps;
	public GameObject movinglight;
    public AudioSource[] sources;
    public AudioClip[] clips;
    public float[] timings;
    public OVRCameraController camcontrol;
    public GameObject ricky;
    public AudioClip[] footsteps;
    public DoorController door;
	public RickyAnimationScript rickyAni;


    int num_clips;
    int num_sources;
    int next_avail_source = 0;

    enum CSEvents
    {
        LINDSAY_SCREAMS = 0,
        RICKY_LINE_1 = 1,
        RICKY_LINE_2 = 2,
        RICKY_LINE_3 = 3,
        RICKY_LINE_4 = 4
    };

    bool[] event_over;

	// Use this for initialization
	void Start () {
       
        sources = GetComponents<AudioSource>();
        num_clips = clips.Length;
        num_sources = sources.Length-1;
        event_over = new bool[num_clips];
        for (int i = 0; i < num_clips; i++) event_over[i] = false;
	}

    // Update is called once per frame
    void Update()
    {
	
	}

    void possess()
    {
        fps.hasControl = false;
        fps.GetComponent<Rigidbody>().isKinematic = true;
        camcontrol.EnableOrientation = false;
        camcontrol.EnablePosition = false;
    }

	void takeControl()
	{
        possess();
		fps.GetComponent<Animator> ().enabled = true;
        
		
		//cutscenesplane.SetActive (true);
		StartCoroutine (playCutsceneAnimationSounds ());
        StartCoroutine(playCutSceneAnimationEvents());
        StartCoroutine(returnControlAfter(30.0f));
        StartCoroutine(turnAroundAndDie());
        StartCoroutine(playfootsteps());
	}

    void returnControl()
    {
        fps.hasControl = true;
        fps.GetComponent<Rigidbody>().isKinematic = false;
        fps.GetComponent<Animator>().enabled = false;
        camcontrol.EnableOrientation = true;
        camcontrol.EnablePosition = true;
        movinglight.GetComponent<Animator>().enabled = false;
        ricky.GetComponent<Animator>().enabled = false;
        //cutscenesplane.SetActive(false);
    }

	IEnumerator playCutsceneAnimationSounds()
	{
        for (float i = 0; i < 120.0f; i += Time.deltaTime)
        {
            for (int j = 0; j < num_clips; j++)
            {
                if (!event_over[j] && i >= timings[j])
                {
                    event_over[j] = true;
                    sources[next_avail_source].PlayOneShot(clips[j]);
                    next_avail_source = (next_avail_source + 1) % num_sources;
                }
            }

            yield return null;
        }
		
	}

    IEnumerator playCutSceneAnimationEvents()
    {
        bool ricky_appears = false;
		bool ricky_walks = false;
		bool ricky_stops = false;
        bool light_swings = false;
        bool player_dies = false;
        bool door_slam = false;
        for (float i = 0; i < 120.0f; i += Time.deltaTime)
        {
            if (!ricky_appears && i >= 16.0f)
            {
                ricky.SetActive(true);
            }
			if (!ricky_walks && i >= 21.0f)
			{
				rickyAni.SendMessage("startWalking");
			}
			if (!ricky_stops && i >= 26.0f)
			{
				rickyAni.SendMessage("stopWalking");
			}
            if (!light_swings && i >= 12.0f)
            {
                movinglight.SetActive(true);
                movinglight.GetComponent<Animator>().enabled = true;
            }
            if (!door_slam && i >= 28.6)
            {
                door.SendMessage("CloseDoor");
                door.isLocked = true;
            }
            //if (!player_dies && i >= 46.0f)
          //  {
          //      fps.Kill();
          //  }
            yield return null;
        }
        
        
      
    }

    IEnumerator returnControlAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        returnControl();
 
    }

    IEnumerator turnAroundAndDie()
    {
        yield return new WaitForSeconds(50.0f);
		float ricky_y = ricky.transform.position.y;
		Vector3 rickypos = fps.transform.position - fps.transform.forward;
		rickypos.y = ricky_y;
		ricky.transform.position = rickypos; 

        ricky.GetComponent<BillBoardScript>().enabled = true;
		ricky.GetComponentInChildren<MeshRenderer> ().material.shader = Shader.Find ("Custom/Ricky");
        possess();
        Quaternion original = camcontrol.transform.rotation;
        Quaternion turnaround = Quaternion.Euler(camcontrol.transform.rotation.eulerAngles + new Vector3(0, 180, 0));
        for (float i = 0; i < 2.0f; i += Time.deltaTime)
        {
            camcontrol.transform.rotation = Quaternion.Slerp(original, turnaround, i/2.0f);
            yield return null;
        }
        fps.Kill();
    }

    IEnumerator playfootsteps()
    {
        yield return new WaitForSeconds(20.0f);
        int counter = 0;
        for (float i = 0; i < 3.0f;)
        {
            for (float j = 0; j < 0.2f; j += Time.deltaTime)
            {
                i += Time.deltaTime;
                yield return null;
            }
            counter++;
            fps.PlayStepSound(FirstPersonCharacter.Floor.basement, (FirstPersonCharacter.Foot)(counter % 2));
            //yield return null;
        }
        yield return new WaitForSeconds(0.7f);
        for (float i = 0; i < 3.3f; )
        {
            for (float j = 0; j < 0.2f; j += Time.deltaTime)
            {
                i += Time.deltaTime;
                yield return null;
            }
            counter++;
            fps.PlayStepSound(FirstPersonCharacter.Floor.basement, (FirstPersonCharacter.Foot)(counter % 2));
            //yield return null;
        }
        yield return new WaitForSeconds(1.2f);
        for (float i = 0; i < 0.1f; i += Time.deltaTime)
        {
            for (float j = 0; j < 0.2f; j += Time.deltaTime)
            {
                i += Time.deltaTime;
                yield return null;
            }
            counter++;
            fps.PlayStepSound(FirstPersonCharacter.Floor.basement, (FirstPersonCharacter.Foot)(counter % 2));
            yield return null;
        }
    }
}
