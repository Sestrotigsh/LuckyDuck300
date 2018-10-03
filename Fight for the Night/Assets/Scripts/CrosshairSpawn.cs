using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairSpawn : MonoBehaviour {

    public GameObject alienClothes;
    public GameObject slasherClothes;
	// Use this for initialization
	void Start () {
		if (slasherClothes.activeInHierarchy == true)
        {
            this.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
