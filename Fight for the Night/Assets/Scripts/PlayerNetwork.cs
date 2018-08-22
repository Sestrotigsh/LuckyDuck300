﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerNetwork : NetworkBehaviour {
////// CONTROLS THE PLAYERS NETWORKING VARIABLES

	public float centreLineX;
	public int team;
	public bool local;
	public int health;
	public GameObject opponent;
	public bool opponentFound;

	// Use this for initialization
	void Start () {
		// Check which side of the map the player is on and then set which team they are on
		if (this.transform.position.x < centreLineX) {
			team = 0;
		} else {
			team = 1;
		}

		tag = ("Player" + team);
		opponent = GameObject.FindGameObjectWithTag ("Player" + (team + 1) % 2);
		//if (opponent != null) {
			//if (opponent.transform.Find("AlienClothes").gameObject.activeSelf == true) {
				//this.gameObject.GetComponent<EnemyWavesOffline>().enabled = false;
				//this.gameObject.GetComponent<EnemyWavesAlien>().enabled = true;
			//} else if (opponent.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
				//this.gameObject.GetComponent<EnemyWavesSlasher>().enabled = true;
				//this.gameObject.GetComponent<EnemyWavesOffline>().enabled = false;
			//}
		//}
		// Check and store if the player is an actual player or just transform data from a player on another machine
		if (isLocalPlayer) {
			local = true;
		} else {
			local = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (opponent == null) {
			opponent = GameObject.FindGameObjectWithTag ("Player" + (team + 1) % 2);
			//if (opponent != null) {
				//if (opponent.transform.Find("AlienClothes").gameObject.activeSelf == true) {
					//this.gameObject.GetComponent<EnemyWavesOffline>().enabled = false;
					//this.gameObject.GetComponent<EnemyWavesAlien>().enabled = true;
				//} else if (opponent.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
					//this.gameObject.GetComponent<EnemyWavesSlasher>().enabled = true;
					//this.gameObject.GetComponent<EnemyWavesOffline>().enabled = false;
				//}
			//}
			
		}
	}

	public void TakeDamage () {
		health -= 1;
		if (isServer) {
			RpcUpdateHealth (health);
		} else {
			CmdUpdateHealth (health);
		}
		if (health <= 0) {
			// If the player is on the server - tell the client to win
			if (isServer) {
				RpcVictory();
			// if the player is a client - tell the server to win
			} else {
				CmdVictory();
			}
			// end in defeat
			SceneManager.LoadScene("Defeat", LoadSceneMode.Single);
		}
	}

	public void EnemyDie(GameObject enemy) {
		if (isServer) {
			RpcKillEnemy(enemy);
		} else {
			CmdKillEnemy(enemy);
		}
		Destroy (enemy);
	}

	[Command]
	void CmdKillEnemy (GameObject target) {
		Destroy (target);
	}
	[ClientRpc]
	void RpcKillEnemy(GameObject target) {
		Destroy (target);
	}

	[Command]
	void CmdUpdateHealth(int amount) {
		health = amount;
	}

	[ClientRpc]
	void RpcUpdateHealth(int amount) {
		health = amount;
	}

	[Command]
	void CmdVictory () {
		// Tell the opponent on the server they have won!
		SceneManager.LoadScene("Victory", LoadSceneMode.Single);
	}
	[ClientRpc]
	void RpcVictory() {
		// tell the client (excluding client on the server) they have won!
		if (!isServer) {
			SceneManager.LoadScene("Victory", LoadSceneMode.Single);
		}
	}
}
