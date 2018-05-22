using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EventTrigger : NetworkBehaviour
{
	public int health = 10;
	public TextMesh textAbove;
	// Use this for initialization
	void Start()
	{
		if (this.CompareTag ("Base" + 0)) {
			int team = 0;
		} else if (this.CompareTag ("Base" + 1)) {
			int team = 1;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy1" || other.tag == "Enemy0")
		{
			other.tag = "Dying Enemy";
			if (health > 0)
            {
                health = health - 1;
            } else
            {
                health = 0;
            }
			
			textAbove.text = ""+health;
		}
	}   

}