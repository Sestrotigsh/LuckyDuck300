using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class MultiMenu : MonoBehaviour {
///// CONTROLS THE MENU SYSTEM AND HOW MATCHES ARE ADDED 

	public Transform matchesListGrid;	 
	public GameObject mainMenu;
	public GameObject chooseCharacter;
	public GameObject multiplayer;
	public GameObject lobby;
	public GameObject props;
	public GameObject matchUIprefab;
	public InputField gameNameInputField;
	public MatchInfoSnapshot snapshot;
	public static MultiMenu singleton;
	List <GameObject> matchSlots = new List<GameObject>();

	void awake() {
		singleton = this;
	}

	// Use this for initialization
	void Start () {
		MainMenu ();
	}

	// load the main menu
	public void MainMenu() {
		mainMenu.SetActive (true);
		props.SetActive(true);
		chooseCharacter.SetActive (false);
		multiplayer.SetActive (false);
		lobby.SetActive (false);
	}

	/// <summary>
	/// Let player select the character
	/// </summary>
	public void SelectCharacter() {
		mainMenu.SetActive (false);
		props.SetActive(false);
		chooseCharacter.SetActive (true);
		multiplayer.SetActive (false);
		lobby.SetActive (false);
	}

	/// <summary>
	/// Open the networking lobby menu
	/// </summary>
	public void OpenNetworking() {
		mainMenu.SetActive (false);
		chooseCharacter.SetActive (false);
		multiplayer.SetActive (true);
		lobby.SetActive (false);
		NetManager.singl.SearchForMatch ();
	}

	// open the lobby
	public void OpenLobby() {
		mainMenu.SetActive (false);
		chooseCharacter.SetActive (false);
		multiplayer.SetActive (false);
		lobby.SetActive (true);
		props.SetActive(true);
	}

	// create the match
	public void CreateMatch() {
		OpenLobby ();
		NetManager.singl.CreateMatch();
	}

	// join the match
	public void JoinMatch() {
		if (snapshot.currentSize < snapshot.maxSize) {
			OpenLobby();
			//NetManager.singl.TryToJoinMatch(snapshot);
		}
	}

	// update the game name
	public void UpdateGameName(string name) {
		NetManager.singl.currentMatch.matchName = name;
	}

	// return to the main menu
	public void BackToMenu() {
		mainMenu.SetActive (false);
		chooseCharacter.SetActive (false);
		multiplayer.SetActive (true);
		lobby.SetActive (false);
		NetManager.singl.BackToMultiplayerMenu();
	}

	// Add a new match to the match list
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
