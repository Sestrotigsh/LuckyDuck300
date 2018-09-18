using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : MonoBehaviour {

	private GameObject quitButton;
	private GameObject backButton;
    private GameObject LobbyPanel;
    private GameObject serverList;

	public void EnterGame () {
		var fooGroup = Resources.FindObjectsOfTypeAll(typeof(GameObject));
                     foreach(GameObject t in fooGroup){
                        if(t.name == "BackButton"){
                            backButton = t;
                        }
                        if (t.name == "ButtonQuit") {
                        	quitButton = t;	
                        }
                        if (t.name == "ServerListPanel") {
                            serverList = t;
                        }
                        if (t.name == "LobbyPanel") {
                            LobbyPanel = t;
                        }
                    }
       backButton.SetActive(false);
       quitButton.SetActive(false);
       LobbyPanel.SetActive(true);
       serverList.SetActive(false);
	}

}
