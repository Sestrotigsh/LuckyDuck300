using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour {
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
		if (other.CompareTag ("Enemy"+team)) {
			//if (player.local) {
				if (player.health > 0) {
					player.TakeDamage();
				}
				other.tag = "Dying Enemy2";
			//}	
		}
	}
}