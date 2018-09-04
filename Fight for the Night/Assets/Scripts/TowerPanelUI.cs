using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPanelUI : MonoBehaviour
{

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

    // Use this for initialization
    void Start()
    {
        UImaterial = GetComponent<Renderer>().material;
        GetComponent<Renderer>().enabled = false;
        currentlyTouching = null;
        playerMan = GameObject.FindGameObjectWithTag("Player" + team).GetComponent<PlayerManagement>();

    }

    // Update is called once per frame
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

    //Colour Change
    void OnMouseOver()
    {
        //Change to highlight color
        Material tempmaterial = UImaterial;
        UImaterial = highlightmaterial;
    }

}


 