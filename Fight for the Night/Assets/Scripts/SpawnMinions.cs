using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnMinions : NetworkBehaviour {
////// CONTROLS PLAYERS ABILITIES TO SPAWN MONSTERS TO ATTACK OTHER PLAYER


	public GameObject enemySpawner;
    public GameObject monster1;
    public GameObject monster2;
	private PlayerManagement playerMan;
	private PlayerNetwork PlayerNet;

    public int monster1Cost;
	public int monster1IncomeBoost;
    public int monster2Cost;
	public int monster2IncomeBoost;

    //private int

	// Use this for initialization
	void Start () {
		playerMan = this.GetComponent<PlayerManagement> ();
		PlayerNet = this.GetComponent<PlayerNetwork> ();
		if (!PlayerNet.local) {
			enabled = false;
		}
		// find the enemy spawner
		int newTeam = this.GetComponent<PlayerNetwork>().team;
		enemySpawner = GameObject.FindGameObjectWithTag ("Spawn" + (newTeam+1)%2);
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown("u")) {
			//if (playerMan.currentGold >= monster1Cost) {
				//CmdSendMonster1(true, 11);
				//playerMan.currentGold -= monster1Cost;
				//playerMan.currentIncome += monster1IncomeBoost;
			//}
		//}
        // else if (Input.GetKeyDown("i")) {
            //if (playerMan.currentGold >= monster1Cost) {
               // CmdSendMonster1(true, 22);
                //playerMan.currentGold -= monster1Cost;
                //playerMan.currentIncome += monster1IncomeBoost;
           // }
       // }
		if (Input.GetKeyDown("o")) {
			if (playerMan.currentGold >= monster1Cost) {
				CmdSendMonster1();
				playerMan.currentGold -= monster1Cost;
				playerMan.currentIncome += monster1IncomeBoost;
			}
		}
        else if (Input.GetKeyDown("p")) {
            if (playerMan.currentGold >= monster2Cost) {
                CmdSendMonster2();
                playerMan.currentGold -= monster2Cost;
                playerMan.currentIncome += monster2IncomeBoost;
            }
        }
	}

	[Command]
    void CmdSendMonster1() {
		var currentMonster = Instantiate(monster1, enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        //currentMonster.GetComponent<Enemy>().customPathBool = customPath;
       // currentMonster.GetComponent<Enemy>().customPathDirection = path;
        NetworkServer.Spawn (currentMonster);
	}

	[Command]
    void CmdSendMonster2() {
		var currentMonster = Instantiate(monster2, enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        //currentMonster.GetComponent<Enemy>().customPathBool = customPath;
        //currentMonster.GetComponent<Enemy>().customPathDirection = path;
        NetworkServer.Spawn (currentMonster);
    }
}
