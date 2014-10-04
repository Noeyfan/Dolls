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
		GameObject temp;
		GameObject Inst;
		//GameObject temp = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/Parts/Face1.prefab", typeof(GameObject));
		//Generate Character
		//Generate Passport = Info
		//Generate Info
		//Generate Dialog

//		if(b) {
		for(int k = 0; k < 5; k++) {
			switch(k){
			case 0: // Change Face
				switch(Random.Range(0,2)) {
				case 0:
					temp = (GameObject)Resources.Load("Face1", typeof(GameObject));
					Inst = Instantiate(temp) as GameObject;
					Inst.transform.parent = people[i].p.transform;
					break;
				case 1:
					temp = (GameObject)Resources.Load("Face2", typeof(GameObject));
					Inst = Instantiate(temp) as GameObject;
					Inst.transform.parent = people[i].p.transform;
					break;
				}
				//temp= GameObject.Find("P_name");
				//temp.guiText.text = peopel_name[Random.Range(0,peopel_name.Length)];
				//temp.transform.parent = people[i].p.transform;
				break;
			case 1:// Change Hair
				switch(Random.Range(0,2)) {
				case 0:
					temp = (GameObject)Resources.Load("Hair1", typeof(GameObject));
					Inst = Instantiate(temp) as GameObject;
					Inst.transform.parent = people[i].p.transform;
					break;
				case 1:
					temp = (GameObject)Resources.Load("Hair2", typeof(GameObject));
					Inst = Instantiate(temp) as GameObject;
					Inst.transform.parent = people[i].p.transform;
					break;
				}
				break;
			case 2://Change Eyes
				switch(Random.Range(0,2)) {
				case 0:
					temp = (GameObject)Resources.Load("Eye1", typeof(GameObject));
					Inst = Instantiate(temp) as GameObject;
					Inst.transform.parent = people[i].p.transform;
					break;
				case 1:
					temp = (GameObject)Resources.Load("Eye2", typeof(GameObject));
					Inst = Instantiate(temp) as GameObject;
					Inst.transform.parent = people[i].p.transform;
					break;
				}
				break;
			case 3://Change Nose
				switch(Random.Range(0,2)) {
				case 0:
					temp = (GameObject)Resources.Load("Nose1", typeof(GameObject));
					Inst = Instantiate(temp) as GameObject;
					Inst.transform.parent = people[i].p.transform;
					break;
				case 1:
					temp = (GameObject)Resources.Load("Nose2", typeof(GameObject));
					Inst = Instantiate(temp) as GameObject;
					Inst.transform.parent = people[i].p.transform;
					break;
				}
				break;
			case 4:
				switch(Random.Range(0,2)) {
				case 0:
					temp = (GameObject)Resources.Load("Mouth1", typeof(GameObject));
					Inst = Instantiate(temp) as GameObject;
					Inst.transform.parent = people[i].p.transform;
					break;
				case 1:
					temp = (GameObject)Resources.Load("Mouth2", typeof(GameObject));
					Inst = Instantiate(temp) as GameObject;
					Inst.transform.parent = people[i].p.transform;
					break;
				}
				GeneratePP(b,i); 				//generate other info
				break;
			}
		}

			/*foreach (Transform parts in people[i].p.transform) {
				//print ("kid: " + parts.name);
				switch(parts.name) {
				case "Face":
					//GenerateFace(parts.gameObject);
					switch(Random.Range(0,2)){
					case 0:
						parts.gameObject = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/Parts/Face1.prefab", typeof(GameObject));
						break;
					case 1:
						parts.gameObject = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/Parts/Face2.prefab", typeof(GameObject));
						break;
					}
					break;
				case "Nose":
					break;
				case "Eyes":
					break;
				case "Mouth":
					break;
				case "Hairs":
					break;
				}
			}
		*/ //}else {
			//Genreate False
		}
	//}

	void GeneratePP (bool b, int i) {
		if(b) {
			//slightly changed
			GameObject passport = (GameObject)Resources.Load("Passport", typeof(GameObject));
			passport = Instantiate(passport) as GameObject;
			GameObject clone = Instantiate(people[i].p, passport.transform.position - pOffset, Quaternion.identity) as GameObject;
			clone.transform.localScale -= new Vector3(0.6F, 0.6F, 0);
			clone.SetActive(true);
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
	void GenerateFace(GameObject go){// Useless, Cannot pass reference
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
	}

	void NextOne(bool b) {
		if(count < numbOfPeople) {
			if(count >= 1) {
				people[count - 1].p.SetActive(false);
				people[count - 1].playerChoice = b;
			}
			people[count++].p.SetActive(true);
			GameObject temp= GameObject.Find("P_name");
			temp.guiText.text = "Name: " + people_name[Random.Range(0,people_name.Length)];
		}
		else {
			for(int i = 0; i < numbOfPeople; i++) {
				if(people[i].identity == people[i].playerChoice) {
					print("same");
					//nothing
				}else {
					print("lose");
					return;
				}
			}
			print("win");
		}
	}
}