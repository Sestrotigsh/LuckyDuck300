using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerTower : NetworkBehaviour {
///// CONTROLS PLAYERS TOWER SPAWNING
	
	public GameObject alienGhost;
	public GameObject alienGhostRenderer;
	public GameObject slasherGhost;
	public GameObject ghost;
	public GameObject alienTowerBasic;
	public GameObject alienTower1;
	public GameObject alienTower2;
	public GameObject alienTower3;
	public GameObject slasherTowerBasic;
	public GameObject slasherTower1;
	public GameObject slasherTower2;
	public GameObject slasherTower3;
	public GameObject currentlyTouching;
	public int baseCost = 25;
	public int level2Cost = 50;
	public int level3Cost = 75;
	public int level4Cost = 100;

	private bool isAlien;
	private PlayerNetwork PlayerNet;

	// Use this for initialization
	void Start () {
		ghost = null;

		PlayerNet = this.GetComponent<PlayerNetwork> ();
		if (!PlayerNet.local) {
			enabled = false;
		}

		if (transform.Find("AlienClothes").gameObject.activeSelf == true) {
			ghost = alienGhost;
			isAlien = true;
			ghost.SetActive(true);
			ghost.GetComponent<Renderer> ().enabled = false;
		} else if (transform.Find("SlasherClothes").gameObject.activeSelf == true) {
			ghost = slasherGhost;
			ghost.SetActive(true);
			isAlien = false;
			ghost.GetComponent<Renderer> ().enabled = false;
		}
		
		currentlyTouching = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (ghost == null) {
			if (transform.Find("AlienClothes").gameObject.activeSelf == true) {
				ghost = alienGhost;
				isAlien = true;
				ghost.SetActive(true);
				ghost.GetComponent<Renderer> ().enabled = false;
			} else if (transform.Find("SlasherClothes").gameObject.activeSelf == true) {
				ghost = slasherGhost;
				ghost.SetActive(true);
				isAlien = false;
				ghost.GetComponent<Renderer> ().enabled = false;
			}
		}

		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift)) {
			Debug.Log("SHIFT");
			ghost.GetComponent<Renderer> ().enabled = true;
		} 

		if (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift)) {
			ghost.GetComponent<Renderer> ().enabled = false;
		}

		// if player clicks then instantiate the tower model
		if (ghost.GetComponent<Renderer> ().enabled == true) {
			if (Input.GetButtonDown ("Fire1")) {
				if (currentlyTouching == null) {
					if (this.GetComponent<PlayerManagement> ().currentGold >= baseCost) {
						if (isAlien == true) {
							CmdcreateTower1Alien (ghost.transform.position, ghost.transform.rotation);
						} else {
							CmdcreateTower1Slasher (ghost.transform.position, ghost.transform.rotation);
						}
						this.GetComponent<PlayerManagement> ().currentGold -= baseCost;
					}
				} else {
					if (currentlyTouching.CompareTag ("TowerBase")) {
						if (this.GetComponent<PlayerManagement> ().currentGold >= level2Cost) {
							Destroy(currentlyTouching);
							if (isAlien == true) {
								CmdcreateTower2Alien (ghost.transform.position, ghost.transform.rotation);
							} else {
								CmdcreateTower2Slasher (ghost.transform.position, ghost.transform.rotation);
							}
							this.GetComponent<PlayerManagement> ().currentGold -= level2Cost;
						}
					} else if (currentlyTouching.CompareTag ("Tower1")) {
						if (this.GetComponent<PlayerManagement> ().currentGold >= level3Cost) {
							Destroy(currentlyTouching);
							if (isAlien == true) {
								CmdcreateTower3Alien (ghost.transform.position, ghost.transform.rotation);
							} else {
								CmdcreateTower3Slasher (ghost.transform.position, ghost.transform.rotation);
							}
							this.GetComponent<PlayerManagement> ().currentGold -= level3Cost;
						}
					} else if (currentlyTouching.CompareTag ("Tower2")) {
						if (this.GetComponent<PlayerManagement> ().currentGold >= level4Cost) {
							Destroy(currentlyTouching);
							if (isAlien == true) {
								CmdcreateTower4Alien (ghost.transform.position, ghost.transform.rotation);
							} else {
								CmdcreateTower4Slasher (ghost.transform.position, ghost.transform.rotation);
							}
							this.GetComponent<PlayerManagement> ().currentGold -= level4Cost;
						}
					}
				}
				ghost.GetComponent<Renderer> ().enabled = false;
			}
		}
	}

	[Command]
	void CmdcreateTower1Alien(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (alienTowerBasic, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower2Alien(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (alienTower1, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower3Alien(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (alienTower2, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower4Alien(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (alienTower3, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

		[Command]
	void CmdcreateTower1Slasher(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (slasherTowerBasic, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower2Slasher(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (slasherTower1, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower3Slasher(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (slasherTower2, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower4Slasher(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (slasherTower3, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
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