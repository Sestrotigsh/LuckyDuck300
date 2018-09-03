using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnMinions : NetworkBehaviour {
////// CONTROLS PLAYERS ABILITIES TO SPAWN MONSTERS TO ATTACK OTHER PLAYER


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


    // Use this for initialization
    void Start () {
		playerMan = this.GetComponent<PlayerManagement> ();
		PlayerNet = this.GetComponent<PlayerNetwork> ();
		if (!PlayerNet.local) {
			enabled = false;
		}
		// find the enemy spawner
		int newTeam = this.GetComponent<PlayerNetwork>().team;
		enemySpawner = GameObject.FindGameObjectWithTag ("SpawnMonster" + newTeam);
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

        if (Input.GetKeyDown("o")) {
			if (playerMan.currentGold >= monster1Cost) {
				if (playerType == "Alien") {
            		CmdSendMonster1Alien();
				} else {
					CmdSendMonster1Slasher();
				}
				playerMan.currentGold -= monster1Cost;
				playerMan.currentIncome += monster1IncomeBoost;
			}
		}
        else if (Input.GetKeyDown("p")) {
            if (playerMan.currentGold >= monster2Cost) {
            	if (playerType == "Alien") {
            		CmdSendMonster2Alien();
				} else {
					CmdSendMonster2Slasher();
				}
                playerMan.currentGold -= monster2Cost;
                playerMan.currentIncome += monster2IncomeBoost;
            }
        }
	}

	[Command]
    void CmdSendMonster1Alien() {
		var currentMonster = Instantiate(monster1Alien, enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
		currentMonster.GetComponent<Enemy>().minionType = Enemy.type.Monster;
		if (enemySpawner.tag == "SpawnMonster0")
        {
            currentMonster.tag = "Enemy1";
        }
        else
        {
            currentMonster.tag = "Enemy0";
        }
        NetworkServer.Spawn (currentMonster);
	}

	[Command]
    void CmdSendMonster2Alien() {
		var currentMonster = Instantiate(monster2Alien, enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        currentMonster.GetComponent<Enemy>().minionType = Enemy.type.Monster;
        if (enemySpawner.tag == "SpawnMonster0")
        {
            currentMonster.tag = "Enemy1";
        }
        else
        {
            currentMonster.tag = "Enemy0";
        }

        NetworkServer.Spawn (currentMonster);
    }

	[Command]
    void CmdSendMonster1Slasher() {
		var currentMonster = Instantiate(monster1Slasher, enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        currentMonster.GetComponent<Enemy>().minionType = Enemy.type.Monster;
		 if (enemySpawner.tag == "SpawnMonster0")
        {
            currentMonster.tag = "Enemy0";
        }
        else
        {
            currentMonster.tag = "Enemy1";
        }
        NetworkServer.Spawn (currentMonster);
	}

    [Command]
    void CmdSendMonster2Slasher() {
		var currentMonster = Instantiate(monster2Slasher, enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        currentMonster.GetComponent<Enemy>().minionType = Enemy.type.Monster;
         if (enemySpawner.tag == "SpawnMonster0")
        {
            currentMonster.tag = "Enemy0";
        }
        else
        {
            currentMonster.tag = "Enemy1";
        }
        NetworkServer.Spawn (currentMonster);
    }
}
