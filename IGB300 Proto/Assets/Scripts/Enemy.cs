using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NavigationAgent {

    //Player Reference
    Player player;

    //Movement Variables
    public float moveSpeed = 10.0f;
    public float minDistance = 0.1f;

    //FSM Variables
    public int newState = 0;
    private int currentState = 0;
    public int startNode = 11;
    public int goal = 0; // The final goal the minion / monster aims to reach
    

    // Use this for initialization
    void Start() {      
        //Initial node index to move to
        currentPath.Add(startNode);
        //Establish reference to player game object
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update () {

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
                Die();
                break;           
        }
                Move();
    }

        //Move Enemy
        private void Move() {

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

    //FSM Behaviour - Roam - Randomly select nodes to travel to using Greedy Search Algorithm
    private void Advance() {
        if (Vector3.Distance(transform.position, graphNodes.graphNodes[currentPath[currentPath.Count - 1]].transform.position) <= minDistance)
        {
            currentPath = AStarSearch(currentPath[currentPathIndex], goal);
            currentPathIndex = 0;
        }

    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
    
}
