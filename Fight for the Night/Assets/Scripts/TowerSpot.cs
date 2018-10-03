using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour {

    public bool isOccupied = false;
    public GameObject towerOn;
    public enum type
    {
        Unplaced,
        Base1,
        Base2,
        Base3,
        Base4,
        Ice1,
        Ice2,
        Ice3,
        Ice4,
        Bomb1,
        Bomb2,
        Bomb3,
        Bomb4,
        loading,
    };

    public type currentType;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
