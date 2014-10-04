using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {
	//GameObject[] people;
	int count = 0;
	public int numbOfPeople = 10;
	public Vector3 pOffset;
	peopleSkeleton[] people;
	string[] people_name = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k"};
	//Component[] parts;
	public GameObject peoplePrefab;

	string[,] resouce_name = {
		{"Face1", "Face2"},
		{"Hair1", "Hair2"},
		{"Eye1", "Eye2"},
		{"Nose1", "Nose2"},
		{"Mouth1", "Mouth2"},
	};
	// Use this for initialization

	struct peopleSkeleton {
		public GameObject p;
		public bool identity;
		public bool playerChoice;
	};

	void Start () {
		people  = new peopleSkeleton[numbOfPeople];
		InstantiatePeople();
		GenerateRandom();
		people[count].p.SetActive(true);
		//GameObject testPrefab = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/WhiteBoard.prefab", typeof(GameObject));
		//Instantiate(testPrefab);
	}
	
	// Update is called once per frame
	void Update () {
		/********** key direct test
		if(Input.GetKeyDown(KeyCode.Space)) {
			if(count >= 1) {
				people[count - 1].p.SetActive(false);
			}
			people[count++].p.SetActive(true);
		}
		//print(Random.Range(0,1));
	**********/
	}

	void InstantiatePeople() {
		for(int i = 0; i <people.Length; i++) {
			people[i].p = Instantiate(peoplePrefab) as GameObject;
			people[i].p.SetActive(false);
		}
	}

	void GenerateRandom () {
		for(int i = 0; i< numbOfPeople; i++) {
			if(Random.Range(0,6) >= 1) {
				people[i].identity = true;
			}else {
				people[i].identity = false;
			}
		}
		for(int i =0; i < numbOfPeople; i++) {
			Generate(people[i].identity, i);
		}
	}

	void Generate(bool b, int i) {
		//GameObject temp = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/Parts/Face1.prefab", typeof(GameObject));
		//Generate Character
		//Generate Passport = Info
		//Generate Info
		//Generate Dialog
		for(int k =0; k < 5; k++) {
			GameObject temp = (GameObject)Resources.Load(resouce_name[k, Random.Range(0, 2)], typeof(GameObject));
			GameObject Inst = Instantiate(temp) as GameObject;
			Inst.transform.parent = people[i].p.transform;
			if (k == 4) {
				GeneratePP(b,i);
			}
		}
	}
	//}

	void GeneratePP (bool b, int i) {
		if(b) {
			//slightly changed
			GameObject passport = (GameObject)Resources.Load("Passport", typeof(GameObject));
			passport = Instantiate(passport) as GameObject;
			GameObject clone = Instantiate(people[i].p, passport.transform.position - pOffset, Quaternion.identity) as GameObject;
			// change clone
			clone.transform.localScale -= new Vector3(0.6F, 0.6F, 0);
			clone.SetActive(true);
			Transform[] allChildren = clone.gameObject.GetComponentsInChildren<Transform>();
			foreach (Transform children in allChildren) {
				if(children.name.Contains("Hair")) {
					print("hair");
					//children.gameObject.renderer = (Instantiate((GameObject)Resources.Load(resouce_name[1, Random.Range(0, 2)], typeof(GameObject))) as GameObject).renderer;
				}
			}
			clone.transform.parent = passport.transform;
			passport.transform.parent = people[i].p.transform;
			passport.SetActive(true);
		} else {
			GameObject passport = (GameObject)Resources.Load("Passport", typeof(GameObject));
			passport = Instantiate(passport) as GameObject;
			GameObject clone = Instantiate(people[i+1].p, passport.transform.position - pOffset, Quaternion.identity) as GameObject;
			clone.transform.localScale -= new Vector3(0.6F, 0.6F, 0);
			clone.SetActive(true);
			clone.transform.parent = passport.transform;
			passport.transform.parent = people[i].p.transform;
			passport.SetActive(true);
			//huge changed
		}
	}

	//Useless for now
	/*void GenerateFace(GameObject go){// Useless, Cannot pass reference
		print(go.name);
		switch(Random.Range(0,2)){
		case 0:
			go = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/Parts/Face1.prefab", typeof(GameObject));
			break;
		case 1:
			go = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/Parts/Face2.prefab", typeof(GameObject));
			break;
		}
		//foreach (var face in GameObject.FindGameObjectsWithTag("Face")) {
		//	face.SetActive(false);
		//}
	}*/

	void NextOne(bool b) {
		if(count < numbOfPeople) 
		{
			people[count].p.SetActive(false);
			people[count++].playerChoice = b;
			if(count < numbOfPeople){
				people[count].p.SetActive(true);
				GameObject temp= GameObject.Find("P_name");
				temp.guiText.text = "Name: " + people_name[Random.Range(0,people_name.Length)];
			}	else {
				for(int i = 0; i < numbOfPeople; i++) {
					print(people[i].identity +" "+ people[i].playerChoice + i);
					if(people[i].identity == people[i].playerChoice) {
						print("same");
						//nothing
					}else if(people[i].identity != people[i].playerChoice){
						print("lose");
						return;
					}
				}
				print("win");
			}
		}
	}
}