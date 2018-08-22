using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {
///// CONTROLS PROJECTILES
    public Enemy enemy;
    public string caster;
    public GameObject player;
	public int damage = 1;
    public int team;
    public string type = "basic";

	// Use this for initialization
	void Start () {
		
	}


	void Update () {
		
	}

    void OnTriggerEnter(Collider other) // Will check if an enemy is hit, and damage it accordingly
    {
        if (other.gameObject.CompareTag("Enemy"+team)) {
            enemy = other.gameObject.GetComponent<Enemy>();
            Debug.Log(damage);
            enemy.Die(damage);
            if (type == "Chill") {
                enemy.GetSlowed();
            }

            if (this.gameObject.tag == ("Projectile"))
            {
                Destroy(this.gameObject);
            }
        }    
    }

    IEnumerator Slow()
    {
        yield return new WaitForSeconds(3);

    }
}
