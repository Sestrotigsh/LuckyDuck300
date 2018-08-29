using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour {

    public List<Sprite> imageForTutorial = new List<Sprite>();
    public Image canvasImage;
    public GameObject lobbyMan;
    public int imageNumber = 0;

	// Use this for initialization
	void Start () {
        lobbyMan = GameObject.Find("LobbyManager");
        lobbyMan.SetActive(false);
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
            SceneManager.LoadScene("Main Screen", LoadSceneMode.Single);
            lobbyMan.SetActive(true);
        }
        TutorialPics();
    }

    private void TutorialPics()
    {
        canvasImage.sprite = imageForTutorial[imageNumber];
    }
}
