using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

	public GameObject towerBasic;
	public GameObject tower1;
	public GameObject tower2;
	public GameObject tower3;
	public GameObject currentlyTouching;
	private Vector3 yAdjust = new Vector3 (0f, 0.4f, 0f);
	public int baseCost = 25;
	public int level2Cost = 50;
	public int level3Cost = 75;
	public int level4Cost = 100;

	private PlayerManagement playerMan;
	public int team;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().enabled = false;
		currentlyTouching = null;
		playerMan = GameObject.FindGameObjectWithTag("Player" + team).GetComponent<PlayerManagement>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift)) {
			GetComponent<Renderer> ().enabled = true;
		} 
		if (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift)) {
			GetComponent<Renderer> ().enabled = false;
		}
		// if player clicks then instantiate the tower model
		if (GetComponent<Renderer> ().enabled == true) {
			if (Input.GetButtonDown ("Fire1")) {
				if (currentlyTouching == null) {
					if (playerMan.currentGold >= baseCost) {
						Instantiate (towerBasic, transform.position-yAdjust, transform.rotation);
						playerMan.currentGold = playerMan.currentGold - baseCost;
					}
				} else {
					if (currentlyTouching.CompareTag ("TowerBase")) {
						if (playerMan.currentGold >= level2Cost) {
							DestroyObject(currentlyTouching);
							Instantiate (tower1, transform.position-yAdjust, transform.rotation);
							playerMan.currentGold = playerMan.currentGold - level2Cost;
						}

					} else if (currentlyTouching.CompareTag ("Tower1")) {
						if (playerMan.currentGold >= level3Cost) {
							DestroyObject (currentlyTouching);
							Instantiate (tower2, transform.position-yAdjust, transform.rotation);
							playerMan.currentGold = playerMan.currentGold - level3Cost;
						}
					} else if (currentlyTouching.CompareTag ("Tower2")) {
						if (playerMan.currentGold >= level4Cost) {
							DestroyObject (currentlyTouching);
							Instantiate (tower3, transform.position-yAdjust, transform.rotation);
							playerMan.currentGold = playerMan.currentGold - level4Cost;
						}
					}
				}
				GetComponent<Renderer> ().enabled = false;
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Projectile") != true) {
			currentlyTouching = other.gameObject;
		}

	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == currentlyTouching)
			currentlyTouching = null;
	}
}
