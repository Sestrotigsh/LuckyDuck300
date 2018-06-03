using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MainMenu() {
		SceneManager.LoadScene("Main Screen", LoadSceneMode.Single);
	}

    public void GoToGame()
    {
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
