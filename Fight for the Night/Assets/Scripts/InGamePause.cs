using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class InGamePause : NetworkBehaviour {
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
        if (!isLocalPlayer) {
            this.enabled = false;
        }
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
                GameObject opponent = this.GetComponent<PlayerNetwork>().opponent;
                // If the player is on the server - tell the client to win
                if (isServer) {
                    if (opponent.transform.Find("AlienClothes").gameObject.activeSelf == true) {
                        RpcVictoryAlien();
                    } else if (opponent.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
                        RpcVictorySlasher();
                    }
            // if the player is a client - tell the server to win
                } else {
                    if (opponent.transform.Find("AlienClothes").gameObject.activeSelf == true) {
                        CmdVictoryAlien();
                    } else if (opponent.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
                        CmdVictorySlasher();
                    }
                }
                // end in defeat
                if (opponent.transform.Find("AlienClothes").gameObject.activeSelf == true) {
                    SceneManager.LoadScene("DefeatAlien", LoadSceneMode.Single);
                } else if (opponent.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
                    SceneManager.LoadScene("DefeatSlasher", LoadSceneMode.Single);
                }
                //SceneManager.LoadScene(0);

                //SceneManager.LoadScene(0);
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


    [Command]
    void CmdVictoryAlien () {
        // Tell the opponent on the server they have won!
        SceneManager.LoadScene("VictoryAlien", LoadSceneMode.Single);
    }

    [Command]
    void CmdVictorySlasher () {
        // Tell the opponent on the server they have won!
        SceneManager.LoadScene("VictorySlasher", LoadSceneMode.Single);
    }

    [ClientRpc]
    void RpcVictoryAlien() {
        // tell the client (excluding client on the server) they have won!
        if (!isServer) {
            SceneManager.LoadScene("VictoryAlien", LoadSceneMode.Single);
        }
    }

    [ClientRpc]
    void RpcVictorySlasher() {
        // tell the client (excluding client on the server) they have won!
        if (!isServer) {
            SceneManager.LoadScene("VictorySlasher", LoadSceneMode.Single);
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

