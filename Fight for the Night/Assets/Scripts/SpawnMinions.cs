using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnMinions : NetworkBehaviour {
////// CONTROLS PLAYERS ABILITIES TO SPAWN MONSTERS TO ATTACK OTHER PLAYER

    private GameObject prompts;
    private float promptDistance;
    public float spawnDistance;

	public GameObject enemySpawner;

    public GameObject monster1Alien;
    public GameObject monster2Alien;
    public GameObject monster1Slasher;
    public GameObject monster2Slasher;

	private PlayerManagement playerMan;
	private PlayerNetwork PlayerNet;

    public int monster1Cost;
	public int monster1IncomeBoost;
    public int monster2Cost;
	public int monster2IncomeBoost;

    // Other variables
    private bool player0Check = false;
    private bool player1Check = false;

    private string playerType = null;

    private PlayerNetwork player0;
    private PlayerNetwork player1;

    private float timer;
    public float spawnGap;

    private GameObject[,] refArray = new GameObject[2,2];


    // Use this for initialization
    void Start () {
        timer = Time.timeSinceLevelLoad;
		playerMan = this.GetComponent<PlayerManagement> ();
		PlayerNet = this.GetComponent<PlayerNetwork> ();
        int newTeam = this.GetComponent<PlayerNetwork>().team;
        prompts = GameObject.FindGameObjectWithTag("Prompts"+newTeam);
		if (!PlayerNet.local) {
            prompts.SetActive(false);
			enabled = false;
		}

		// find the enemy spawner
		enemySpawner = GameObject.FindGameObjectWithTag ("SpawnMonster" + newTeam);

        refArray[0,0] = monster1Alien;
        refArray[0,1] = monster2Alien;
        refArray[1,0] = monster1Slasher;
        refArray[1,1] = monster2Slasher;
	}
	
	// Update is called once per frame
	void Update () {

        // Check the first player is in game
        if (player0Check == false)
        {
            if (GameObject.FindGameObjectWithTag("Player0"))
            {
                player0 = GameObject.FindGameObjectWithTag("Player0").GetComponent<PlayerNetwork>();
                player0Check = true;
            }
        }

        // Check the second player is in game
        if (player1Check == false)
        {
            if (GameObject.FindGameObjectWithTag("Player1"))
            {
                player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerNetwork>();
                player1Check = true;
            }
        }

        // Check for opponent
        if (player0Check == true && player1Check == true && playerType == null)
        {
            if (this.tag == "Player0")
            {
                if (player1.opponent.transform.Find("AlienClothes").gameObject.activeSelf == true)
                {
                    playerType = "Alien";
                }
                else
                {
                    playerType = "Slasher";
                }
            } else
            {
                if (player0.opponent.transform.Find("AlienClothes").gameObject.activeSelf == true)
                {
                    playerType = "Alien";
                }
                else
                {
                    playerType = "Slasher";
                }
            }                     

        }

        // check the distance to the prompts
        promptDistance = Vector3.Distance(prompts.transform.position, this.transform.position);

        if (promptDistance < spawnDistance) {
            prompts.SetActive(true);
                    if (Input.GetKeyDown("q")) {
            if (timer < Time.timeSinceLevelLoad) {
                 if (playerMan.currentGold >= monster1Cost) {
                if (playerType == "Alien") {
                    // Check which team is spawning
                        CmdSpawnMonster(0,0);
                } else {
                    // Check which team is spawning
                        CmdSpawnMonster(1,0);
                }
                playerMan.Spend(monster1Cost);
                playerMan.currentIncome += monster1IncomeBoost;
            }
            timer = Time.timeSinceLevelLoad + spawnGap;
            }
        }
        else if (Input.GetKeyDown("e")) {
            if (timer < Time.timeSinceLevelLoad) {
                if (playerMan.currentGold >= monster2Cost) {
                if (playerType == "Alien") {
                        CmdSpawnMonster(0,1);
                } else {
                        CmdSpawnMonster(1,1);
                }
                playerMan.Spend(monster2Cost);
                playerMan.currentIncome += monster2IncomeBoost;
            }
                timer = Time.timeSinceLevelLoad + spawnGap;
            }

        }

        } else {
            prompts.SetActive(false);
        }

	}

    [Command]
    void CmdSpawnMonster(int team, int monsterType) {
        var currentMonster = Instantiate(refArray[team,monsterType], enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        currentMonster.GetComponent<EnemyTagging>().syncData.x = 1;
        NetworkServer.Spawn (currentMonster);
    }

}
