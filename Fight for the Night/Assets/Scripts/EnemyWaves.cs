using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyWaves : NetworkBehaviour {
    // SPAWN GENERIC IN WAVES TO ATTACK THE PLAYER AND SHOW ON OTHER PLAYERS GAME

    // Spawners and enemy in the wave

    public GameObject spawner;
	//public GameObject enemy;
	public GameObject alienEnemy;
	public GameObject slasherEnemy;

	private int nextSpawn; // Determine the start of the next Wave
	public int initialSpawn; // Determine the time for the first wavespawn
	private int spawnDistance = 10; // Determine the time distance between the end and the start of the next wave 
	public int waveLength = 3; // Number of second / number of minions the wave will be
	private int remainingMinions; // The current time the wave start + the waveLength


    // Other variables
    private bool player0Check = false;
    private bool player1Check = false;

    private string player0Opp = null;
    private string player1Opp = null;

    private PlayerNetwork player0;
    private PlayerNetwork player1;

	// Use this for initialization
	void Start () {
        // only spawn for the local player

        

		remainingMinions = waveLength;
		nextSpawn = initialSpawn;
	    
        if (this.tag != "Player0")
        {
            enabled = false;
        }
	}

	// Update is called once per frame
	void Update () {

        if (spawner == null)
        {
            spawner = GameObject.FindGameObjectWithTag("Spawn");
        }

        // Check the first player is in game
        if (player0Check == false)
        {
            if ( GameObject.FindGameObjectWithTag("Player0"))
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

        if (player0Check == true && player1Check == true && (player0Opp == null || player1Opp == null))
        {
            if (player0.opponent.transform.Find("AlienClothes").gameObject.activeSelf == true)
            {
                player0Opp = "Alien";
            }
            else
            {
                player0Opp = "Slasher";
            }

            if (player1.opponent.transform.Find("AlienClothes").gameObject.activeSelf == true)
            {
                player1Opp = "Alien";
            } else
            {
                player1Opp = "Slasher";
            }

        }
            

        if (player0Opp != null && player1Opp != null)
        {
            Wave();
        }
		
	}

	/// <summary>
	/// spawn the wave of minions
	/// </summary>
	private void Wave() {
		if (Time.timeSinceLevelLoad > nextSpawn) {
			if (remainingMinions > 0) {
				nextSpawn = nextSpawn + 1;
				
                if (player0Opp == "Alien")
                {
                    CmdSpawnEnemyAlien( 0);
                } else
                {
                    CmdSpawnEnemySlasher( 0);
                }

                if (player1Opp == "Alien")
                {
                    CmdSpawnEnemyAlien( 1);
                } else
                {
                    CmdSpawnEnemySlasher(1);
                }

				remainingMinions = remainingMinions - 1;
			} else {
				remainingMinions = waveLength;
				nextSpawn = nextSpawn + spawnDistance;
			}                      
		}
	}

	/// <summary>
	/// spawn the enemy with respect to the server
	/// </summary>
	/// <param name="pos">the position to spawn in</param>
	[Command]
	void CmdSpawnEnemyAlien( int minionTeam) {
		var currentEnemy = Instantiate(alienEnemy, spawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        currentEnemy.tag = "Enemy" + minionTeam;
        NetworkServer.Spawn (currentEnemy);
        
    }

	[Command]
	void CmdSpawnEnemySlasher(int minionTeam) {
		var currentEnemy = Instantiate(slasherEnemy, spawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        currentEnemy.tag = "Enemy" + minionTeam;
        NetworkServer.Spawn (currentEnemy);
	}

}
