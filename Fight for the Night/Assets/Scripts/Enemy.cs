using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : NavigationAgent
{
    //Movement Variables
	public float centreZ;
    public float moveSpeed = 10.0f;
    public float minDistance = 0.1f;
	public int health = 1;
	private ProjectileController bullet;

    public int team;
    public int enemyTeam;
    public int newState = 0;
    private int currentState = 0;
    public int startNode;
    public int goal; // The final goal the minion / monster aims to reach
    System.Random rand = new System.Random();

	private PlayerManagement playerMan;
	private PlayerNetwork playerNet;

	public GameObject Spawner1;
	public GameObject Spawner2;

    public int value; // The gold value of the monster

    public bool isStunned = false;
    public float endOfStun;

    // Use this for initialization
    void Start()
    {
		Spawner1 = GameObject.FindGameObjectWithTag ("Spawn" + 0);
		Spawner2 = GameObject.FindGameObjectWithTag ("Spawn" + 1);
		float distanceToSpawn1 = Vector3.Distance (transform.position, Spawner1.transform.position);
		float distanceToSpawn2 = Vector3.Distance (transform.position, Spawner2.transform.position);


        if (rand.Next(0, 2) == 0)
        {
            startNode = 22;
        }
        else
        {
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

        if (this.tag == "Dying Enemy")
        {
            currentState = 1;
        }

        switch (currentState)
        {
            //Roam
            case 0:
                Advance();
                break;
            //Hide
            case 1:
			DestroyEnemy();
                break;
        }

        if (endOfStun <= Time.timeSinceLevelLoad)
        {
            isStunned = false;
        }

        if (isStunned == false)
        {
            Move();
        }
        
        
    }

    //Move Enemy
    private void Move()
    {

        if (currentPath.Count > 0)
        {

            //Move towards next node in path
            transform.position = Vector3.MoveTowards(transform.position, graphNodes.graphNodes[currentPath[currentPathIndex]].transform.position, moveSpeed * Time.deltaTime);

            //Increase path index
            if (Vector3.Distance(transform.position, graphNodes.graphNodes[currentPath[currentPathIndex]].transform.position) <= minDistance)
            {

                if (currentPathIndex < currentPath.Count - 1)
                    currentPathIndex++;
            }

            currentNodeIndex = graphNodes.graphNodes[currentPath[currentPathIndex]].GetComponent<LinkedNodes>().index;   //Store current node index
        }
    }

    //FSM Behaviour - Roam - Randomly select nodes to travel to using Greedy Search Algorithm
    private void Advance()
    {
        if (Vector3.Distance(transform.position, graphNodes.graphNodes[currentPath[currentPath.Count - 1]].transform.position) <= minDistance)
        {
            currentPath = AStarSearch(currentPath[currentPathIndex], goal);
            currentPathIndex = 0;
        }

    }

	public void Die(int damage)
    {
		health -= damage;
		if (health <= 0) {
			playerMan.currentGold = playerMan.currentGold + value;
			playerNet.EnemyDie (this.gameObject);
		}      
    }



    private void DestroyEnemy()
    {
		playerNet.EnemyDie (this.gameObject);
    }   

    public void Stun(float timeStun)
    {
        isStunned = true;
        endOfStun = Time.time + timeStun;
    }

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Projectile")) {
			Die (other.GetComponent<ProjectileController> ().damage);
			Destroy (other.gameObject);
		}
	}
}