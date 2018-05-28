using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsAlien : MonoBehaviour
{

	// Find the shooting position of each character. Currently at the front
	private GameObject canv; // The canvas and Text attached with this player 
	private GameObject spell1T; // Text of spell1
	private GameObject spell2T; 

	public GameObject spell1object;
	public Transform frontPos;

	// Audio part
	AudioSource audioS;
	public AudioClip laserSound;
	public AudioClip repulsionSound;

	// Passive
	private GameObject lastHit = null;

	//Auto attack
	public GameObject projectile;
	public int baseAuto;
	private int autoDamage;

	// Spell 1
	float power = 1000;
	public int spell1Damage = 10;
	private float CDTimer1 =0; // Next time the spell available
	public int CD1; // The actual CD between spell
	public float remainingTime1 = 0; // The number of seconds left before the next cast available

	// Spell 2
	private Rigidbody rb;
	float power2 = 2500;
	public int spell2Damage = 10;
	private float CDTimer2 = 0; // Next time the spell available
	public int CD2; // The actual CD between spell
	public float remainingTime2 = 0; // The number of seconds left before the next cast available
	public ParticleSystem windSpin;


	// Spell 3
	// Spell 4


	// Use this for initialization
	void Start() {
		if (!this.GetComponent<PlayerNetwork>().local) {
			return;
		}

		audioS = GetComponent<AudioSource>();

		canv = this.transform.Find("Canvas").gameObject;
		spell1T = canv.transform.Find("Spell1").gameObject.transform.Find("Text").gameObject;
		spell2T = canv.transform.Find("Spell2").gameObject.transform.Find("Text").gameObject;
		autoDamage = baseAuto;
	}

	// Update is called once per frame
	void Update()
	{
		if (!this.GetComponent<PlayerNetwork>().local) {
			return;
		}
		if (!(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
		{
			if (Time.time >= CDTimer1 && Input.GetKeyDown("1"))
			{
				Spell1();

			}
			else if (Time.time >= CDTimer2 && Input.GetKeyDown("2"))
			{
				Spell2();
			}
			else if (Input.GetMouseButtonDown(0))
			{
				BasicAttack();
			}
		}
		RemainingTime();
	}

	public void Passive(GameObject obj)
	{
		if (obj == lastHit)
		{
			autoDamage = autoDamage + 2;
		} else
		{
			autoDamage = baseAuto;
		}
	}

	private void BasicAttack()
	{
		GameObject instance = Instantiate(projectile, frontPos.position, frontPos.rotation) as GameObject;
		instance.GetComponent<ProjectileController>().damage = autoDamage;
		instance.GetComponent<ProjectileController>().caster = "Alien";
		instance.GetComponent<ProjectileController>().player = this.gameObject;
		instance.GetComponent<Rigidbody>().AddForce(frontPos.transform.forward * power);        
		Destroy(instance, 1.0f);
	}

	private void Spell1() // Spell 1 of the Alien, the laser
	{
		audioS.clip = laserSound;
		audioS.Play();
		GameObject instance = Instantiate(spell1object, frontPos.transform.position, frontPos.transform.rotation);
		instance.GetComponent<ProjectileController>().damage = spell1Damage;
		instance.GetComponent<Rigidbody>().AddForce(frontPos.transform.forward * power);

		//Timing Management
		CDTimer1 = Time.time + CD1;
		remainingTime1 = CD1;

		// Destroy
		Destroy(instance, 2.5f);
	}

	private void Spell2() { // Spell 2 Celestial push

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);        
		Vector3 directionPush;
		Rigidbody rbody;
		ParticleSystem partInstance = Instantiate(windSpin, transform);

		audioS.clip = repulsionSound;
		audioS.Play();

		int i = 0;
		while (i < hitColliders.Length)
		{
			if (hitColliders[i].tag == "Enemy1" || hitColliders[i].tag == "Enemy0")
			{
				Enemy enemy;
				directionPush = new Vector3( hitColliders[i].transform.position.x - transform.position.x, 0, hitColliders[i].transform.position.z - transform.position.z);
				rbody = hitColliders[i].GetComponent<Rigidbody>();
				enemy = hitColliders[i].GetComponent<Enemy>();
				enemy.Stun(3);
				rbody.AddForce(directionPush.normalized * power2);

				StartCoroutine(Wait(rbody, directionPush, enemy, partInstance));

			}
			i++;
		}

		//Timing Management
		CDTimer2 = Time.time + CD2;
		remainingTime2 = CD2;
	}

	IEnumerator Wait(Rigidbody rbody, Vector3 push, Enemy enemy, ParticleSystem part)
	{ // Time associated with spell 2
		yield return new WaitForSeconds(2);
		if (rbody != null)
		{
			if (rbody.velocity != Vector3.zero)
			{
				rbody.velocity = Vector3.zero;
				rbody.angularVelocity = Vector3.zero;
				enemy.Die(spell2Damage);
			}
			else
			{
				enemy.Die(spell2Damage);
			}
		}

	}

	private void RemainingTime() // Calculate the remaining time before next use, which will be used for the UI
	{
		if (remainingTime1 > 0)
		{
			remainingTime1 = CDTimer1 - Time.time;
			spell1T.GetComponent<Text>().text = remainingTime1.ToString("F2");
			if (remainingTime1 < 0)
			{
				remainingTime1 = 0;
				spell1T.GetComponent<Text>().text = "";
			}         
		}

		if (remainingTime2 > 0)
		{
			remainingTime2 = CDTimer2 - Time.time;
			spell2T.GetComponent<Text>().text = remainingTime2.ToString("F2");
			if (remainingTime2 < 0)
			{
				remainingTime2 = 0;
				spell2T.GetComponent<Text>().text = "";
			}
		}
	}
}
