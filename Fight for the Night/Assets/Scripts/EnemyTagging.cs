using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyTagging : NetworkBehaviour {
	// This class is used to control the tagging of enemies

	// The team defined when spawned
	[SyncVar]
	public int team = -1;
	[SyncVar]
	// The pathway defined when spawned
	public int path = -1;
    [SyncVar]
    public int waveNumber;

	// booleans used to stop tags being repeatedly changed in update
	private bool teamDefined = false;
	private bool pathDefined1 = false;
	private bool pathDefined2 = false;


	void Update() {
		// if team hasn't been tagged any time
		if (teamDefined == false) {
			// if the team has been changed from the default of -1
			if (team > -1) {
				this.tag = "Enemy" + team;
				this.GetComponent<Enemy>().teamReady = true;
				teamDefined = true;
			}
		}

		// if path hasn't been set at all
		if (pathDefined1 == false) {
			// if the path has been changed from the default of -1
			if (path > -1) {
				this.GetComponent<Enemy>().pathReady = true;
				pathDefined1 = true;
			}
		}
		
		// if the monster path hasn't been set at all
		if (pathDefined2 == false) {
			// if the path has been defined as -10 it means the enemy is a monster
			if (path < -9) {
				this.GetComponent<Enemy>().monsterReady = true;
				pathDefined2 = true;
			}
		}
	}
}
