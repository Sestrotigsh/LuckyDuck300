using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

    public Enemy enemy;
    public string caster;
    public GameObject player;
	public int damage = 1;

	// Use this for initialization
	void Start () {
		
	}


	void Update () {
		
	}

    void OnTriggerEnter(Collider other) // Will check if an enemy is hit, and damage it accordingly
    {
        if (other.gameObject.CompareTag("Enemy0") || other.gameObject.CompareTag("Enemy1")) {
            enemy = other.gameObject.GetComponent<Enemy>();

            // If the caster is using the alien character, will modify the basic auto with the passive.
            if (caster == "Alien")
            {
                player.GetComponent<SpellsAlien>().Passive(enemy.gameObject);
            }

            enemy.Die(damage);

            if (this.gameObject.tag == ("Projectile"))
            {
                Destroy(this.gameObject);
            }


        }    

    }
}
