using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TowerButtonUI : NetworkBehaviour {

    // Use this for initialization
    //Panel
    public GameObject subPanel;

    private GameObject player0;
    private GameObject player1;
    public PlayerManagement playerMan;
    private PlayerTower playerBuild;


    void Start()
    {
        player0 = GameObject.FindWithTag("Player0");
        player1 = GameObject.FindWithTag("Player1");

        if (player0 != null) {
            if (player0.GetComponent<NetworkIdentity>().isLocalPlayer) {
                playerMan = player0.GetComponent<PlayerManagement>();
                playerBuild = player0.GetComponent<PlayerTower>();
            } else if (player1 != null) {
                if (player1.GetComponent<NetworkIdentity>().isLocalPlayer) {
                    playerMan = player1.GetComponent<PlayerManagement>();
                    playerBuild = player1.GetComponent<PlayerTower>();
                }
            }
        }
    }

    void Update()
    {
        if (playerMan == null) {
            player0 = GameObject.FindWithTag("Player0");
             player1 = GameObject.FindWithTag("Player1");
            if (player0 != null) {
            if (player0.GetComponent<NetworkIdentity>().isLocalPlayer) {
                playerMan = player0.GetComponent<PlayerManagement>();
                playerBuild = player0.GetComponent<PlayerTower>();
            } else if (player1 != null) {
                if (player1.GetComponent<NetworkIdentity>().isLocalPlayer) {
                    playerMan = player1.GetComponent<PlayerManagement>();
                    playerBuild = player1.GetComponent<PlayerTower>();
                }
            }
        }
        } else {
            if (subPanel.activeSelf == true) {
                 if (Input.GetButtonDown("Fire1")) {
                    if (this.CompareTag("Tower1")) {
                        playerBuild.RecieveDirections(1);
                    } else if (this.CompareTag("Tower2")) {
                        playerBuild.RecieveDirections(2);
                    } else if (this.CompareTag("Tower3")) {
                        playerBuild.RecieveDirections(3);
                    }
                }
            }
        }
    }

    void OnMouseOver()
    {
        subPanel.SetActive(true);
    }

    void OnMouseExit()
    {
        subPanel.SetActive(false);
    }
}
