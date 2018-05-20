using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : NavigationAgent
{
    //Movement Variables
    public float moveSpeed = 10.0f;
    public float minDistance = 0.1f;
	public int health = 1;
	private ProjectileController bullet;

    public int team;
    public int newState = 0;
    private int currentState = 0;
    public int startNode;
    public int goal; // The final goal the minion / monster aims to reach
    System.Random rand = new System.Random();

    // Use this for initialization
    void Start()
    {

        if (this.tag == "Enemy1")
        {
            team = 1;
        }
        else
        {
            team = 0;
        }

        if (rand.Next(0, 2) == 0)
        {
            startNode = 22;
        }
        else
        {
            startNode = 11;
        }

        //Find waypoint graph
        graphNodes = GameObject.FindGameObjectWithTag("waypoint graph" + team).GetComponent<WaypointGraph>();
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
			Die(health);
                break;
        }

        Move();
        
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

	private void Die(int damage)
    {
		health -= damage;
		if (health <= 0) {
			Destroy(this.gameObject);
		}
        
    }

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Projectile")) {
			bullet = other.gameObject.GetComponent<ProjectileController> ();
			Die(bullet.damage);
		}
	}
}