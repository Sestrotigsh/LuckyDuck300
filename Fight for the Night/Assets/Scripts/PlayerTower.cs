using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerTower : NetworkBehaviour {

	public GameObject ghost;
	public GameObject towerBasic;
	public GameObject tower1;
	public GameObject tower2;
	public GameObject tower3;
	public GameObject currentlyTouching;
	public int baseCost = 25;
	public int level2Cost = 50;
	public int level3Cost = 75;
	public int level4Cost = 100;

	// Use this for initialization
	void Start () {
		ghost.GetComponent<Renderer> ().enabled = false;
		currentlyTouching = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift)) {
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
						CmdcreateTower1 (ghost.transform.position, ghost.transform.rotation);
						this.GetComponent<PlayerManagement> ().currentGold -= baseCost;
					}
				} else {
					if (currentlyTouching.CompareTag ("TowerBase")) {
						if (this.GetComponent<PlayerManagement> ().currentGold >= level2Cost) {
							Destroy(currentlyTouching);
							CmdcreateTower2 (ghost.transform.position, ghost.transform.rotation);
							this.GetComponent<PlayerManagement> ().currentGold -= level2Cost;
						}
					} else if (currentlyTouching.CompareTag ("Tower1")) {
						if (this.GetComponent<PlayerManagement> ().currentGold >= level3Cost) {
							Destroy(currentlyTouching);
							CmdcreateTower3 (ghost.transform.position, ghost.transform.rotation);
							this.GetComponent<PlayerManagement> ().currentGold -= level3Cost;
						}
					} else if (currentlyTouching.CompareTag ("Tower2")) {
						if (this.GetComponent<PlayerManagement> ().currentGold >= level4Cost) {
							Destroy(currentlyTouching);
							CmdcreateTower4 (ghost.transform.position, ghost.transform.rotation);
							this.GetComponent<PlayerManagement> ().currentGold -= level4Cost;
						}
					}
				}
				ghost.GetComponent<Renderer> ().enabled = false;
			}
		}
	}

	[Command]
	void CmdcreateTower1(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (towerBasic, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower2(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (tower1, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower3(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (tower2, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower4(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (tower3, position, rotation) as GameObject;
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