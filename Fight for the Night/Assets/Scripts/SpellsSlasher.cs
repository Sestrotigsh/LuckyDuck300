﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsSlasher : MonoBehaviour
{
///// SPELLS FOR THE SLASHER

    private GameObject canv; // The canvas and Text attached with this player 

    // Find the shooting position of each character. Currently at the front
    public Transform frontPos;

    // Audio part
    [Header("Audio Components")]
    public AudioClip spell1Sound;
    public AudioClip spell2Sound;
    public AudioClip spell1ComboSound;
    public AudioClip basicSound;
    AudioSource audioS;

    // Passive
    private int team;

    //Auto attack
    [Header("Basic Attack Components")]
   
   
    public int autoDamage;
    public float fireRate = 0.4f;
    private float fireTimer = 0.0f;
    

    // Spell 1
    [Header("Spell 1 Components")]

    private GameObject spell1Text; // Text of spell1
    public int spell1Damage = 4;
    private int numberOfHits = 6;
    private int currentHit = 0;
    private int currentDamages;

    private float CDTimer1; // Next time the spell available
    public int CD1 = 12; // The actual CD between spell
    public float remainingTime1; // The number of seconds left before the next cast available

    private bool isCutting = false;

    // Spell 2
    [Header("Spell 2 Components")]

    public int spell2Damage = 25;
    private float CDTimer2; // Next time the spell available
    public int CD2 = 10; // The actual CD between spell
    public float remainingTime2; // The number of seconds left before the next cast available
    bool isDashing = false;
    private GameObject spell2Text;

    // Spell 3
    private int damageMult = 0;
    public int multValue;
    public int spell3Damage;
    private bool isSpinning = false;
    public ParticleSystem spinDashEffect;

    // SPell 4 : Battle cry
    private float damageBoost = 0;
    public float damageBoostValue = 0.2f;
    private List<GameObject> enemyToSlow = new List<GameObject>();
    private bool isCrying = false;

    // Use this for initialization
    void Start()
    {
       
        if (!this.GetComponent<PlayerNetwork>().local)
        {
            return;
        }

        if (this.tag == "Player0")
        {
            team = 0;
        } else
        {
            team = 1;
        }

        audioS = GetComponent<AudioSource>();

        canv = this.transform.Find("Canvas").gameObject;
        spell1Text = canv.transform.Find("Spell1").gameObject.transform.Find("Text").gameObject;
        spell2Text = canv.transform.Find("Spell2").gameObject.transform.Find("Text").gameObject;
        foreach (Transform child in transform) if (child.CompareTag("ShootingPoint")) {
            frontPos = child;
        }
   
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<PlayerNetwork>().local)
        {
            return;
        }
        if (!(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            if (Time.timeSinceLevelLoad >= CDTimer1 && Input.GetKeyDown("1"))
            {
                Spell1();

            }
            else if (Time.timeSinceLevelLoad >= CDTimer2 && Input.GetKeyDown("2"))
            {
                Spell2();

            }
            else if (Input.GetMouseButton(0) && isCutting == false)
            {
                if (fireTimer < Time.timeSinceLevelLoad)
                {
                    BasicAttack();
                    fireTimer = fireRate + Time.timeSinceLevelLoad;
                }
            } else if (Input.GetKeyDown("2") && isDashing == true)
            {
                isSpinning = true;
            } else if (Input.GetKeyDown("1") && isCutting == true)
            {
                isCrying = true;
            }
        }

        if (isCutting == true)
        {
            Spell1();
        }
        RemainingTime();
    }

    public void Passive(GameObject obj)
    {


    }

    private void BasicAttack()
    {
        audioS.clip = spell1Sound;
        //audioS.Play();
        Collider[] hitColliders = Physics.OverlapBox(frontPos.position, new Vector3(1, 1, 1), Quaternion.identity); ///  IS THE HALF FROM CENTRE

        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Enemy"+team)
            {
                Enemy enemy;
                enemy = hitColliders[i].GetComponent<Enemy>();
                enemy.Die(autoDamage);
            }

            i++;
        }
        audioS.Play();

    }

    private void Spell1() // Spell 1 of the Slasher, the multislash
    {
        
        audioS.clip = spell1Sound;
        this.GetComponent<playerAnimation>().BigShoot();
        // Time of the next hit in the spell 1
        currentDamages = currentHit * 2 + spell1Damage;

        if (currentHit < numberOfHits)
        {
            if (Time.timeSinceLevelLoad >= CDTimer1)
            {
                isCutting = true;
                audioS.Play();
                Collider[] hitColliders = Physics.OverlapBox(frontPos.position, new Vector3(2, 2, 2), Quaternion.identity);

                int i = 0;
                while (i < hitColliders.Length)
                {
                    if (hitColliders[i].tag == "Enemy"+team)
                    {
                        Enemy enemy;
                        enemy = hitColliders[i].GetComponent<Enemy>();
                        enemy.Die(currentDamages);
                    }

                    i++;
                }
                currentHit = currentHit + 1;

                //Timing Management
                CDTimer1 = (float)(Time.timeSinceLevelLoad + 0.5);
                
            }
            
        }

        if (currentHit == 6)
        {
            //Timing Management
            CDTimer1 = Time.timeSinceLevelLoad + CD1;
            remainingTime1 = CD1;
            isCutting = false;
            
            currentHit = 0;
             if (isCrying == true)
            {
                StartCoroutine(CryWait());
            }
        }
    }

    private void Spell2()
    {
        this.GetComponent<playerAnimation>().ForcePush();
        audioS.clip = spell2Sound;
        audioS.Play();
        isDashing = true;
        Rigidbody rbody = this.gameObject.GetComponent<Rigidbody>();
        rbody.AddForce(transform.forward.normalized * 2000);

        //Timing Management
        CDTimer2 = Time.timeSinceLevelLoad + CD2;
        remainingTime2 = CD2;

        StartCoroutine(Wait(rbody));

       

    }

    IEnumerator Wait(Rigidbody rbody)
    { // Time associated with spell 2
        yield return new WaitForSeconds((float)0.4);
        if (rbody != null)
        {
            if (rbody.velocity != Vector3.zero)
            {
                rbody.velocity = Vector3.zero;
                rbody.angularVelocity = Vector3.zero;
            
            }
            if (isSpinning == true)
           {
               ComboSpin();
           }
            isSpinning = false;
            isDashing = false;
        }
        yield break;
    }

    private void ComboSpin()
    {
        ParticleSystem partInstance = Instantiate(spinDashEffect, transform);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 8);

        for (int i =0; i< hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Enemy"+team)
            {
                Enemy enemy;
                enemy = hitColliders[i].GetComponent<Enemy>();
                enemy.Die(spell3Damage + (damageMult * multValue));
                damageMult = 0;
            }

        }
    }

    private void BattleCry()
    {
    audioS.clip = spell1ComboSound;
    audioS.Play();
    damageBoost = damageBoostValue;
    if (this.tag == "Player0")
    {
        foreach( GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy0"))
        {
            enemyToSlow.Add(enemy);
        }
        } else
        {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy1"))
        {
            enemyToSlow.Add(enemy);
        }
    }

    foreach (GameObject enemy in enemyToSlow)
    {
        enemy.GetComponent<Enemy>().moveSpeed = enemy.GetComponent<Enemy>().moveSpeed / 2;
    }

    StartCoroutine(Buff());
    }

    IEnumerator Buff()
    {
        yield return new WaitForSeconds(10);
        damageBoost = 0;
        foreach (GameObject enemy in enemyToSlow)
        {
            enemy.GetComponent<Enemy>().moveSpeed = enemy.GetComponent<Enemy>().initialSpeed;
        }
        enemyToSlow.Clear();
        yield break;
    }

    IEnumerator CryWait()
    {
        yield return new WaitForSeconds(0.5f);
        BattleCry();
        isCrying = false; 
        yield break;
    }

    private void RemainingTime() // Calculate the remaining time before next use, which will be used for the UI, Usable for all characters
    {
        if (remainingTime1 > 0)
        {
            remainingTime1 = CDTimer1 - Time.timeSinceLevelLoad;
            spell1Text.GetComponent<Text>().text = remainingTime1.ToString();
            if (remainingTime1 < 0)
            {
                remainingTime1 = 0;
                spell1Text.GetComponent<Text>().text = "";
            }
        }

        if (remainingTime2 > 0)
        {
            remainingTime2 = CDTimer2 - Time.timeSinceLevelLoad;
            spell2Text.GetComponent<Text>().text = remainingTime2.ToString();
            if (remainingTime2 < 0)
            {
                remainingTime2 = 0;
                spell2Text.GetComponent<Text>().text = "";
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDashing == true && other.tag == "Enemy" + team)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.Die(Mathf.RoundToInt(spell2Damage + (spell2Damage * damageBoost)));
            damageMult += 1;
        }
    }
}

