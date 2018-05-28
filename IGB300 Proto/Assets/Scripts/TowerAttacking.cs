using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttacking : MonoBehaviour {

	private Transform target;
	public float range = 15.0f;
	public float fireRate = 1.0f;
	public float turnSpeed = 10.0f;
	private float fireCountdown = 0.0f;

	public float Firepower = 800f;
	public GameObject projectile;
	public Transform barrel;
	private PlayerNetwork player;

	// Use this for initialization
	void Start () {
		// DIRTY HARD CODE - SHOULD BE FIXED IN FINAL BUT GOOD ENOUGH FOR PROTOTYPE
		player = GameObject.FindGameObjectWithTag ("Player" + 0).GetComponent<PlayerNetwork>();
		if (player.local) {
			// CHECK IF THEY'RE ON THE CORRECT SIDE (player 0 is  < centre, player 1 is > centre)
			if (transform.position.z < player.centreLineZ) {
				InvokeRepeating ("updateTarget", 0f, 0.5f);
			} else {
				enabled = false;
			}
		} else {
			player = GameObject.FindGameObjectWithTag ("Player" + 1).GetComponent<PlayerNetwork>();
			if (player.local) {
				if (transform.position.z > player.centreLineZ) {
					InvokeRepeating ("updateTarget", 0f, 0.5f);
				} else {
					enabled = false;
				}
			}
		}
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
		Destroy (instance, 1.0f);
	}

	void updateTarget() {

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy"+player.team);
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
