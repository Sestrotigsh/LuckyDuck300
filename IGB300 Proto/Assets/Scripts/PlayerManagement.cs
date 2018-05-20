using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManagement : MonoBehaviour {


    private static int playerTeam = 0; // Keep trask of which team both players are

    private int thisPlayerTeam; // The current team of this player

    private SpawnMinions spawner; // Script associated with this team spanwer
    private EventTrigger baseHP; // cript associated with this team base HP
    private GameObject canv; // The canvas and TYext attached with this player
    private GameObject canvHPT;
    private GameObject goldT;
    private GameObject incomeT;

    public int startingGold = 50;
    public int currentIncome;
    public int currentGold;
    public int incomeInterval = 5;
    private int timeCount;
	// Use this for initialization
	void Start () {


        // Manage the team counting

        thisPlayerTeam = playerTeam;
        currentGold = currentGold + startingGold;

        playerTeam = playerTeam + 1;
        if (playerTeam > 1)
        {
            playerTeam = 1;
        }

        this.tag = "Player" + thisPlayerTeam; // Set the team of the player.

        // Find all the corresponding game objects

        spawner = GameObject.FindGameObjectWithTag("Spawn"+thisPlayerTeam).GetComponent<SpawnMinions>();
        baseHP = GameObject.FindGameObjectWithTag("Base" + thisPlayerTeam).GetComponent<EventTrigger>();
        canv = this.transform.Find("Canvas").gameObject;

        // Set the income starting time

        timeCount = spawner.initialSpawn;
        spawner.GetPlayer();
    }
	
	// Update is called once per frame

	void Update () {

        SpawnMonster();
        UpdateCanvas();
        Income();
	}

    private void UpdateCanvas() // Update the Base HP on the canvas
    {
        canvHPT = canv.transform.Find("BaseHPText").gameObject;
        canvHPT.GetComponent<Text>().text = "Remaining Health : " + baseHP.health;

        goldT = canv.transform.Find("GoldText").gameObject;
        goldT.GetComponent<Text>().text = "Gold : " + currentGold;

        incomeT = canv.transform.Find("IncomeText").gameObject;
        incomeT.GetComponent<Text>().text = "Income : " + currentIncome + " Per "+incomeInterval +" seconds";
    }

    private void SpawnMonster() // Send the monsters
    {
        if (Input.GetKeyDown("o"))
        {
            spawner.SpawnMonster1();
        }
        else if (Input.GetKeyDown("p"))
        {
            spawner.SpawnMonster2();
        }
    }

    private void Income()
    {
        if (timeCount <= Time.time)
        {
            currentGold = currentGold + currentIncome;
            timeCount = timeCount + incomeInterval;
        }
    }
}
