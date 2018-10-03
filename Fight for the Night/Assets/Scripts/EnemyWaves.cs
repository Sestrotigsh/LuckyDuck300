using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyWaves : NetworkBehaviour {
    // SPAWN GENERIC IN WAVES TO ATTACK THE PLAYER AND SHOW ON OTHER PLAYERS GAME

    // Spawners and enemy in the wave
    
    public GameObject spawner;
	public GameObject alienEnemy;
	public GameObject slasherEnemy;
    private GameObject[,] refArray = new GameObject[1,2];

    System.Random rand = new System.Random();


        // Other variables
    private bool player0Check = false;
    private bool player1Check = false;

    private string player0Opp = null;
    private string player1Opp = null;

    private PlayerNetwork player0;
    private PlayerNetwork player1;


    public int waveLength = 3; // Number of second / number of minions the wave will be
    private int remainingMinions; // The current time the wave start + the waveLength
    private float nextSpawn; // Determine the start of the next Wave
    public int initialSpawn; // Determine the time for the first wavespawn	
	public int spawnDistance; // Determine the time distance between the end and the start of the next wave 
    public int waveCount = 0;

    private int selectedPath1 = -1;
    private int selectedPath2 = -1;
    private int selectedChar1 = -1;
    private int selectedChar2 = -1;



	// Use this for initialization
	void Start () {
        if (!isServer || !this.GetComponent<PlayerNetwork>().local) {
            enabled = false;
        }
        refArray[0,0] = alienEnemy;
        refArray[0,1] = slasherEnemy;


		remainingMinions = waveLength;
		nextSpawn = initialSpawn;





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
            nextSpawn = Time.timeSinceLevelLoad + initialSpawn;

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
        if (remainingMinions > 0) {
            if (Time.timeSinceLevelLoad > nextSpawn) {

                if (player0Opp == "Alien")
                {
                    selectedChar1 = 0;
                    // Pick the random path the enemy will take
                     if (rand.Next(0, 2) == 0) {
                        selectedPath1 = 0;
                    } else {
                        selectedPath1 = 11;
                    }
                } else {
                    selectedChar1 = 1;
                    // Pick the random path the enemy will take
                    if (rand.Next(0, 2) == 0) {
                        selectedPath1 = 0;
                    } else {
                         selectedPath1 = 11;
                    }
                }
                
                if (player1Opp == "Alien")
                {
                    selectedChar2 = 0;
                    // Pick the random path the enemy will take
                    if (rand.Next(0, 2) == 0) {
                         selectedPath2 = 0;
                    } else {
                        selectedPath2 = 11;
                    }
                } else
                {
                    selectedChar2 = 1;
                    // Pick the random path the enemy will take
                    if (rand.Next(0, 2) == 0) {
                        selectedPath2 = 0;
                    } else {
                        selectedPath2 = 11;
                    }
                }
                CmdSpawnMinion(selectedChar1, selectedChar2, selectedPath1, selectedPath2, spawner.transform.position, waveCount);

                remainingMinions = remainingMinions - 1;
                nextSpawn = Time.timeSinceLevelLoad + 2.0f;
            }
        } else {
            remainingMinions = waveLength;
            nextSpawn =  Time.timeSinceLevelLoad + spawnDistance;
            waveCount = waveCount + 1;
        }
	}





    [Command]
    void CmdSpawnMinion(int team1, int team2, int path1, int path2, Vector3 position, int healthScale) {
        var currentEnemy1 = Instantiate(refArray[0,team1], (position - new Vector3 (5.0f, 0.0f, 0.0f)), Quaternion.Euler(0, 0, 0)) as GameObject;
        currentEnemy1.GetComponent<EnemyTagging>().syncData.y = path1;
        currentEnemy1.GetComponent<EnemyTagging>().syncData.z = healthScale;
        NetworkServer.Spawn (currentEnemy1);
        var currentEnemy2 = Instantiate(refArray[0,team2], (position + new Vector3 (5.0f, 0.0f, 0.0f)), Quaternion.Euler(0, 0, 0)) as GameObject;
        currentEnemy2.GetComponent<EnemyTagging>().syncData.y = path2;
        currentEnemy2.GetComponent<EnemyTagging>().syncData.z = healthScale;
        NetworkServer.Spawn (currentEnemy2);
    }
}
