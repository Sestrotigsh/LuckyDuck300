﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsAlien : MonoBehaviour
{
///// SPELLS FOR THE ALIEN

	// Find the shooting position of each character. Currently at the front
	private GameObject canv; // The canvas and Text attached with this player 
	private GameObject spell1T; // Text of spell1
	private GameObject spell2T; 

	public GameObject spell1object;
	public Transform frontPos;
	private int team;

	// Audio part
	AudioSource audioS;
	public AudioClip laserSound;
	public AudioClip repulsionSound;
	public AudioClip basicSound;

	// Passive
	private GameObject lastHit = null;

	//Auto attack
	public GameObject projectile;
	public int baseAuto;
	public int autoDamage;
	public float fireRate = 0.4f;
	private float fireTimer = 0.0f;
	

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
	private bool isComboable = false;

	// Spell 3
	public List<Enemy> enemyList;
    public int CD3;
    public float CDTimer3 = 0;
    public float remainingTime3 = 0;
    public ParticleSystem alienExecute;

	// Spell 4
	public GameObject spell11object;
    private float laserGap = 2;
    private float laserGapTimer;
    private float CDTimer11 = 0; // Next time the spell available
    public int CD11; // The actual CD between spell
    public float remainingTime11 = 0; // The number of seconds left before the next cast available

    // Recoil
    public float accuracyCounter; // determines how much to distort accuracy
    public float maxAccuracyDistortion; // the maximum amount of accuracy distortion
    public Text Crosshairs;

	// Use this for initialization
	void Start() {
		accuracyCounter = 0.0f;
		if (!this.GetComponent<PlayerNetwork>().local) {
			return;
		}

		if (this.tag == "Player0")
        {
            team = 0;
        }
        else
        {
            team = 1;
        }

		audioS = GetComponent<AudioSource>();

		canv = this.transform.Find("Canvas").gameObject;
		spell1T = canv.transform.Find("Spell1(Alien)").gameObject.transform.Find("Text").gameObject;
		spell2T = canv.transform.Find("Spell2(Alien)").gameObject.transform.Find("Text").gameObject;
		autoDamage = baseAuto;
		foreach (Transform child in transform) if (child.CompareTag("ShootingPoint")) {
			if (child.tag != "GameController") {
				frontPos = child;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (!this.GetComponent<PlayerNetwork>().local) {
			return;
		}
		
		if (accuracyCounter > 0.0f) {
			accuracyCounter -= Time.deltaTime * 0.2f;
		}

		if (accuracyCounter <= 0.0f) {
			Crosshairs.text = "          |\n        ~ ~\n          |";
		} else if (accuracyCounter > 0.75f) {
			Crosshairs.text = "          |\n\n   ~           ~\n\n          |";
		} else if (accuracyCounter > 0.5f && accuracyCounter < 0.65f) {
			Crosshairs.text = "          |\n\n    ~        ~\n\n          |";
		} else if (accuracyCounter > 0.25f && accuracyCounter < 0.4f) {
			Crosshairs.text = "          |\n       ~   ~\n          |";
		} 







		
		
		if (!(Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))) {
			if (Time.time >= CDTimer1 && Input.GetKeyDown ("1")) {
                if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("pushing") == true) {
                    return;
                }
                StartCoroutine(Spell1 ());

			} else if (Time.time >= CDTimer2 && Input.GetKeyDown ("2")) {
				Spell2 ();
			} else if (Time.timeSinceLevelLoad >= CDTimer3 && Input.GetKeyDown("2") && isComboable == true) {
				ComboRepulsion();
			} else if (Time.time >= CDTimer11 && Input.GetKeyDown("1") && Time.time < laserGapTimer)
            {
                StartCoroutine(Spell1Combo());
            }
			if (Input.GetMouseButton (0)) {
				if (fireTimer < Time.timeSinceLevelLoad) {
                    if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("pushing") == true) {
                        return;
                    }
					BasicAttack ();





					fireTimer = fireRate + Time.timeSinceLevelLoad;
				}
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
		///// INSTANTIATE BASED ON PERCENTAGE OF COUNTER FILLED AS PERCENTAGE OF ACCURACY DISTORTION
		if (accuracyCounter < 1.0f) {
			accuracyCounter += 0.175f + (Time.deltaTime * 0.2f);	
		}
		


		Vector3 adjustedAccuracy = new Vector3 (Random.Range(-1*accuracyCounter*maxAccuracyDistortion,accuracyCounter*maxAccuracyDistortion),Random.Range(-1*accuracyCounter*maxAccuracyDistortion,accuracyCounter*maxAccuracyDistortion),Random.Range(-1*accuracyCounter*maxAccuracyDistortion,accuracyCounter*maxAccuracyDistortion));
		GameObject instance = Instantiate(projectile, (frontPos.position), this.transform.rotation) as GameObject;
		instance.transform.Rotate(adjustedAccuracy);
		instance.GetComponent<ProjectileController>().damage = baseAuto;
		instance.GetComponent<ProjectileController>().caster = "Alien";
		instance.GetComponent<ProjectileController>().player = this.gameObject;
		instance.GetComponent<ProjectileController>().team = team;
		instance.GetComponent<Rigidbody>().AddForce(instance.transform.forward * power);       
		Destroy(instance, 1.0f);
		audioS.clip = basicSound;
		audioS.Play();
	}

	IEnumerator Spell1() // Spell 1 of the Alien, the laser
	{
		//Timing Management
		CDTimer1 = Time.time + CD1;
		remainingTime1 = CD1;
		laserGapTimer = Time.time + laserGap;
		
		this.GetComponent<playerAnimation>().BigShoot();
        yield return new WaitForSeconds(0.5f);
		audioS.clip = laserSound;
		audioS.Play();
		GameObject instance = Instantiate(spell1object, frontPos.transform.position, this.transform.rotation);

		instance.GetComponent<ProjectileController>().damage = spell1Damage;
		instance.GetComponent<ProjectileController>().caster = "Alien";
		instance.GetComponent<ProjectileController>().player = this.gameObject;
		instance.GetComponent<ProjectileController>().team = team;
		instance.GetComponent<Rigidbody>().AddForce(this.transform.forward * power);       
		audioS.clip = laserSound;
		audioS.Play();

		// Destroy
		Destroy(instance, 2.5f);
		yield break;
	}

	    IEnumerator Spell1Combo() // Spell 1 of the Alien, the laser
    {
	//Timing Management
        CDTimer11 = Time.time + CD11;
        remainingTime11 = CD11;

    	this.GetComponent<playerAnimation>().BigShoot();
        yield return new WaitForSeconds(0.5f);

        GameObject instance = Instantiate(spell11object, frontPos.transform.position, this.transform.rotation);
        instance.GetComponent<ProjectileController>().damage = spell1Damage;
        instance.GetComponent<ProjectileController>().caster = "Alien";
		instance.GetComponent<ProjectileController>().player = this.gameObject;
        instance.GetComponent<ProjectileController>().team = team;
        instance.GetComponent<ProjectileController>().slowValue = 1;
        instance.GetComponent<Rigidbody>().AddForce(this.transform.forward * power);


        // Destroy
        Destroy(instance, 2.5f);
        yield break;
    }

	private void Spell2() { // Spell 2 Celestial push

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);        
		Vector3 directionPush;
		Rigidbody rbody;
		ParticleSystem partInstance = Instantiate(windSpin, transform);

		audioS.clip = repulsionSound;
		audioS.Play();
        this.GetComponent<playerAnimation>().ForcePush();

        int i = 0;
		while (i < hitColliders.Length)
		{
			if (hitColliders[i].tag == "Enemy"+team)
			{
				Enemy enemy;
				directionPush = new Vector3( hitColliders[i].transform.position.x - transform.position.x, 0, hitColliders[i].transform.position.z - transform.position.z);
				rbody = hitColliders[i].GetComponent<Rigidbody>();
				enemy = hitColliders[i].GetComponent<Enemy>();
				enemyList.Add(enemy);
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
		isComboable = true;
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
			isComboable = false;
			yield break;
		}
	}

	 private void ComboRepulsion()
    {

        for (int i = 0; i < enemyList.Count; i++)
        {
            if(enemyList[i] != null)
            {
                ParticleSystem partInstance = Instantiate(alienExecute, new Vector3(enemyList[i].transform.position.x, enemyList[i].transform.position.y + 0.7f, enemyList[i].transform.position.z), Quaternion.Euler(-90, 0, 0));
                enemyList[i].Die(999999999);
            }
           
        }
        enemyList.Clear();
        CDTimer3 = Time.timeSinceLevelLoad + CD3;
        remainingTime3 = CD3;
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
