using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaScreenMat : MonoBehaviour {

    public Material mat1;
    public Material mat2;

    public Material usedMat;

    public float timer;
    public float interval = 5;
    
	// Use this for initialization
	void Start () {
        timer = interval;
        usedMat = mat1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= timer && usedMat == mat1 )
        {
            this.gameObject.GetComponent<MeshRenderer>().material = mat2;
            usedMat = mat2;
            timer = Time.time + interval;
        } else if (Time.time >= timer && usedMat == mat2)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = mat1;
            usedMat = mat1;
            timer = Time.time + interval;
        }

    }
}
