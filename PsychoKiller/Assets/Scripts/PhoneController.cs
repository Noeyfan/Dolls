using UnityEngine;
using System.Collections;

public class PhoneController : MonoBehaviour
{

		public GameObject phoneSource;

		private GameObject palm, phone;

		// Use this for initialization
		void Start ()
		{
				phone = Instantiate (phoneSource) as GameObject;
		}
	
		// Update is called once per frame
		void Update ()
		{
				GameObject palm = GameObject.Find ("palm");

				if (palm != null) {
						phone.transform.position = palm.transform.position;
						phone.transform.rotation = palm.transform.rotation;
						phone.transform.Translate (new Vector3 (0f, -0.05f, 0f), Space.Self);
				} else {

				}
		}
}
