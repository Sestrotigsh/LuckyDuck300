using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

	public GameObject towerGhost;
	public GameObject towerBasic;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift)) {
			towerGhost.SetActive (true);
		} 
		if (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift)) {
			towerGhost.SetActive (false);
		}
		// if player clicks then instantiate the tower model
		if (towerGhost.activeInHierarchy == true) {
			if (Input.GetButtonDown ("Fire1")) {
				Instantiate (towerBasic, towerGhost.transform.position, towerGhost.transform.rotation);
				towerGhost.SetActive (false);
			}
		}
		
	}
}
