using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyWaves : NetworkBehaviour {

	public GameObject playerSpawner;
	public GameObject enemySpawner;
	public GameObject enemy;

	private int nextSpawn; // Determine the start of the next Wave
	public int initialSpawn; // Determine the time for the first wavespawn
	private int spawnDistance = 10; // Determine the time distance between the end and the start of the next wave 
	public int waveLength = 5; // Number of second / number of minions the wave will be
	private int remainingMinions; // The current time the wave start + the waveLength

	private PlayerNetwork PlayerNet;

	// Use this for initialization
	void Start () {
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

	private void Wave() {
		if (Time.timeSinceLevelLoad > nextSpawn) {
			if (remainingMinions > 0) {
				nextSpawn = nextSpawn + 1;
				CmdSpawnEnemy(playerSpawner.transform.position);
				remainingMinions = remainingMinions - 1;
			} else {
				remainingMinions = waveLength;
				nextSpawn = nextSpawn + spawnDistance;
			}                      
		}
	}

	[Command]
	void CmdSpawnEnemy(Vector3 pos)
	{
		var currentEnemy = Instantiate(enemy, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
		NetworkServer.Spawn (currentEnemy);
	}
}
