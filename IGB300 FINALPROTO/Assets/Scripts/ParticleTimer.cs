﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTimer : MonoBehaviour {

    public float timer;
    public float length;

	// Use this for initialization
	void Start () {
        timer = Time.time + length;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > timer)
        {
            Destroy(this.gameObject);
        }
	}
}
