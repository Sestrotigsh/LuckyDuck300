using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyWavesSlasher : NetworkBehaviour {
// SPAWN GENERIC IN WAVES TO ATTACK THE PLAYER AND SHOW ON OTHER PLAYERS GAME

	// Spawners and enemy in the wave
	public GameObject playerSpawner;
	public GameObject enemy;

	private int nextSpawn; // Determine the start of the next Wave
	public int initialSpawn; // Determine the time for the first wavespawn
	private int spawnDistance = 10; // Determine the time distance between the end and the start of the next wave 
	public int waveLength = 5; // Number of second / number of minions the wave will be
	private int remainingMinions; // The current time the wave start + the waveLength
	private PlayerNetwork PlayerNet; // networking attached to the player

	public int monster1HPBoost;

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
				CmdSpawnEnemy(playerSpawner.transform.position);
				remainingMinions = remainingMinions - 1;
			} else {
				remainingMinions = waveLength;
				nextSpawn = nextSpawn + spawnDistance;
				monster1HPBoost = monster1HPBoost + 2;
			}                      
		}
	}

	/// <summary>
	/// spawn the enemy with respect to the server
	/// </summary>
	/// <param name="pos">the position to spawn in</param>
	[Command]
	void CmdSpawnEnemy(Vector3 pos) {
		var currentEnemy = Instantiate(enemy, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
		currentEnemy.GetComponent<Enemy>().health = currentEnemy.GetComponent<Enemy>().health + monster1HPBoost;
        if (playerSpawner.tag == "Spawn0")
        {
            currentEnemy.tag = "Enemy0";
        }
        else
        {
            currentEnemy.tag = "Enemy1";
        }
        NetworkServer.Spawn (currentEnemy);
	}

}
