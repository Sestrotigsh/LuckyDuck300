using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChoosePlayer : NetworkBehaviour {

	[SyncVar]
	public int character;
	private GameObject quitbutton;

	// Use this for initialization
	void Start () {
		quitbutton = GameObject.FindWithTag("QuitMenu");
		quitbutton.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
