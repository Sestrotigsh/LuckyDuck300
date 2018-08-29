using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttacking : MonoBehaviour {
///// TOWERS SHOOTING WHEN ON CORRECT SIDE
	
	private Transform target;
	public float range = 15.0f;
	public float fireRate = 1.0f;
	public float turnSpeed = 10.0f;
	private float fireCountdown = 0.0f;

	public float Firepower = 800f;
	public GameObject projectile;
	public Transform barrel;
	private PlayerNetwork player;
	public int team;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		UpdateTarget();
		if (target == null) {
			return;
		}
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (dir);
		Vector3 rotation = Quaternion.Lerp(transform.rotation,lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		transform.rotation = Quaternion.Euler (0f, rotation.y, 0f);
		barrel.transform.LookAt (target);
		if (fireCountdown <= Time.timeSinceLevelLoad) {
			Shoot ();
			fireCountdown = Time.timeSinceLevelLoad + fireRate;
		}		
	}

	void Shoot() {
		GameObject instance = Instantiate (projectile, barrel.position, barrel.rotation) as GameObject;
		instance.GetComponent<ProjectileController>().team = team;
		instance.GetComponent<Rigidbody> ().AddForce (barrel.forward * Firepower);
		Destroy(instance, 2.0f);
	}

	void UpdateTarget() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy"+team);
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
