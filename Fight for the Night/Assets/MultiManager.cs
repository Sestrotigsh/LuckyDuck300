using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class MultiManager : NetworkLobbyManager {

	public GameObject mainMenu;
	public GameObject chooseCharacter;
	public GameObject multiplayer;
	public GameObject lobby;
	public GameObject Join;
	public InputField gameNameInputField;

	public void SearchForMatch() {
		StartMatchMaker ();
		matchMaker.ListMatches(0,20,"",true,0,0,ReturnMatch);
	}

	public void ReturnMatch (bool success, string extendedInfo, List <MatchInfoSnapshot> matches) {
		for (int i = 0; i < matches.Count; i++) {
			Join.SetActive (true);
			// ADJUST THE TEXT TO DISPLAY THE CREATED MATCH
		}
	}

	public void CreateMatch() {
//		currentMatch = new MatchSettings ();
//		currentMatch.matchName = "NewGame";
		StartMatchMaker ();
		string data = gameNameInputField.text;
		matchMaker.CreateMatch (data, (uint)2, true, "", "", "", 0, 0, MatchCreated);
	}

	void MatchCreated(bool success, string extendedInfo, MatchInfo matchInfo) {
		if (success) {
			OpenLobby ();
			NetworkServer.Listen (matchInfo, 9000);
			StartHost (matchInfo);
			GetComponent<NetworkLobbyPlayer> ().SendReadyToBeginMessage ();
			// host = true;
		}
	}

	public void OpenLobby() {
		mainMenu.SetActive (false);
		chooseCharacter.SetActive (false);
		multiplayer.SetActive (false);
		lobby.SetActive (true);
	}




}
