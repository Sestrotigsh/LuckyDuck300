using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaScreenMat : MonoBehaviour {
///// CONTROLS CINEMA SCREEN OBJECT - NO LONGER IN GAME

    // materials to cover screen
    public Material mat1;
    public Material mat2;
    public Material usedMat;

    // timers to control screen change
    public float timer;
    public float interval = 5;
    
	// Use this for initialization
	void Start () {
        // set the screen to first camera view
        timer = interval;
        usedMat = mat1;
	}
	
	// Update is called once per frame
	void Update () {
		// Change the screen to the next (second) viewing angle
		if (Time.time >= timer && usedMat == mat1 ) {
            this.gameObject.GetComponent<MeshRenderer>().material = mat2;
            usedMat = mat2;
            timer = Time.time + interval;
		// change the screen to the next (first) viewing angle
        } else if (Time.time >= timer && usedMat == mat2) {
            this.gameObject.GetComponent<MeshRenderer>().material = mat1;
            usedMat = mat1;
            timer = Time.time + interval;
        }
    }
}
