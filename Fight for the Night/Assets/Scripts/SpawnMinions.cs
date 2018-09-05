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
                    // Check which team is spawning
                    if (enemySpawner.tag == "SpawnMonster0") {
                        CmdSendMonster1Alien(1,-10);
                    } else {
                        CmdSendMonster1Alien(0,-10);
                    }
				} else {
                    // Check which team is spawning
                    if (enemySpawner.tag == "SpawnMonster0") {
                        CmdSendMonster1Slasher(1,-10);
                    } else {
                        CmdSendMonster1Slasher(0,-10);
                    }
				}
				playerMan.currentGold -= monster1Cost;
				playerMan.currentIncome += monster1IncomeBoost;
			}
		}
        else if (Input.GetKeyDown("p")) {
            if (playerMan.currentGold >= monster2Cost) {
            	if (playerType == "Alien") {
                    // Check which team is spawning
                    if (enemySpawner.tag == "SpawnMonster0") {
                        CmdSendMonster2Alien(1,-10);
                    } else {
                        CmdSendMonster2Alien(0,-10);
                    }
				} else {
                    // Check which team is spawning
                    if (enemySpawner.tag == "SpawnMonster0") {
                        CmdSendMonster2Slasher(1,-10);
                    } else {
                        CmdSendMonster2Slasher(0,-10);
                    }
				}
                playerMan.currentGold -= monster2Cost;
                playerMan.currentIncome += monster2IncomeBoost;
            }
        }
	}


	[Command]
    // minion team is a value denoting the minions team
    // monsterPlaceholder is a value of -10 to define the enemy is a monster
    void CmdSendMonster1Alien(int minionTeam, int monsterPlaceholder) {
		var currentMonster = Instantiate(monster1Alien, enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
		currentMonster.GetComponent<Enemy>().minionType = Enemy.type.Monster;
        currentMonster.GetComponent<EnemyTagging>().team = minionTeam;
        currentMonster.GetComponent<EnemyTagging>().path = monsterPlaceholder;
        NetworkServer.Spawn (currentMonster);
	}

	[Command]
    // minion team is a value denoting the minions team
    // monsterPlaceholder is a value of -10 to define the enemy is a monster
    void CmdSendMonster2Alien(int minionTeam, int monsterPlaceholder) {
		var currentMonster = Instantiate(monster2Alien, enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        currentMonster.GetComponent<Enemy>().minionType = Enemy.type.Monster;
        currentMonster.GetComponent<EnemyTagging>().team = minionTeam;
        currentMonster.GetComponent<EnemyTagging>().path = monsterPlaceholder;
        NetworkServer.Spawn (currentMonster);
    }

	[Command]
    // minion team is a value denoting the minions team
    // monsterPlaceholder is a value of -10 to define the enemy is a monster
    void CmdSendMonster1Slasher(int minionTeam, int monsterPlaceholder) {
		var currentMonster = Instantiate(monster1Slasher, enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        currentMonster.GetComponent<Enemy>().minionType = Enemy.type.Monster;
        currentMonster.GetComponent<EnemyTagging>().team = minionTeam;
        currentMonster.GetComponent<EnemyTagging>().path = monsterPlaceholder;
        NetworkServer.Spawn (currentMonster);
	}

    [Command]
    // minion team is a value denoting the minions team
    // monsterPlaceholder is a value of -10 to define the enemy is a monster
    void CmdSendMonster2Slasher(int minionTeam, int monsterPlaceholder) {
		var currentMonster = Instantiate(monster2Slasher, enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        currentMonster.GetComponent<Enemy>().minionType = Enemy.type.Monster;
        currentMonster.GetComponent<EnemyTagging>().team = minionTeam;
        currentMonster.GetComponent<EnemyTagging>().path = monsterPlaceholder;
        NetworkServer.Spawn (currentMonster);
    }
}
