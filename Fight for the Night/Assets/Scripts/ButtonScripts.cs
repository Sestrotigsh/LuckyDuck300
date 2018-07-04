using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour {

	/// <summary>
	/// Load the main menu of the game.
	/// </summary>
	public void MainMenu() {
		SceneManager.LoadScene("Main Screen", LoadSceneMode.Single);
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

}
