using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
	public int health = 10;
	public TextMesh textAbove;

	// Use this for initialization
	void Start()
	{

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