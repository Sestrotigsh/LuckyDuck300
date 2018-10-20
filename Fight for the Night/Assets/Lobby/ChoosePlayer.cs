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
	public GameObject AlienSpell1;
	public GameObject AlienSpell2;
	public GameObject SlasherSpell1;
	public GameObject SlasherSpell2;
	public GameObject Loading;

	// Use this for initialization
	void Start () {
		Loading = GameObject.FindWithTag("Loading");
		if (Loading != null) {
			Loading.SetActive(false);
		}
		
		//quitbutton = GameObject.FindWithTag("QuitMenu");
		//quitbutton.SetActive(false);
		// if the player has selected the alien
		if (playerTeam == 0) {
			//Debug.Log("ALIEN PLAYER");
			// CHANGE ANIMATION AVATAR
			alienClothes.SetActive(true);
			alienChosen = true;
			this.GetComponent<Animator>().avatar = alienAvatar;
			this.gameObject.GetComponent<SpellsAlien>().enabled = true;
			if (this.GetComponent<PlayerNetwork>().local) {
				AlienSpell1.SetActive(true);
				AlienSpell2.SetActive(true);
				this.GetComponent<Animator>().runtimeAnimatorController = alienController;
			} else {
				this.GetComponent<Animator>().runtimeAnimatorController = alienController;
				//this.GetComponent<Animator>().runtimeAnimatorController = alienPlayer2;
			}
			
				
		} 
		// if the player has selected the slasher
		if (playerTeam == 1) {
			//Debug.Log("SLASHER PLAYER");
			// CHANGE ANIMATION AVATAR
			slasherClothes.SetActive(true);
			slasherChosen = true;
			this.GetComponent<Animator>().avatar = slasherAvatar;
			this.gameObject.GetComponent<SpellsSlasher>().enabled = true;
			if (this.GetComponent<PlayerNetwork>().local) {
				SlasherSpell1.SetActive(true);
				SlasherSpell2.SetActive(true);
				this.GetComponent<Animator>().runtimeAnimatorController = slasherController;
			} else {
				this.GetComponent<Animator>().runtimeAnimatorController = slasherController;
				//this.GetComponent<Animator>().runtimeAnimatorController = slasherPlayer2;
			}
			
		}
	}
}
