using UnityEngine;
using System.Collections;

public class CreatureSpawner : MonoBehaviour {

	public GameObject monster;
	public MonsterBehavior monsterScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Random.Range (0, 100) < 0.05f) {
			Vector3 newMonsterPos = new Vector3 (30, -2, 0);
		
			newMonsterPos.z = Random.Range (-10f, 0f);

			monster = Instantiate (monster, newMonsterPos, transform.rotation) as GameObject;

			monster.transform.Rotate(0, -90, 0);
			monsterScript = monster.GetComponent<MonsterBehavior>();
			monsterScript.speed = 1f;
		}
	}
}
