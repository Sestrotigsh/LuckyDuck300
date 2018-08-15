using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChoosePlayer : NetworkBehaviour {

	[SyncVar]
	public int playerSelected;
	private GameObject quitbutton;

	// Use this for initialization
	void Start () {
		quitbutton = GameObject.FindWithTag("QuitMenu");
		quitbutton.SetActive(false);
		// if the player has selected the alien
		if (playerSelected == 1) {
			// SPAWN THE ALIEN SHIT
		} 
		// if the player has selected the slasher
		if (playerSelected == 2) {
			// SPAWN THE SLASHER SHIT
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
