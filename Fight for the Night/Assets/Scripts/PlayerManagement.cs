using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManagement : MonoBehaviour {

	public GameObject canv; // The canvas and Text attached with this player
	public GameObject canvHPT;
	public GameObject goldT;
	public GameObject incomeT;
    public GameObject endGame;

    private EventTrigger baseHP;
	private PlayerNetwork player;
    private PlayerNetwork player2;

    public int startingGold = 50;
    public int currentIncome;
    public int currentGold;
    public int incomeInterval = 5;
    private int timeCount;

	// Controls for player reset when touching minion
	private GameObject spawnpoint;

	// Use this for initialization
	void Start () {
        // Manage Gold
        currentGold = currentGold + startingGold;
		timeCount = incomeInterval;
		player = this.GetComponent<PlayerNetwork> ();
        if (player.team == 0)
        {
            if (GameObject.FindGameObjectWithTag("Player1"))
            {
                player2 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerNetwork>();
            }           
        } else if (player.team == 1)
        {
            if (GameObject.FindGameObjectWithTag("Player0"))
            {
                player2 = GameObject.FindGameObjectWithTag("Player0").GetComponent<PlayerNetwork>();
            }
        }
		spawnpoint = GameObject.Find ("SpawnPoint" + (player.team + 1));
    }
	
	// Update is called once per frame
	void Update () {
		if (!player.local) {
			return;
		}
        UpdateCanvas();
        Income();
	}

    private void UpdateCanvas() // Update the Base HP on the canvas
    {
		canvHPT.GetComponent<Text>().text = "Remaining Health : " + player.health;    
        goldT.GetComponent<Text>().text = "Gold : " + currentGold;      
        incomeT.GetComponent<Text>().text = "Income : " + currentIncome + " Per "+incomeInterval +" seconds";
        if (player.health <= 0)
        {
            endGame.GetComponent<Text>().text = "Defeat";
        } else if (player2)
        {
            if (player2.health <= 0)
            {
                endGame.GetComponent<Text>().text = "Victory!";
            }          
        }       
    }

    private void Income()
    {
		if (timeCount <= Time.timeSinceLevelLoad)
        {
            currentGold = currentGold + currentIncome;
            timeCount = timeCount + incomeInterval;
        }
    }

	void OnTriggerEnter(Collider other) {
		if (!player.local) {
			return;
		}
		if (other.CompareTag ("Enemy")) {
			transform.position = spawnpoint.transform.position;
			this.GetComponent<playerAnimation> ().setupCamera ();
		}
	}
}
