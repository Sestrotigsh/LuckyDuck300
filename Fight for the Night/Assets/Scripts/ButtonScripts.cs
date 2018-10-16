using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ButtonScripts : NetworkBehaviour {
///// CONTROL UI BUTTONS

	public GameObject optionsPanel;
	public GameObject startMenu;
	public GameObject controls;
	public GameObject sounds;
    public GameObject LobbyManager;
    private GameObject mainPanel;
    private GameObject title;
    private GameObject returnButton;
    private GameObject quitButton;
    public GameObject graphicsSlider;


    public void EnterLobbby() {
        var fooGroup = Resources.FindObjectsOfTypeAll(typeof(GameObject));
                     foreach(GameObject t in fooGroup){
                        if(t.name == "MainPanel"){
                            mainPanel = t;
                        }
                    }
        mainPanel.SetActive(false);
    }

    public void EnableReturnButton() {
        var fooGroup = Resources.FindObjectsOfTypeAll(typeof(GameObject));
                     foreach(GameObject t in fooGroup){
                        if(t.name == "BackButton"){
                            returnButton = t;
                        }
                    }
        returnButton.SetActive(true);
    }



	/// <summary>
	/// Load the main menu of the game.
	/// </summary>
	public void MainMenu() {
        Destroy(LobbyManager);
        NetworkLobbyManager.Shutdown();
        SceneManager.LoadScene("Main Screen", LoadSceneMode.Single);
        /*
        if (isServer) {
            NetworkLobbyManager.singleton.StopHost();
        } else {
            NetworkLobbyManager.singleton.StopClient();
        }
        
		var fooGroup = Resources.FindObjectsOfTypeAll(typeof(GameObject));
                     foreach(GameObject t in fooGroup){
                        if(t.name == "StartOptions"){
                            startMenu = t;
                        }
                        if (t.name == "OptionsPanel") {
                            optionsPanel = t;
                        }
                        if (t.name == "Title") {
                            title = t;
                        }
                        if (t.name == "BackButton") {
                            returnButton = t;
                        }
                        if (t.name == "ButtonQuit") {
                            quitButton = t;
                        }
                    }
        title.SetActive(true);
        returnButton.SetActive(false);
        quitButton.SetActive(false);
        SceneManager.LoadScene("Main Screen", LoadSceneMode.Single);
        Menu();
        */

	}

	/// <summary>
	/// loads the game map
	/// </summary>
    public void GoToGame() {
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }

	/// <summary>
	/// Quit the game
	/// </summary>
    public void Exit() {
        Application.Quit();
    }

    public void Options() {
    	startMenu.SetActive(false);
    	optionsPanel.SetActive(true);
        graphicsSlider.SetActive(true);
    }

    public void Menu() {
    	startMenu.SetActive(true);
    	optionsPanel.SetActive(false);
        graphicsSlider.SetActive(false);
    }

    public void Tutorial()
    {
        optionsPanel.SetActive(false);
        startMenu.SetActive(false);
        LobbyManager.GetComponent<NetworkManager>().enabled = false;
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void Controls() {
    controls.SetActive(true);
    sounds.SetActive(false);
    }

    public void Sounds() {
    controls.SetActive(false);
    sounds.SetActive(true);
    }


}
