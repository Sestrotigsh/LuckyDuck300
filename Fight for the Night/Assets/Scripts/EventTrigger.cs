using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EventTrigger : NetworkBehaviour {
///// CONTROLS PLAYERS BASE OBJECTS

	// The bases health values and indicators
	public TextMesh textAbove;
	public PlayerNetwork player;
	public int team;

	// Use this for initialization
	void Start() {
		player = null;
		// find the player
		if (GameObject.FindGameObjectWithTag ("Player" + team) != null) {
			player = GameObject.FindGameObjectWithTag ("Player" + team).GetComponent<PlayerNetwork> ();
			textAbove.text = "" + player.health;
		}
	}

	// Update is called once per frame
	void Update() {
		// if player not yet found - find the player
		if (player == null) {
			if (GameObject.FindGameObjectWithTag ("Player" + team) != null) {
				player = GameObject.FindGameObjectWithTag ("Player" + team).GetComponent<PlayerNetwork> ();
			}
		} else {
			textAbove.text = "" + player.health;
		}
	}

	void OnTriggerEnter(Collider other) {
		// check if enemy enters base
		if (player.local) {
			if (other.CompareTag ("Enemy")) {
				if (player.health > 0) {
					player.TakeDamage();
				}
			}
		} 
        if (other.CompareTag ("Enemy")) {
        	other.tag = "Dying Enemy2";
        }
	}
}