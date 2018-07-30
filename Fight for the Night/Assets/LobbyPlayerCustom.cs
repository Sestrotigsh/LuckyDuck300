using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayerCustom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<NetworkLobbyPlayer> ().SendReadyToBeginMessage ();
	}

	public void OnClientEnterLobby() {
		this.GetComponent<NetworkLobbyPlayer> ().SendReadyToBeginMessage ();
	}
}
