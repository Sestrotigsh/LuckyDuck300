using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupScript : MonoBehaviour {

	public GameObject StartMenu;

	// Use this for initialization
	void Start () {
		StartMenu = GameObject.FindWithTag("Startup");
		StartMenu.SetActive(true);
		Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
