using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyWaves : NetworkBehaviour {
// SPAWN GENERIC IN WAVES TO ATTACK THE PLAYER AND SHOW ON OTHER PLAYERS GAME

	// Spawners and enemy in the wave
	public GameObject playerSpawner;
	//public GameObject enemy;
	public GameObject alienEnemy;
	public GameObject slasherEnemy;

	private int nextSpawn; // Determine the start of the next Wave
	public int initialSpawn; // Determine the time for the first wavespawn
	private int spawnDistance = 10; // Determine the time distance between the end and the start of the next wave 
	public int waveLength = 3; // Number of second / number of minions the wave will be
	private int remainingMinions; // The current time the wave start + the waveLength
	private PlayerNetwork PlayerNet; // networking attached to the player

	// Use this for initialization
	void Start () {
		// only spawn for the local player
		PlayerNet = this.GetComponent<PlayerNetwork> ();
		if (!PlayerNet.local) {
			enabled = false;
		}
       
		remainingMinions = waveLength;
		nextSpawn = initialSpawn;
		playerSpawner = GameObject.FindGameObjectWithTag ("Spawn" + PlayerNet.team);
		alienEnemy.tag = "Enemy"+PlayerNet.team;
		slasherEnemy.tag = "Enemy"+PlayerNet.team;

		//if (transform.Find("AlienClothes").gameObject.activeSelf == true) {
				//enemy = alienEnemy;
		//} else if (transform.Find("SlasherClothes").gameObject.activeSelf == true) {
				//enemy = slasherEnemy;
		//}
	}

	// Update is called once per frame
	void Update () {
		Wave();
	}

	/// <summary>
	/// spawn the wave of minions
	/// </summary>
	private void Wave() {
		if (Time.timeSinceLevelLoad > nextSpawn) {
			if (remainingMinions > 0) {
				nextSpawn = nextSpawn + 1;
				if (PlayerNet.opponent != null) {
                    if (PlayerNet.opponent.transform.Find("AlienClothes").gameObject.activeSelf == true) {
                        CmdSpawnEnemyAlien(playerSpawner.transform.position);
                        
                    } else if (PlayerNet.opponent.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
						CmdSpawnEnemySlasher(playerSpawner.transform.position);
                        
                    }
				} else {
					if (transform.Find("AlienClothes").gameObject.activeSelf == true) {
                        CmdSpawnEnemyAlien(playerSpawner.transform.position);
                       
                    } else {
                        CmdSpawnEnemySlasher(playerSpawner.transform.position);
                        
                    }
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
	void CmdSpawnEnemyAlien(Vector3 pos) {
		var currentEnemy = Instantiate(alienEnemy, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
		NetworkServer.Spawn (currentEnemy);
	}

	[Command]
	void CmdSpawnEnemySlasher(Vector3 pos) {
		var currentEnemy = Instantiate(slasherEnemy, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
		NetworkServer.Spawn (currentEnemy);
	}

}
