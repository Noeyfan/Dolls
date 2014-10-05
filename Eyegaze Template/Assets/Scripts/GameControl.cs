using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {
	//GameObject[] people;
	int count = 0;
	public int numbOfPeople = 10;
	public Vector3 pOffset;
	public Vector3 immOffset;

	public GameObject namePos;
	public GameObject flagPos;
	public GameObject birPos;
	public GameObject sexPos;

	peopleSkeleton[] people;
	//Component[] parts;
	public GameObject peoplePrefab;

	string[,] resouce_name = {
		{"Face1", "Face2", "Face3"},
		//{"Hair1", "Hair2", "Hair2"},
		{"Eye1", "Eye2", "Eye3"},
		{"Nose1", "Nose2", "Nose3"},
		{"Mouth1", "Mouth2", "Mouth3"},
		{"Beard"},
	};

	string[,] pp_name = {
		{"name01","name02","name03","name04","name05","name06","name07","name08","name09","name10"},
		{"Flag_of_China","Flag_of_France","Flag_of_Germany","Flag_of_India","Flag_of_Japan","Flag_of_Russia","Flag_of_South_Korea","Flag_of_the_UK","Flag_of_the_United_States","Flag_of_TW"},
		{"birthday01","birthday02","birthday03","birthday04","birthday05","birthday06","birthday07","birthday08","birthday09","birthday10"},
	};
	// Use this for initialization

	struct peopleSkeleton {
		public GameObject p;
		public bool identity;
		public bool ismale;
		public bool playerChoice;
	};

	void Start () {
		people  = new peopleSkeleton[numbOfPeople];
		InstantiatePeople();
		GenerateRandom();
		people[count].p.SetActive(true);
		//for(int i = 0; i < people.Length; i++) {
		//	print(people[i].ismale);
		//}
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
		//for(int k =0; k < 5; k++) {
		for(int k =0; k < 4; k++) { // no hair
			int rVal = Random.Range(0,3);
			string parts_name = resouce_name[k, rVal];
			GameObject temp = (GameObject)Resources.Load(parts_name, typeof(GameObject));
			if(k == 0) {
				if(rVal == 0){
					people[i].ismale = false;
				}
				else {
					people[i].ismale = true;
				}
			}
			GameObject Inst = Instantiate(temp) as GameObject;
			Inst.transform.parent = people[i].p.transform;
			//if (k =4 3) { no hair
			if (k == 3) {
				GeneratePP(b,i);
				GenerateImmgration(b,i);
			}
		}
	}
	//}

	void GeneratePP (bool b, int i) {
		GameObject sex;
		GameObject passport = (GameObject)Resources.Load("Passport", typeof(GameObject));
		passport = Instantiate(passport) as GameObject;
		if(people[i].ismale) {
			sex = Instantiate((GameObject)Resources.Load("Male", typeof(GameObject))) as GameObject;
		}else {
			sex = Instantiate((GameObject)Resources.Load("Female", typeof(GameObject))) as GameObject;
		}

		if(b) {
			//slightly changed
			GameObject clone = Instantiate(people[i].p, passport.transform.position - pOffset, Quaternion.identity) as GameObject;

			// change clone
			clone.transform.localScale -= new Vector3(0.7F, 0.7F, 0);
			clone.SetActive(true);
			//Transform[] allChildren = clone.gameObject.GetComponentsInChildren<Transform>();
			//foreach (Transform children in allChildren) {
				//if(children.name.Contains("Hair")) {
					//print("hair");
					//children.gameObject.renderer = (Instantiate((GameObject)Resources.Load(resouce_name[1, Random.Range(0, 2)], typeof(GameObject))) as GameObject).renderer;
				//}
			//}
			clone.transform.parent = passport.transform;
			//generate info
			for(int k = 0; k < 3; k++) {
				string parts_name = pp_name[k, Random.Range(0,10)];
				GameObject tmp_pp = Instantiate((GameObject)Resources.Load(parts_name, typeof(GameObject))) as GameObject;
				tmp_pp.SetActive(true);
				tmp_pp.transform.parent = clone.transform;
			}
			sex.SetActive(true);
			sex.transform.parent = passport.transform;
			passport.transform.parent = people[i].p.transform;
			passport.SetActive(true);
		} else {
			GameObject clone = Instantiate(people[i].p, passport.transform.position - pOffset, Quaternion.identity) as GameObject;
			clone.transform.localScale -= new Vector3(0.7F, 0.7F, 0);
			clone.SetActive(true);
			//Beard for test

			//place that need to be changed
			//int place_change = Random.Range(0,2);

			GameObject tmp = (GameObject)Resources.Load("Beard", typeof(GameObject));
			GameObject ist = Instantiate(tmp) as GameObject;
			ist.transform.parent = clone.transform;
			clone.transform.parent = passport.transform;

			for(int k = 0; k < 3; k++) {
				string parts_name = pp_name[k, Random.Range(0,10)];
				GameObject tmp_pp = Instantiate((GameObject)Resources.Load(parts_name, typeof(GameObject))) as GameObject;
				tmp_pp.SetActive(true);
				tmp_pp.transform.parent = clone.transform;
			}
			sex.SetActive(true);
			sex.transform.parent = passport.transform;

			passport.transform.parent = people[i].p.transform;
			passport.SetActive(true);
			//huge changed
		}
	}

	void GenerateImmgration(bool b, int i) {
		GameObject name, flg, bir;
		GameObject sf = (GameObject)Resources.Load("ScreenFile", typeof(GameObject));
		sf = Instantiate(sf) as GameObject;
		if(b) {
			GameObject clone_im = Instantiate(people[i].p, sf.transform.position + immOffset, Quaternion.identity) as GameObject;
			clone_im.transform.localScale -= new Vector3(0.45F, 0.45F, 0);
			clone_im.SetActive(true);
			foreach (Transform child in clone_im.transform) {
				if(child.tag == "Passport") {
					child.gameObject.SetActive(false);
				}
			}

			//random change

			name = people[i].p.transform.GetChild(4).GetChild(2).GetChild(4).gameObject;
			name = Instantiate(name, namePos.transform.position, Quaternion.identity) as GameObject;
			name.SetActive(true);
			name.transform.parent = sf.transform;
			name.transform.localScale -= new Vector3(0.8F, 0.8F, 0);

			flg = people[i].p.transform.GetChild(4).GetChild(2).GetChild(5).gameObject;
			flg = Instantiate(flg, flagPos.transform.position, flagPos.transform.rotation) as GameObject;
			flg.SetActive(true);
			flg.transform.parent = sf.transform;
			flg.transform.localScale -= new Vector3(0.1F, 0.1F, 0);

			bir = people[i].p.transform.GetChild(4).GetChild(2).GetChild(6).gameObject;
			bir = Instantiate(bir, birPos.transform.position, Quaternion.identity) as GameObject;
			bir.SetActive(true);
			bir.transform.parent = sf.transform;
			//bir.transform.localScale += new Vector3(0.8F, 0.8F, 0);

			bir = people[i].p.transform.GetChild(4).GetChild(3).gameObject;
			bir = Instantiate(bir, sexPos.transform.position, Quaternion.identity) as GameObject;
			bir.SetActive(true);
			bir.transform.parent = sf.transform;
			bir.transform.localScale += new Vector3(0.5F, 0.5F, 0);

			clone_im.transform.parent = sf.transform;
			sf.transform.parent = people[i].p.transform;
			sf.SetActive(true);
		}else {
			//wrong one
			GameObject clone_im = Instantiate(people[i].p, sf.transform.position + immOffset, Quaternion.identity) as GameObject;
			clone_im.transform.localScale -= new Vector3(0.45F, 0.45F, 0);
			clone_im.SetActive(true);
			foreach (Transform child in clone_im.transform) {
				if(child.tag == "Passport") {
					child.gameObject.SetActive(false);
				}
			}

			//big change
			int place_change = Random.Range(1,4);
			for(int k = 0; k < place_change; k++) {
				int parts = Random.Range(1,4);
				print("enter change");
				GameObject msk = Instantiate((GameObject)Resources.Load(resouce_name[parts,Random.Range(0,3)], typeof(GameObject))) as GameObject;
				Destroy( people[i].p.transform.GetChild(parts).gameObject);
				msk.transform.parent = people[i].p.transform;
			}


			name = people[i].p.transform.GetChild(4).GetChild(2).GetChild(5).gameObject;
			name = Instantiate(name, namePos.transform.position, Quaternion.identity) as GameObject;
			name.SetActive(true);
			name.transform.parent = sf.transform;
			name.transform.localScale -= new Vector3(0.8F, 0.8F, 0);
			
			flg = people[i].p.transform.GetChild(4).GetChild(2).GetChild(6).gameObject;
			flg = Instantiate(flg, flagPos.transform.position, flagPos.transform.rotation) as GameObject;
			flg.SetActive(true);
			flg.transform.parent = sf.transform;
			flg.transform.localScale -= new Vector3(0.1F, 0.1F, 0);
			
			bir = people[i].p.transform.GetChild(4).GetChild(2).GetChild(7).gameObject;
			bir = Instantiate(bir, birPos.transform.position, Quaternion.identity) as GameObject;
			bir.SetActive(true);
			bir.transform.parent = sf.transform;
			//bir.transform.localScale += new Vector3(0.8F, 0.8F, 0);
			
			bir = people[i].p.transform.GetChild(4).GetChild(3).gameObject;
			bir = Instantiate(bir, sexPos.transform.position, Quaternion.identity) as GameObject;
			bir.SetActive(true);
			bir.transform.parent = sf.transform;
			bir.transform.localScale += new Vector3(0.5F, 0.5F, 0);
	
			clone_im.transform.parent = sf.transform;
			sf.transform.parent = people[i].p.transform;
			sf.SetActive(true);
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
				//GameObject temp= GameObject.Find("P_name");
				//temp.guiText.text = "Name: " + people_name[Random.Range(0,people_name.Length)];
			}	else {
				for(int i = 0; i < numbOfPeople; i++) {
					//print(people[i].identity +" "+ people[i].playerChoice + i);
					if(people[i].identity == people[i].playerChoice) {
						//print("same");
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