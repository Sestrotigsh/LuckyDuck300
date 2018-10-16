using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerNetwork : NetworkBehaviour {
////// CONTROLS THE PLAYERS NETWORKING VARIABLES
	public float centreLineX;
	public int team;
	public bool local;

	public GameObject opponent;
	public bool opponentFound;

	[SyncVar]
	public int health;
	[SyncVar]
	public bool winner = false;
	[SyncVar]
	public bool loser = false;

	// Use this for initialization
	void Start () {
		// Check which side of the map the player is on and then set which team they are on
		if (this.transform.position.x < centreLineX) {
			team = 0;
		} else {
			team = 1;
		}

		tag = ("Player" + team);
		opponent = GameObject.FindGameObjectWithTag ("Player" + (team + 1) % 2);
		//if (opponent != null) {
			//opponent.GetComponent<PlayerManagement>().canv.SetActive(false);
		//}
		//if (opponent != null) {
			//if (opponent.transform.Find("AlienClothes").gameObject.activeSelf == true) {
				//this.gameObject.GetComponent<EnemyWavesOffline>().enabled = false;
				//this.gameObject.GetComponent<EnemyWavesAlien>().enabled = true;
			//} else if (opponent.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
				//this.gameObject.GetComponent<EnemyWavesSlasher>().enabled = true;
				//this.gameObject.GetComponent<EnemyWavesOffline>().enabled = false;
			//}
		//}
		// Check and store if the player is an actual player or just transform data from a player on another machine
		if (isLocalPlayer) {
			local = true;
		} else {
			local = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (opponent == null) {
			opponent = GameObject.FindGameObjectWithTag ("Player" + (team + 1) % 2);
			
		}
		if (team == 0 && this.transform.position.x > centreLineX) {
			Vector3 hardReset = transform.position;
			hardReset.x = centreLineX - 1.0f;
			transform.position = hardReset;
		} else if (team == 1 && this.transform.position.x < centreLineX) {
			Vector3 hardReset = transform.position;
			hardReset.x = centreLineX + 1.0f;
			transform.position = hardReset;
		}

		if (local && opponent != null) {
			if (winner == true || opponent.GetComponent<PlayerNetwork>().loser == true) {
					GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
					//opponent.GetComponent<PlayerNetwork>().DeclareLoser();
					if (this.transform.Find("AlienClothes").gameObject.activeSelf == true) {
						SceneManager.LoadScene("VictoryAlien", LoadSceneMode.Additive);
					} else if (this.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
						SceneManager.LoadScene("VictorySlasher", LoadSceneMode.Additive);
					}
					Cursor.visible = true;
					this.GetComponent<EnemyWaves>().enabled = false;
					this.GetComponent<PropsSpawn>().DeactivateProps();
					this.GetComponent<AudioListener>().enabled = false;
					opponent.SetActive(false);
					this.gameObject.SetActive(false);

					opponent.SetActive(false);
			}
			if (loser == true || opponent.GetComponent<PlayerNetwork>().winner == true) {
					GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
					//opponent.GetComponent<PlayerNetwork>().DeclareWinner();
					if (opponent.transform.Find("AlienClothes").gameObject.activeSelf == true) {
						SceneManager.LoadScene("DefeatAlien", LoadSceneMode.Additive);
					} else if (opponent.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
						SceneManager.LoadScene("DefeatSlasher", LoadSceneMode.Additive);
					}
					Cursor.visible = true;
					this.GetComponent<EnemyWaves>().enabled = false;
					this.GetComponent<PropsSpawn>().DeactivateProps();
					this.GetComponent<AudioListener>().enabled = false;
					opponent.SetActive(false);
					this.gameObject.SetActive(false);
			}
			if (health <= 0) {
				DeclareLoser();
			}
		}
	}

	public void TakeDamage () {
		if (isServer) {
			health -= 1;
		}
		



		/*
		if (isServer) {
			RpcUpdateHealth (health);
		} else {
			CmdUpdateHealth (health);
		}
		*/
				/*
				// If the player is on the server - tell the client to win
				if (isServer) {
					if (opponent.transform.Find("AlienClothes").gameObject.activeSelf == true) {
						RpcVictoryAlien();
					} else if (opponent.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
						RpcVictorySlasher();
					}
			// if the player is a client - tell the server to win
				} else {
					if (opponent.transform.Find("AlienClothes").gameObject.activeSelf == true) {
						CmdVictoryAlien();
					} else if (opponent.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
						CmdVictorySlasher();
					}
				}
				// end in defeat
				if (opponent.transform.Find("AlienClothes").gameObject.activeSelf == true) {
					SceneManager.LoadScene("DefeatAlien", LoadSceneMode.Single);
				} else if (opponent.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
					SceneManager.LoadScene("DefeatSlasher", LoadSceneMode.Single);
				}
				*/
	}

	public void EnemyDie(GameObject enemy) {
		NetworkInstanceId enemyID = enemy.GetComponent<NetworkIdentity>().netId;
		CmdKillEnemy(enemyID);
	}

	[Command]
	void CmdKillEnemy (NetworkInstanceId ID) {
		GameObject target = NetworkServer.FindLocalObject(ID);
        NetworkServer.Destroy(target);
	}












	public void DeclareWinner() {
		winner = true;
		if (!isServer) {
			CmdDelcareWinner();
		}
	}	

	public void DeclareLoser() {
		loser = true;
		if (!isServer) {
			CmdDelcareLoser();
		}
	}	

	[Command]
	void CmdDelcareWinner() {
		winner = true;
	}

	[Command]
	void CmdDelcareLoser() {
		loser = true;
	}

}
