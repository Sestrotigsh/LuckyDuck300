using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonUI : MonoBehaviour {

    // Use this for initialization
    //Panel
    public GameObject subPanel;

    //Tower Selection Variables
    //material 
    public Material UImaterial;
    public Material highlightmaterial;
    public GameObject towerBasic;


    //Tower Spawn
    public GameObject currentlyTouching;
    private Vector3 yAdjust = new Vector3(0f, 0.4f, 0f);
    public int baseCost = 25;

    private PlayerManagement playerMan;
    public int team;

    void Start()
    {
    }

    void Update()
    {

        // If player clicks on the panel then instantiate the tower model
        if (GetComponent<Renderer>().enabled == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (currentlyTouching == null)
                {
                    if (playerMan.currentGold >= baseCost)
                    {
                        Instantiate(towerBasic, transform.position - yAdjust, transform.rotation);
                        playerMan.currentGold = playerMan.currentGold - baseCost;
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
