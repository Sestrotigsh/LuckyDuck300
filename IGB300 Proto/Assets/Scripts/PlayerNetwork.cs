﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetwork : NetworkBehaviour {

	public float centreLineZ;
	public int team;
	public bool local;
	public int health;

	// Use this for initialization
	void Start () {
		// Check which side of the map the player is on and then set which team they are on
		if (this.transform.position.z < centreLineZ) {
			team = 0;
		} else {
			team = 1;
		}
		tag = ("Player" + team);
		// Check and store if the player is an actual player or just transform data from a player on another machine
		if (isLocalPlayer) {
			local = true;
		} else {
			local = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TakeDamage () {
		health -= 1;
		if (isServer) {
			RpcUpdateHealth (health);
		} else {
			CmdUpdateHealth (health);
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
}
