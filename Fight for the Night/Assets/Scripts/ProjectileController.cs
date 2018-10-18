using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {
///// CONTROLS PROJECTILES
    public Enemy enemy;
    public string caster;
    public GameObject player;
	public int damage = 1;
    public int team;
    public enum type
    {
        Basic,
        Chill,
        Explosion,
        ChillExplosion
    };
    public type currentType;
    // Slow value work as follow : 1 : 0% slow, 2 : 50% slow, 3 : 66% slow etc.
    public float slowValue;
    public int duration;

    //AOE values
    public int aoeSize;


	// Use this for initialization
	void Start () {
		
	}


	void Update () {
		
	}

    void OnTriggerEnter(Collider other) // Will check if an enemy is hit, and damage it accordingly
    {
        if (other.gameObject.CompareTag("Enemy"+team)) {
            enemy = other.gameObject.GetComponent<Enemy>();

             // Slow
            if (currentType == type.Basic)
            {
                // Deals the damages
                enemy.Die(damage);
            }
            else if (currentType == type.Chill)
            {
                enemy.GetSlowed(slowValue, duration);
                // Deals the damages
                enemy.Die(damage);

            }
            // AUE Damages
            else if (currentType == type.Explosion)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, aoeSize);
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].tag == "Enemy" + team)
                    {
                        Enemy enemy;
                        enemy = hitColliders[i].GetComponent<Enemy>();
                        // Deals the damages
                        enemy.Die(damage);
                    }
                }
            // AOE SLOW
            } else if (currentType == type.ChillExplosion)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, aoeSize);
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i].tag == "Enemy" + team)
                    {
                        Enemy enemy;
                        enemy = hitColliders[i].GetComponent<Enemy>();
                        enemy.GetSlowed(slowValue, duration);
                        // Deals the damages
                        enemy.Die(damage);
                    }
                }
            }

            if (this.gameObject.tag == ("Projectile"))
            {
                Destroy(this.gameObject);
            }
        }  else if (other.gameObject.CompareTag("Environment"))
        {
          
            Destroy(this.gameObject);
        }  
    }

    IEnumerator Slow()
    {
        yield return new WaitForSeconds(3);

    }
}
