using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class tutorial : NetworkBehaviour {

    public List<Sprite> imageForTutorial = new List<Sprite>();
    public Image canvasImage;
    public GameObject lobbyMan;
    public int imageNumber = 0;
    public GameObject LobbyManager;

    public GameObject menu;
    public GameObject title;
    public GameObject leftArrow;
    public GameObject rightArrow;

	// Use this for initialization
	void Start () {
        var fooGroup = Resources.FindObjectsOfTypeAll(typeof(GameObject));
                     foreach(GameObject t in fooGroup){
                        if(t.name == "StartOptions"){
                            menu = t;
                        }
                        if (t.name == "Title") {
                            title = t;
                        }
                    }

        LobbyManager = GameObject.FindWithTag("Lobby");

        //lobbyMan = GameObject.Find("LobbyManager");
        //lobbyMan.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow) && imageNumber < imageForTutorial.Count-1)
        {
            
            TutorialPics(1);
            
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && imageNumber > 0)
        {
            TutorialPics(-1);
        } 
    }

    public void TutorialPics(int number)
    {
        if (number == 1) {
            if (imageNumber == imageForTutorial.Count-2) {
                rightArrow.SetActive(false);
            } else if (imageNumber == 0) {
                leftArrow.SetActive(true);
            }
        } else if (number == -1) {
            if (imageNumber == 1) {
                leftArrow.SetActive(false);
            } else if (imageNumber == imageForTutorial.Count-1) {
                rightArrow.SetActive(true);
            }
        }
        
        canvasImage.sprite = imageForTutorial[imageNumber + number];
        imageNumber += number;
    }




    public void ReturnToMenu() {
        title.SetActive(true);
        menu.SetActive(true);
        LobbyManager.GetComponent<NetworkManager>().enabled = true;
        SceneManager.LoadScene("Main Screen", LoadSceneMode.Single);
    }


}
