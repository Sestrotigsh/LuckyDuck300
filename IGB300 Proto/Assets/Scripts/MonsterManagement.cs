using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManagement : MonoBehaviour {


    private static int playerTeam = 0;
    private int thisPlayerTeam;
    public SpawnMinions spawner;

	// Use this for initialization
	void Start () {
        thisPlayerTeam = playerTeam;

        playerTeam = playerTeam + 1;
        if (playerTeam > 1)
        {
            playerTeam = 1;
        }

        spawner = GameObject.FindGameObjectWithTag("Spawn"+thisPlayerTeam).GetComponent<SpawnMinions>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("o"))
        {
            spawner.SpawnMonster1();
        } else if (Input.GetKeyDown("p")) {
            spawner.SpawnMonster2();
        }
	}
}
