using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChoosePlayer : NetworkBehaviour {

	[SyncVar]
	public int playerTeam;
	//public GameObject quitbutton;
	public GameObject alienClothes;
	public GameObject slasherClothes;
	public bool slasherChosen = false;
	public bool alienChosen = false;

	// Use this for initialization
	void Start () {
		//quitbutton = GameObject.FindWithTag("QuitMenu");
		//quitbutton.SetActive(false);
		// if the player has selected the alien
		if (playerTeam == 0) {
			//Debug.Log("ALIEN PLAYER");
			// CHANGE ANIMATION AVATAR
			alienClothes.SetActive(true);
			alienChosen = true;
			if (this.GetComponent<PlayerNetwork>().local) 
				this.gameObject.GetComponent<SpellsAlien>().enabled = true;
				
			
			

		} 
		// if the player has selected the slasher
		if (playerTeam == 1) {
			//Debug.Log("SLASHER PLAYER");
			// CHANGE ANIMATION AVATAR
			slasherClothes.SetActive(true);
			slasherChosen = true;
			if (this.GetComponent<PlayerNetwork>().local) 
				this.gameObject.GetComponent<SpellsSlasher>().enabled = true;

			
			

		}
	}
}
