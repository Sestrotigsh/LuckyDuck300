using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHide : MonoBehaviour {

	public GameObject quitButton;
	public GameObject returnButton;

	// Use this for initialization
	void Start () {
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;
	}
	
	// Update is called once per frame
	void Update () {
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;
		if (sceneName == "Main Scene") {
			//Debug.Log("GAME OPEN");
			quitButton.SetActive(false);
			returnButton.SetActive(false);
			this.enabled = false;
		}
	}
}
