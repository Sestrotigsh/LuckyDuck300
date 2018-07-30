using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class NetManager : NetworkLobbyManager {

	public static NetManager singl;
	public MatchSettings currentMatch;
	public GameObject mainMenu;
	public GameObject chooseCharacter;
	public GameObject multiplayer;
	public GameObject lobby;
	public Transform matchesListGrid;
	public GameObject matchUIprefab;
	public GameObject joinButton;
	public Text matchNameCH;
	public InputField gameNameInputField;

	MatchInfoSnapshot joinMatchCH;

	List <GameObject> matchSlots = new List<GameObject>();


	void Awake() {
		singl = this;
	}

//	public void InitMatch() {
//		currentMatch = new MatchSettings ();
//		currentMatch.matchName = "NewGame";
//	}

	public void SearchForMatch() {
		StartMatchMaker ();
		matchMaker.ListMatches(0,20,"",true,0,0,ReturnMatch);
	}

	public void ReturnMatch (bool success, string extendedInfo, List <MatchInfoSnapshot> matches) {
		for (int i = 0; i < matches.Count; i++) {
			// DO SOMETHING HERE TO SHOW THE CURRENT MATCHES
			joinButton.SetActive(true);
			matchNameCH.text = matches [i].name;
			joinMatchCH = matches [i];
			//AddMatchSlot (matches [i]);
		}
	}

	public void CreateMatch() {
		currentMatch = new MatchSettings ();
		currentMatch.matchName = gameNameInputField.text;
		StartMatchMaker ();
		string data = currentMatch.matchName;
		matchMaker.CreateMatch (data, (uint)2, true, "", "", "", 0, 0, MatchCreated);
	}


	void MatchCreated(bool success, string extendedInfo, MatchInfo matchInfo) {
		if (success) {
			OpenLobby ();
			NetworkServer.Listen (matchInfo, 9000);
			StartHost (matchInfo);
			// host = true;
		}
	}

	public void TryToJoinMatch() {
		matchMaker.JoinMatch (joinMatchCH.networkId, "", "", "", 0, 0, MatchJoin);
	}

	void MatchJoin(bool success, string extendedInfo, MatchInfo matchInfo) {
		if (success) {
			StartClient (matchInfo);
			OpenLobby ();
			// Host = false;
		}
	}

	public void BackToMultiplayerMenu() {
		StopClient ();
		StopHost ();
		StopMatchMaker ();
	}

	public void OpenLobby() {
		mainMenu.SetActive (false);
		chooseCharacter.SetActive (false);
		multiplayer.SetActive (false);
		lobby.SetActive (true);
	}

	public void AddMatchSlot(MatchInfoSnapshot mi) {
		GameObject go = Instantiate (matchUIprefab) as GameObject;
		go.transform.SetParent (matchesListGrid);
		MatchesUI u = go.GetComponent<MatchesUI> ();
		string mName = "";
		u.matchName.text = mi.name;
		if (u.matchName.text == "") {
			u.matchName.text = "NewMatch";
		}
		u.snapshot = mi;
		matchSlots.Add (go);
	}


}




public class MatchSettings {
	public string matchName;
	// OTHER STUFF ADDED HERE 2:03 IF NEEDED
}