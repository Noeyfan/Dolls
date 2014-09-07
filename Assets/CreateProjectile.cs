using UnityEngine;
using System.Collections;

public class CreateProjectile : MonoBehaviour {

	public Rigidbody projectile;
	void Update() {
		if (Input.GetButtonDown("Fire1")) {
			Rigidbody clone;
			Vector3 ProjectilePos = transform.position;
			ProjectilePos.y += 1;
			ProjectilePos.x += 0.5f;
			clone = Instantiate(projectile, ProjectilePos, transform.rotation) as Rigidbody;
			clone.rigidbody.AddForce(transform.forward * 2000);
			clone.angularVelocity = (new Vector3(Random.Range (0,50), Random.Range (0,100), Random.Range (0,50)));
		}
	}
}
