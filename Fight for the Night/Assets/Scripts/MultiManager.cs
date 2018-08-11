using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class MultiManager : NetworkLobbyManager {
///// CONTROLS ALL THE LOBBY SETTINGS
	
	public GameObject mainMenu; // main menu
	public GameObject chooseCharacter; // choose character menu
	public GameObject multiplayer; // multiplayer menu
	public GameObject lobby; // lobby menu
	public GameObject Join; // join menu
	public InputField gameNameInputField; // new game text field

	// look for available matches
	public void SearchForMatch() {
		StartMatchMaker ();
		matchMaker.ListMatches(0,20,"",true,0,0,ReturnMatch);
	}

	// take SearchForMatch data and visualise it
	public void ReturnMatch (bool success, string extendedInfo, List <MatchInfoSnapshot> matches) {
		for (int i = 0; i < matches.Count; i++) {
			Join.SetActive (true);
		}
	}

	// Create a new match
	public void CreateMatch() {
		StartMatchMaker ();
		string data = gameNameInputField.text;
		matchMaker.CreateMatch (data, (uint)2, true, "", "", "", 0, 0, MatchCreated);
	}

	// take created match data and make relevant controls
	void MatchCreated(bool success, string extendedInfo, MatchInfo matchInfo) {
		if (success) {
			OpenLobby ();
			NetworkServer.Listen (matchInfo, 9000);
			StartHost (matchInfo);
			GetComponent<NetworkLobbyPlayer> ().SendReadyToBeginMessage ();
			// host = true;
		}
	}

	// reset the menu to fit the lobby when it is activated
	public void OpenLobby() {
		mainMenu.SetActive (false);
		chooseCharacter.SetActive (false);
		multiplayer.SetActive (false);
		lobby.SetActive (true);
	}
}