using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCollider : MonoBehaviour {
///// CONTROLS PROJECTILES AND ENEMIES DIRECT INTERACTION WITH ENVIRONMENT

	/// <summary>
	/// controls what happens when player spells or projectiles collide with the in game geometry
	/// </summary>
	/// <param name="other">The players ability hitting the geometry</param>
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == ("Projectile") || other.gameObject.tag == ("Spell")) {            
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == ("Enemy")) {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
