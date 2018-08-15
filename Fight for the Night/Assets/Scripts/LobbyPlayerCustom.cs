using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayerCustom : MonoBehaviour {
///// TELLS THE GAME THAT THE PLAYERS ARE READY TO START THE GAME

	// Use this for initialization
	void Start () {
		this.GetComponent<NetworkLobbyPlayer> ().SendReadyToBeginMessage ();
	}

	public void OnClientEnterLobby() {
		this.GetComponent<NetworkLobbyPlayer> ().SendReadyToBeginMessage ();
	}
}
