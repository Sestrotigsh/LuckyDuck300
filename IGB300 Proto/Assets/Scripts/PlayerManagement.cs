using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManagement : MonoBehaviour {

	public GameObject canv; // The canvas and Text attached with this player
	public GameObject canvHPT;
	public GameObject goldT;
	public GameObject incomeT;

	private EventTrigger baseHP;
	private PlayerNetwork player;

    public int startingGold = 50;
    public int currentIncome;
    public int currentGold;
    public int incomeInterval = 5;
    private int timeCount;

	// Use this for initialization
	void Start () {
        // Manage Gold
        currentGold = currentGold + startingGold;
		timeCount = incomeInterval;
		player = this.GetComponent<PlayerNetwork> ();

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
    }

    private void Income()
    {
		if (timeCount <= Time.timeSinceLevelLoad)
        {
            currentGold = currentGold + currentIncome;
            timeCount = timeCount + incomeInterval;
        }
    }
}
