using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TowerAttacking : MonoBehaviour {

	private Transform target;
	public float range = 15.0f;
	public float fireRate = 1.0f;
	public float turnSpeed = 10.0f;
	private float fireCountdown = 0.0f;

	public float Firepower = 800f;
	public GameObject projectile;
	public Transform barrel;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("updateTarget", 0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			return;
		}
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (dir);
		Vector3 rotation = Quaternion.Lerp(transform.rotation,lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		transform.rotation = Quaternion.Euler (0f, rotation.y, 0f);




	
		barrel.transform.LookAt (target);
		if (fireCountdown <= 0f) {
			Shoot ();
			fireCountdown = 1.0f / fireRate;
		}
		fireCountdown -= Time.deltaTime;
		
	}

	void Shoot() {
		GameObject instance = Instantiate (projectile, barrel.position, barrel.rotation) as GameObject;
		instance.GetComponent<Rigidbody> ().AddForce (barrel.forward * Firepower);
		//NetworkServer.Spawn (instance);
		Destroy (instance, 1.0f);
	}

	void updateTarget() {
		GameObject[] enemies1 = GameObject.FindGameObjectsWithTag ("Enemy0");
		GameObject[] enemies2 = GameObject.FindGameObjectsWithTag ("Enemy1");
		GameObject[] enemies = enemies1.Concat (enemies2).ToArray ();
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies) {
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance) {
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}
		if (nearestEnemy != null && shortestDistance < range) {
			target = nearestEnemy.transform;
		} else {
			target = null;
		}

	}
}
