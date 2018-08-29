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
	public RuntimeAnimatorController alienController;
	public RuntimeAnimatorController alienPlayer2;
	public Avatar alienAvatar;
	public RuntimeAnimatorController slasherController;
	public RuntimeAnimatorController slasherPlayer2;
	public Avatar slasherAvatar;
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
			this.GetComponent<Animator>().avatar = alienAvatar;
			if (this.GetComponent<PlayerNetwork>().local) {
				this.gameObject.GetComponent<SpellsAlien>().enabled = true;
			}
			this.GetComponent<Animator>().runtimeAnimatorController = alienController;

				
		} 
		// if the player has selected the slasher
		if (playerTeam == 1) {
			//Debug.Log("SLASHER PLAYER");
			// CHANGE ANIMATION AVATAR
			slasherClothes.SetActive(true);
			slasherChosen = true;
			this.GetComponent<Animator>().avatar = slasherAvatar;

			if (this.GetComponent<PlayerNetwork>().local) {
				this.gameObject.GetComponent<SpellsSlasher>().enabled = true;
			}
			this.GetComponent<Animator>().runtimeAnimatorController = slasherController;
		}
	}
}
