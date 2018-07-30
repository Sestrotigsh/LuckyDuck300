using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrading : MonoBehaviour {

	public GameObject tower1;
	public GameObject tower2;
	public GameObject tower3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Die()
	{
		Destroy(this.gameObject);
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.CompareTag("TowerBase")) {
			Destroy (other.gameObject);
			Instantiate (tower1, transform.position, transform.rotation);
			Die ();
		}

//			
//			Die ();
//		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
//			if(Input.GetButtonDown ("Fire1")) {
//				if (this.tag == "TowerBase") {
//					
//					Destroy(other.gameObject);
//					Die ();
//				} else if (this.tag == "Tower1") {
//					Instantiate (tower2, transform.position, transform.rotation);
//					Destroy(other.gameObject);
//					Die ();
//				} else if (this.tag == "Tower2") {
//					Instantiate (tower3, transform.position, transform.rotation);
//					Destroy(other.gameObject);
//					Die ();
//				} 
//			}
//		}
	}
}