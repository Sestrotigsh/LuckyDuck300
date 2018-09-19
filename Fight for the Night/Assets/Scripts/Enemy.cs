using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Enemy : NavigationAgent {
///// CONTROLS ENEMY AI MOVEMENT AND DEATH

    //Movement Variables
    public float moveSpeed = 10.0f;
    public float minDistance = 0.1f;
	public int health = 1;
    public float currentHealth;
    public float initialSpeed;
    public int healthScalingValue;

	// Multiplayer variables
    public int team;
    public int enemyTeam;
    public int newState = 0;
    private int currentState = 0;
    public int startNode;
    public int goal; // The final goal the minion / monster aims to 
    // TACTICAL CONTROLS - RANDOM NUMBER


	// Player variables
	private PlayerManagement playerMan;
	private PlayerNetwork playerNet;

    public bool pathReady = false;
    public bool teamReady = false;
    public bool monsterReady = false;
    public bool healthReady = true;


        public enum type
        {
        Minion,
        Monster
        };

    public type minionType = type.Minion;

    // HP Bar
    public Image healthBar;

    // TACTICAL CONTROLS - CHOOSE WHICH PATH TO SEND MINIONS
    //public bool customPathBool = false;
    //public int customPathDirection = 0;

    public float value; // The gold value of the monster
    public bool isStunned = false;
    public float endOfStun;

    // Death flashing effect variables
    private float deathTimer = 0.0f;
    private SkinnedMeshRenderer rend;
    public GameObject mesh;
    public GameObject animator;

    // Use this for initialization
    void Start() {
        currentHealth = health;
		// find spawners and calculate distances    
        goal = 16;
        initialSpeed = moveSpeed;
        startNode = -1;

        // initialise the renderer
        rend = mesh.GetComponent<SkinnedMeshRenderer>();
        //rend.material = mesh.Shader.Find("Blink");  
    }

    // Update is called once per frame
    void Update() {
        // if the enemy has been assigned as a monster
        if (monsterReady == true) {
            // set it as a monster
            if (minionType != type.Monster) {
                minionType = type.Monster;
            } 
            // give it the desired start node
            startNode = 5;
            currentPath.Add(startNode);
            monsterReady = false;
        }

        // if the team has been assigned
        if (teamReady == true) {
            if (this.tag == "Enemy0") {
                graphNodes = GameObject.FindGameObjectWithTag ("waypoint graph" + 0).GetComponent<WaypointGraph> ();
                playerNet = GameObject.FindGameObjectWithTag ("Player0").GetComponent<PlayerNetwork> ();
                playerMan = GameObject.FindGameObjectWithTag ("Player0").GetComponent<PlayerManagement> ();
                team = 0;
            } else if (this.tag == "Enemy1") {
                graphNodes = GameObject.FindGameObjectWithTag ("waypoint graph" + 1).GetComponent<WaypointGraph> ();
                playerNet = GameObject.FindGameObjectWithTag ("Player1").GetComponent<PlayerNetwork> ();
                playerMan = GameObject.FindGameObjectWithTag ("Player1").GetComponent<PlayerManagement> ();
                team = 1;
            }
            teamReady = false;
        }

        // if the non monster has been assigned a path
        if (pathReady == true) {
            startNode = this.GetComponent<EnemyTagging>().path;
            currentPath.Add(startNode);
            pathReady = false;
        }

        if (healthReady== true)
        {
            health = health + healthScalingValue * this.GetComponent<EnemyTagging>().waveNumber;
            healthReady = false;
        }

        // if the path is ready, progress with standard enemy behaviour
        if (startNode != -1 && graphNodes != null) {
            if (deathTimer != 0.0f) {
            rend.materials[0].SetFloat("_Blink", 1.0f);
            rend.materials[1].SetFloat("_Blink", 1.0f);
            //rend.material.SetFloat("_Blink", 1.0f);
            if (animator != null) {
                animator.GetComponent<Animator>().enabled = false;
            }
            this.GetComponent<BoxCollider>().enabled = false;
            if (deathTimer < Time.timeSinceLevelLoad) {
                playerNet.EnemyDie (this.gameObject);
            }
        }

        // TACTICAL CONTROLS - PLAYER CONTROLLED MINION PATH
        // Adjust the path to fit the players custom directions
        // if (customPathBool == true) {
            //currentPath.Remove(startNode);
            //currentPath.Add(customPathDirection);
            //customPathBool = false;
        //}

        // If enemy is destroyed - perform relevant actions
        if (this.tag == "Dying Enemy") {
            currentState = 1;
        }

        if (this.tag == "Dying Enemy2") {
            currentState = 2;
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
            case 2:
                playerNet.EnemyDie (this.gameObject);
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
		currentHealth = currentHealth - damage;
        healthBar.fillAmount = currentHealth / health;
		if (currentHealth <= 0) {  
            value = value / (Mathf.Log10 (playerMan.currentGold) / 2);
			playerMan.Earn(value);
            isStunned = true;
            endOfStun = Time.time + 2.0f;
            deathTimer = Time.timeSinceLevelLoad + 0.75f; // CHANGE HAS OCCURED HERE FOR BUG FIXING was 0.75f
			//playerNet.EnemyDie (this.gameObject);
		}      
    }
	/// <summary>
	/// delete the enemy from the game
	/// </summary>
    private void DestroyEnemy() {
        ///////// CODE BEING ALTERED HERE
        isStunned = true;
        endOfStun = Time.time + 2.0f;
        deathTimer = Time.timeSinceLevelLoad + 0.75f;
		//playerNet.EnemyDie (this.gameObject);
    }   

	/// <summary>
	/// stun the enemy for the specified amount of time
	/// </summary>
	/// <param name="timeStun">The amount of time to be stunned</param>
    public void Stun(float timeStun) {
        isStunned = true;
        endOfStun = Time.time + timeStun;
    }

    // A slow of 2 means 50%
    IEnumerator Slow(float levelOfSlow, int duration)
    {
        moveSpeed = moveSpeed / levelOfSlow;
        yield return new WaitForSeconds(duration);
        moveSpeed = initialSpeed;
        yield break;
    }

    public void GetSlowed(float levelOfSlow, int duration)
    {
        StartCoroutine(Slow(levelOfSlow, duration));
    }


	/// <summary>
	/// When hit by projectile - call relevant methods and destroy the projectile
	/// </summary>
	/// <param name="other">The projectile that hit the enemy</param>
	void OnTriggerEnter(Collider other) {
		//if (other.CompareTag ("Projectile")) {
			//Die (other.GetComponent<ProjectileController> ().damage);
			//Destroy (other.gameObject);
		//}
         if (other.CompareTag("SideWall")) {
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
	}
}