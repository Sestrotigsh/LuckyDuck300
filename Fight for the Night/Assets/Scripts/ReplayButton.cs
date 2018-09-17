using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : MonoBehaviour {

	private GameObject quitButton;
	private GameObject backButton;

	public void EnterGame () {
		var fooGroup = Resources.FindObjectsOfTypeAll(typeof(GameObject));
                     foreach(GameObject t in fooGroup){
                        if(t.name == "BackButton"){
                            backButton = t;
                        }
                        if (t.name == "ButtonQuit") {
                        	quitButton = t;	
                        }
                    }
       backButton.SetActive(false);
       quitButton.SetActive(false);
	}

}
