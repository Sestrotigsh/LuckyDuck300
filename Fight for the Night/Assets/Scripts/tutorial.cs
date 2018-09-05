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
        if (Input.GetKeyDown("d") && imageNumber < imageForTutorial.Count-1)
        {
            imageNumber += 1;
            
        } else if (Input.GetKeyDown("a") && imageNumber > 0)
        {
            imageNumber -= 1;
            
        } else if (Input.GetKeyDown("d") && imageNumber == imageForTutorial.Count-1)
        {
            title.SetActive(true);
            menu.SetActive(true);
            LobbyManager.GetComponent<NetworkManager>().enabled = true;
            SceneManager.LoadScene("Main Screen", LoadSceneMode.Single);
        }
        TutorialPics();
    }

    private void TutorialPics()
    {
        canvasImage.sprite = imageForTutorial[imageNumber];
    }
}
