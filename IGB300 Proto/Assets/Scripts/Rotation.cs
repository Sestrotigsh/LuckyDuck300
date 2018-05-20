using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    private Vector3 curLoc;
    private Vector3 prevLoc;
    private Vector3 newAngle;
    public float lookSpeed = 10;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Rotate();
    }

    private void Rotate()
    {
        prevLoc = curLoc;
        curLoc = transform.position;
        newAngle = new Vector3(prevLoc.x - transform.position.x, prevLoc.y - transform.position.y, prevLoc.z - transform.position.z);
        if (newAngle != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(prevLoc - transform.position), Time.fixedDeltaTime * lookSpeed);
        }
    }
}
