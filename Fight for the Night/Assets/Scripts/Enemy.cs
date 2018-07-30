using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : NavigationAgent {
	
    //Movement Variables
	public float centreZ;
    public float moveSpeed = 10.0f;
    public float minDistance = 0.1f;
	public int health = 1;
	private ProjectileController bullet;

	// Multiplayer variables
    public int team;
    public int enemyTeam;
    public int newState = 0;
    private int currentState = 0;
    public int startNode;
    public int goal; // The final goal the minion / monster aims to reach
    System.Random rand = new System.Random();

	// Player variables
	private PlayerManagement playerMan;
	private PlayerNetwork playerNet;

	// Spawn points
	public GameObject Spawner1;
	public GameObject Spawner2;

    public bool customPathBool = false;
    public int customPathDirection = 0;

    public int value; // The gold value of the monster
    public bool isStunned = false;
    public float endOfStun;

    // Use this for initialization
    void Start() {
		// find spawners and calculate distances
		Spawner1 = GameObject.FindGameObjectWithTag ("Spawn" + 0);
		Spawner2 = GameObject.FindGameObjectWithTag ("Spawn" + 1);
		float distanceToSpawn1 = Vector3.Distance (transform.position, Spawner1.transform.position);
		float distanceToSpawn2 = Vector3.Distance (transform.position, Spawner2.transform.position);

       
            


            // choose which of the two paths to go down
            if (rand.Next(0, 2) == 0) {
                startNode = 22;
            } else {
                startNode = 11;
            }
        



        //Find waypoint graph
		if (distanceToSpawn1 < distanceToSpawn2) {
			graphNodes = GameObject.FindGameObjectWithTag ("waypoint graph" + 0).GetComponent<WaypointGraph> ();
			playerNet = GameObject.FindGameObjectWithTag ("Player0").GetComponent<PlayerNetwork> ();
			playerMan = GameObject.FindGameObjectWithTag ("Player0").GetComponent<PlayerManagement> ();
		} else {
			graphNodes = GameObject.FindGameObjectWithTag ("waypoint graph" + 1).GetComponent<WaypointGraph> ();
			playerNet = GameObject.FindGameObjectWithTag ("Player1").GetComponent<PlayerNetwork> ();
			playerMan = GameObject.FindGameObjectWithTag ("Player1").GetComponent<PlayerManagement> ();
		}
        //Initial node index to move to
        currentPath.Add(startNode);
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust the path to fit the players custom directions
        if (customPathBool == true) {
            currentPath.Remove(startNode);
            currentPath.Add(customPathDirection);
            customPathBool = false;
        }


		// If enemy is destroyed - perform relevant actions
        if (this.tag == "Dying Enemy") {
            currentState = 1;
        }
		// Switch to control enemiy moving or dying
        switch (currentState) {
            //Roam
            case 0:
                Advance();
                break;
            //Die
            case 1:
			DestroyEnemy();
                break;
        }
		// after sufficient time is passed - remove the stun effect
        if (endOfStun <= Time.timeSinceLevelLoad) {
            isStunned = false;
        }
        if (isStunned == false) {
            Move();
        }
    }

    /// <summary>
    /// Move the enemy
    /// </summary>
    private void Move() {
		// If the goal has not been reached
        if (currentPath.Count > 0) {
            //Move towards next node in path
            transform.position = Vector3.MoveTowards(transform.position, graphNodes.graphNodes[currentPath[currentPathIndex]].transform.position, moveSpeed * Time.deltaTime);
            //Increase path index
            if (Vector3.Distance(transform.position, graphNodes.graphNodes[currentPath[currentPathIndex]].transform.position) <= minDistance) {
                if (currentPathIndex < currentPath.Count - 1)
                    currentPathIndex++;
            }
            currentNodeIndex = graphNodes.graphNodes[currentPath[currentPathIndex]].GetComponent<LinkedNodes>().index;   //Store current node index
        }
    }
		
	/// <summary>
	/// FSM Behaviour - Move towards the next node using greedy search algorithm
	/// </summary>
    private void Advance() {
        if (Vector3.Distance(transform.position, graphNodes.graphNodes[currentPath[currentPath.Count - 1]].transform.position) <= minDistance) {
            currentPath = AStarSearch(currentPath[currentPathIndex], goal);
            currentPathIndex = 0;
        }
    }

	/// <summary>
	/// take damage and die if enough damage is taken
	/// </summary>
	/// <param name="damage">The amount of damage to take</param>
	public void Die(int damage) {
		health -= damage;
		if (health <= 0) {
			playerMan.currentGold = playerMan.currentGold + value;
			playerNet.EnemyDie (this.gameObject);
		}      
    }
	/// <summary>
	/// delete the enemy from the game
	/// </summary>
    private void DestroyEnemy() {
		playerNet.EnemyDie (this.gameObject);
    }   

	/// <summary>
	/// stun the enemy for the specified amount of time
	/// </summary>
	/// <param name="timeStun">The amount of time to be stunned</param>
    public void Stun(float timeStun) {
        isStunned = true;
        endOfStun = Time.time + timeStun;
    }

	/// <summary>
	/// When hit by projectile - call relevant methods and destroy the projectile
	/// </summary>
	/// <param name="other">The projectile that hit the enemy</param>
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Projectile")) {
			Die (other.GetComponent<ProjectileController> ().damage);
			Destroy (other.gameObject);
		}
	}
}