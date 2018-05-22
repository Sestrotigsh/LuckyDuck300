using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerManagement : NetworkBehaviour {

	GameManager gameManager;

    private static int playerTeam = 0; // Keep trask of which team both players are

    private int thisPlayerTeam; // The current team of this player

    private SpawnMinions spawner; // Script associated with this team spanwer
    private EventTrigger baseHP; // cript associated with this team base HP
    private GameObject canv; // The canvas and Text attached with this player
    private GameObject canvHPT;
    private GameObject goldT;
    private GameObject incomeT;

    public int startingGold = 50;
    public int currentIncome;
    public int currentGold;
    public int incomeInterval = 5;
    private int timeCount;

	public GameObject placeTower;
	public Vector3 position;





	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager> ();

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
        canvHPT = canv.transform.Find("BaseHPText").gameObject;
        goldT = canv.transform.Find("GoldText").gameObject;
        incomeT = canv.transform.Find("IncomeText").gameObject;

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
  
        canvHPT.GetComponent<Text>().text = "Remaining Health : " + baseHP.health;       
        goldT.GetComponent<Text>().text = "Gold : " + currentGold;      
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

	public void SpawnTower() {
		if (isLocalPlayer) {
			//Debug.Log (thisPlayerTeam);
			//GameObject go = Instantiate (placeTower, position, transform.rotation);
			CmdSpawnTower ();
		}
	}

	[Command]
	void CmdSpawnTower() {
		GameObject go = Instantiate (placeTower, position, transform.rotation);
		NetworkServer.SpawnWithClientAuthority(go,connectionToClient);
	}
}
