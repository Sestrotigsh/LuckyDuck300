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

	private float centreLineX = 190.0f;

	public float Firepower = 800f;
	public GameObject projectile;
	public Transform barrel;
	private PlayerNetwork player;
	public int team;
    public GameObject playerChar;
    public GameObject upgradeArrow;
    public int displayDistance;
    public float dist;


	// Use this for initialization
	void Start () {
		if (this.transform.position.x < centreLineX) {
			team = 0;
		} else {
			team = 1;
		}
        Vector3 arrowPosition = new Vector3(0, 3, 0);
        playerChar = GameObject.FindGameObjectWithTag("Player" + team);
        upgradeArrow = Instantiate(upgradeArrow, arrowPosition, transform.rotation, transform);
        upgradeArrow.transform.localPosition = arrowPosition;
        

    }
	
	// Update is called once per frame
	void Update () {
        DisplayArrow();
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
		this.GetComponent<AudioSource>().Play();
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

    void DisplayArrow()
    {
        if ((Vector3.Distance(playerChar.transform.position, transform.position) < displayDistance) && (upgradeArrow.activeInHierarchy == false))
        {
            if (this.transform.parent.tag == "BaseTower4" || this.transform.parent.tag == "IceTower4 " || this.transform.parent.tag == "BombTower4")
            {

            } else
            {
                upgradeArrow.SetActive(true);
                Vector3 playerDir = new Vector3(playerChar.transform.position.x, this.transform.position.y, playerChar.transform.position.z);
                upgradeArrow.transform.LookAt(playerDir);
            }
            
        } else if (upgradeArrow.activeInHierarchy == true && (Vector3.Distance(playerChar.transform.position, transform.position) > displayDistance))
        {
            upgradeArrow.SetActive(false);
        }       
    }
}
