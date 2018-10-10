using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAlienBob : MonoBehaviour {

    //Variables
    float maxHeight = 1.0f;
    float minHeight = 0.0f;
    float speed = 1.0f;
    


    // Use this for initialization
    void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > maxHeight) {
            speed = speed * -1;
            minHeight = Random.Range(0.0f, 0.5f);
        } else if (transform.position.y < minHeight) {
            speed = speed * -1;
            maxHeight = Random.Range(0.5f, 1.0f);
        }
        //transform.Translate(0, 1, 0);
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
