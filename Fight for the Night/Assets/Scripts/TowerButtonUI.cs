using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class TowerButtonUI : NetworkBehaviour {

    // Use this for initialization
    //Panel
    public GameObject subPanel;

    private GameObject player0;
    private GameObject player1;
    public PlayerManagement playerMan;
    private PlayerTower playerBuild;
    private Camera mainCamera;
    public TextMesh subPanelText;

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

        subPanelText = subPanel.transform.Find("Text").GetComponent<TextMesh>();
        
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
                    if (this.CompareTag("TowerBase")) {
                        playerBuild.ReceiveDirections(1);
                    } else if (this.CompareTag("TowerBomb")) {
                        playerBuild.ReceiveDirections(2);
                    } else if (this.CompareTag("TowerIce")) {
                        playerBuild.ReceiveDirections(3);
                    }
                }
            }
        }

        towerDetectionType();
    }

    private void towerDetectionType()
    {
        GameObject typeOfTower = playerBuild.currentlyTouching;

        if (this.tag == "TowerBase")
        {
            subPanelText.text = "Base Tower 1" + Environment.NewLine + "Cost :" + playerBuild.baseCost;
        } else if (this.tag == "TowerIce")
        {
            subPanelText.text = "Ice Tower 1" + Environment.NewLine + "Cost :" + playerBuild.iceCost;
        } else if (this.tag == "TowerBomb")
        {
            subPanelText.text = "Bomb Tower 1" + Environment.NewLine + "Cost :" + playerBuild.bombCost;
        }

        if (typeOfTower != null)
        {
            if (this.tag == "TowerBase")
            {
                if (typeOfTower.tag == "BaseTower1")
                {
                    subPanelText.text = "Base Tower 2" + Environment.NewLine + "Cost :" + playerBuild.baselevel2Cost;

                }
                else if (typeOfTower.tag == "BaseTower2")
                {
                    subPanelText.text = "Base Tower 3" + Environment.NewLine + "Cost :" + playerBuild.baselevel3Cost;

                }
                else if (typeOfTower.tag == "BaseTower3")
                {
                    subPanelText.text = "Base Tower 4" + Environment.NewLine + "Cost :" + playerBuild.baselevel4Cost;

                }

                else if (typeOfTower.tag == "BaseTower4" || typeOfTower.tag == "IceTower1" || typeOfTower.tag == "IceTower2" || typeOfTower.tag == "IceTower3" || typeOfTower.tag == "IceTower4" || typeOfTower.tag == "BombTower1" || typeOfTower.tag == "BombTower2" || typeOfTower.tag == "BombTower3" || typeOfTower.tag == "BombTower4")
                {
                    this.gameObject.SetActive(false);
                    
                }            
            }
            else if (this.tag == "TowerIce")
            {
                if (typeOfTower.tag == "IceTower1")
                {
                    subPanelText.text = "Ice Tower 2" + Environment.NewLine + "Cost :" + playerBuild.icelevel2Cost;
                }
                else if (typeOfTower.tag == "IceTower2")
                {
                    subPanelText.text = "Ice Tower 3" + Environment.NewLine + "Cost :" + playerBuild.icelevel3Cost;
                }
                else if (typeOfTower.tag == "IceTower3")
                {
                    subPanelText.text = "Ice Tower 4" + Environment.NewLine + "Cost :" + playerBuild.icelevel4Cost;
                }

                else if (typeOfTower.tag == "BaseTower1" || typeOfTower.tag == "BaseTower2" || typeOfTower.tag == "BaseTower3" || typeOfTower.tag == "BaseTower4" || typeOfTower.tag == "IceTower4" || typeOfTower.tag == "BombTower1" || typeOfTower.tag == "BombTower2" || typeOfTower.tag == "BombTower3" || typeOfTower.tag == "BombTower4")
                {
                    this.gameObject.SetActive(false);
                }           
            }

            else if (this.tag == "TowerBomb")
            {
                if (typeOfTower.tag == "BombTower1")
                {
                    subPanelText.text = "Bomb Tower 2" + Environment.NewLine + "Cost :" + playerBuild.bomblevel2Cost;
                }
                else if (typeOfTower.tag == "BombTower2")
                {
                    subPanelText.text = "Bomb Tower 3" + Environment.NewLine + "Cost :" + playerBuild.bomblevel3Cost;
                }
                else if (typeOfTower.tag == "BombTower3")
                {
                    subPanelText.text = "Bomb Tower 4" + Environment.NewLine + "Cost :" + playerBuild.bomblevel4Cost;
                }

                else if (typeOfTower.tag == "BaseTower1" || typeOfTower.tag == "BaseTower2" || typeOfTower.tag == "BaseTower3" || typeOfTower.tag == "BaseTower4" || typeOfTower.tag == "IceTower1" || typeOfTower.tag == "IceTower2" || typeOfTower.tag == "IceTower3" || typeOfTower.tag == "IceTower4" || typeOfTower.tag == "BombTower4")
                {
                    this.gameObject.SetActive(false);
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
