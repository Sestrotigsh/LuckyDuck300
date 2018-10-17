using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAlienBob : MonoBehaviour {

    //Variables
    float maxHeight = 0.3f;
    float minHeight = 0.0f;
    float speed = 1.0f;
    private float initialHeight;
    


    // Use this for initialization
    void Start () {
        initialHeight = transform.position.y;
        
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > (maxHeight+initialHeight)) {
            speed = speed * -1;
            minHeight = Random.Range(0.1f+initialHeight, 0.2f+initialHeight);
        } else if (transform.position.y < minHeight+initialHeight) {
            speed = speed * -1;
            maxHeight = Random.Range(0.2f+initialHeight, 0.3f+initialHeight);
        }
        //transform.Translate(0, 1, 0);
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
