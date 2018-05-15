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

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().enabled = false;
		currentlyTouching = null;
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
					Instantiate (towerBasic, transform.position-yAdjust, transform.rotation);
				} else {
					if (currentlyTouching.CompareTag ("TowerBase")) {
						DestroyObject (currentlyTouching);
						Instantiate (tower1, transform.position-yAdjust, transform.rotation);
					} else if (currentlyTouching.CompareTag ("Tower1")) {
						DestroyObject (currentlyTouching);
						Instantiate (tower2, transform.position-yAdjust, transform.rotation);
					} else if (currentlyTouching.CompareTag ("Tower2")) {
						DestroyObject (currentlyTouching);
						Instantiate (tower3, transform.position-yAdjust, transform.rotation);
					}
				}
				GetComponent<Renderer> ().enabled = false;
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		currentlyTouching = other.gameObject;
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == currentlyTouching)
			currentlyTouching = null;
	}
}
