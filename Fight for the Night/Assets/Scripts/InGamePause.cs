using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePause : MonoBehaviour {
    /* Script for In Game Pause Menu
    Start and Update run the panel bool
    Methods are key button presses
         
         */

    //In Game Pause Menu Object
    public GameObject pauseMenu;


	// Use this for initialization
	void Start () {
        pauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        //Get Pause Button (Escape Key) Input
        if (Input.GetKey(KeyCode.M))
        {
                pauseMenu.SetActive(true);
            }

            //Turn Menu Off
        if (Input.GetKey(KeyCode.N))
        {
                pauseMenu.SetActive(false);
            }

        //Turn Menu Off
        if (Input.GetKey(KeyCode.L))
        {
            if (pauseMenu.activeInHierarchy == true)
            {
                SceneManager.LoadScene(0);
            }
        }

        //Turn Menu Off
        if (Input.GetKey(KeyCode.J))
        {
            if (pauseMenu.activeInHierarchy == true)
            {
                Application.Quit();
            }
        }

    }

       
            
	
    /* Commented out Because no Mouse interaction
    //Button Press Run Scripts

    //On Press, switch scene to Main Screen
    public void MainMenuPress()
    {
        
        //Using Scene Manager switch to Main Screen as 0
        SceneManager.LoadScene(0);
    }
    
    /*Not Implemented
    public void OptionsMenuPress()
    {

    }/
    
    //On Press Quit Application (using Unity Quit)
    public void ExitGamePress()
    {
        Application.Quit();
    }*/
}

