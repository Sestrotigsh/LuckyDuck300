using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour {

    public Quaternion iniRot;
    public GameObject playerChar;
    public int playerTeam;
	// Use this for initialization
	void Start () {
        playerTeam = gameObject.GetComponentInParent<Enemy>().team;
        playerChar = GameObject.FindGameObjectWithTag("Player" + playerTeam);
    }
	
	// Update is called once per frame
	
    void LateUpdate()
    {
        Vector3 playerDir = new Vector3(playerChar.transform.position.x, this.transform.position.y, playerChar.transform.position.z);
        this.gameObject.transform.LookAt(playerDir);
    }
}
