using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinions : MonoBehaviour {

	public GameObject enemy;
	private Vector3 position;
	private float nextSpawn = 0; // Determine the start of the next Wave
	public float initialSpawn = 10; // Determine the time for the first wavespawn
	private float spawnDistance = 5; // Determine the time distance between the end and the start of the next wave 
	public float waveLength = 5; // Number of second / number of minions the wave will be
	private float timeAfterWave; // The current time the wave start + the waveLength

	public int team;
	private Vector3 spawnSpot;


	// Use this for initialization
	void Start () {
		timeAfterWave = waveLength;
		spawnSpot = transform.position;
		spawnSpot.y = 0;
		nextSpawn = initialSpawn;
	}
	
	// Update is called once per frame
	void Update () {
		Wave();
	}

	private void Wave()
	{

		if (Time.time > nextSpawn)
		{
			if (timeAfterWave > 0)
			{
				nextSpawn = nextSpawn + 1;
				SpawnEnemy();
				timeAfterWave = timeAfterWave - 1;
			} else
			{
				timeAfterWave = waveLength;
				nextSpawn = nextSpawn + spawnDistance;
			}                      
		}
	}

	private void SpawnEnemy()
	{
		GameObject enemies = Instantiate(enemy, spawnSpot, Quaternion.Euler(-90, 0, 0));
		enemies.tag = "Enemy" + team;
	}



}
