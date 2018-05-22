using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

	[SyncVar]
	public Vector2 Health = new Vector2 (10,10);

	// Use this for initialization
	void Start () {
		// Start runs with menu start up
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
