using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnMinions : NetworkBehaviour {
////// CONTROLS PLAYERS ABILITIES TO SPAWN MONSTERS TO ATTACK OTHER PLAYER


	public GameObject enemySpawner;

    public GameObject monster1Alien;
    public GameObject monster2Alien;
    public GameObject monster1Slasher;
    public GameObject monster2Slasher;

	private PlayerManagement playerMan;
	private PlayerNetwork PlayerNet;

    public int monster1Cost;
	public int monster1IncomeBoost;
    public int monster2Cost;
	public int monster2IncomeBoost;

    public int monster1HPBoost;
    public int monster2HPBoost;

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
				if (PlayerNet.team == 0) {
            		CmdSendMonster1Alien();
				} else if (PlayerNet.team == 1) {
					CmdSendMonster1Slasher();
				}
				playerMan.currentGold -= monster1Cost;
				playerMan.currentIncome += monster1IncomeBoost;
			}
		}
        else if (Input.GetKeyDown("p")) {
            if (playerMan.currentGold >= monster2Cost) {
            	if (PlayerNet.team == 0) {
            		CmdSendMonster2Alien();
				} else if (PlayerNet.team == 1) {
					CmdSendMonster2Slasher();
				}
                playerMan.currentGold -= monster2Cost;
                playerMan.currentIncome += monster2IncomeBoost;
            }
        }
	}

	[Command]
    void CmdSendMonster1Alien() {
		var currentMonster = Instantiate(monster1Alien, enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
		currentMonster.GetComponent<Enemy>().health = currentMonster.GetComponent<Enemy>().health + monster1HPBoost;
		monster1HPBoost = monster1HPBoost + 2;
        //currentMonster.GetComponent<Enemy>().customPathBool = customPath;
       // currentMonster.GetComponent<Enemy>().customPathDirection = path;
        NetworkServer.Spawn (currentMonster);
	}

	[Command]
    void CmdSendMonster2Alien() {
		var currentMonster = Instantiate(monster2Alien, enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
		currentMonster.GetComponent<Enemy>().health = currentMonster.GetComponent<Enemy>().health + monster2HPBoost;
        monster2HPBoost = monster2HPBoost + 2;
        //currentMonster.GetComponent<Enemy>().customPathBool = customPath;
        //currentMonster.GetComponent<Enemy>().customPathDirection = path;
        NetworkServer.Spawn (currentMonster);
    }

	[Command]
    void CmdSendMonster1Slasher() {
		var currentMonster = Instantiate(monster1Slasher, enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
		currentMonster.GetComponent<Enemy>().health = currentMonster.GetComponent<Enemy>().health + monster1HPBoost;
		monster1HPBoost = monster1HPBoost + 2;
        //currentMonster.GetComponent<Enemy>().customPathBool = customPath;
       // currentMonster.GetComponent<Enemy>().customPathDirection = path;
        NetworkServer.Spawn (currentMonster);
	}

    [Command]
    void CmdSendMonster2Slasher() {
		var currentMonster = Instantiate(monster2Slasher, enemySpawner.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
		currentMonster.GetComponent<Enemy>().health = currentMonster.GetComponent<Enemy>().health + monster2HPBoost;
        monster2HPBoost = monster2HPBoost + 2;
        //currentMonster.GetComponent<Enemy>().customPathBool = customPath;
        //currentMonster.GetComponent<Enemy>().customPathDirection = path;
        NetworkServer.Spawn (currentMonster);
    }
}
