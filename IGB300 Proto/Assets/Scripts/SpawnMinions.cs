using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinions : MonoBehaviour {

	public GameObject enemy;
    public GameObject monster1;
    public GameObject monster2;

    public int monster1Cost;
    public int monster2Cost;

    private PlayerManagement playerMan;

    private Vector3 position;
	private int nextSpawn; // Determine the start of the next Wave
	public int initialSpawn; // Determine the time for the first wavespawn
	private int spawnDistance = 5; // Determine the time distance between the end and the start of the next wave 
	public int waveLength = 5; // Number of second / number of minions the wave will be
	private int remainingMinions; // The current time the wave start + the waveLength

	public int team;
	private Vector3 spawnSpot;


	// Use this for initialization
	void Start () {

        remainingMinions = waveLength;
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
			if (remainingMinions > 0)
			{
				nextSpawn = nextSpawn + 1;
				SpawnEnemy();
                remainingMinions = remainingMinions - 1;
			} else
			{
                remainingMinions = waveLength;
				nextSpawn = nextSpawn + spawnDistance;
			}                      
		}
	}

	private void SpawnEnemy()
	{
		GameObject enemies = Instantiate(enemy, spawnSpot, Quaternion.Euler(0, 0, 0));
		enemies.tag = "Enemy" + team;
	}

    public void SpawnMonster1()
    {
        if (playerMan.currentGold >= monster1Cost && Time.time >= initialSpawn)
        {
            GameObject enemies = Instantiate(monster1, spawnSpot, Quaternion.Euler(0, 0, 0));
            enemies.tag = "Enemy" + team;
            playerMan.currentIncome = playerMan.currentIncome + 2;
            playerMan.currentGold = playerMan.currentGold - monster1Cost;
        }
  
    }
    public void SpawnMonster2()
    {

        if (playerMan.currentGold >= monster2Cost && Time.time >= initialSpawn)
        {
            GameObject enemies = Instantiate(monster2, spawnSpot, Quaternion.Euler(0, 0, 0));
            enemies.tag = "Enemy" + team;
            playerMan.currentIncome = playerMan.currentIncome + 5;
            playerMan.currentGold = playerMan.currentGold - monster2Cost;
        }
    }

    public void GetPlayer()
    {
        playerMan = GameObject.FindGameObjectWithTag("Player" + team).GetComponent<PlayerManagement>();
    }
}
