using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapsScript : MonoBehaviour {
    // CONTROLS PLAYERS TRAPS

    private PlayerNetwork player;
    private int team;

    private GameObject enemyPlayer;

    public float trapHeight;
    public GameObject[] blockTrapList;
    private GameObject currentlyMoving;
    public float speed;

    public ParticleSystem slowCurseParticles;

    public float blockTrapsCD;
    public float slowCurseCD;
    public bool isBlocking = false;

    // Use this for initialization
    void Start () {
        player = this.GetComponent<PlayerNetwork>();
        team = player.team;
        if (team == 0)
        {
            blockTrapList = GameObject.FindGameObjectsWithTag("BlockTrap0");
            slowCurseParticles = GameObject.FindGameObjectWithTag("SlowCurse1").GetComponent<ParticleSystem>();
        } else if (team ==1)
        {
            blockTrapList = GameObject.FindGameObjectsWithTag("BlockTrap1");
            slowCurseParticles = GameObject.FindGameObjectWithTag("SlowCurse1").GetComponent<ParticleSystem>();
        }

        if (this.gameObject.CompareTag("Player0"))
        {
            enemyPlayer = GameObject.FindGameObjectWithTag("Player0");          
        }
        else if (this.gameObject.CompareTag("Player1"))
        {
            enemyPlayer = GameObject.FindGameObjectWithTag("Player0");
        }



    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("5") && blockTrapsCD < Time.timeSinceLevelLoad && isBlocking == false)
        {
            BlockTrap();           
        }

        if (Input.GetKeyDown("6") && slowCurseCD < Time.timeSinceLevelLoad)
        {
            SlowCurse();
        }
    }

    private void BlockTrap()
    {
      
        int trapWall = Random.Range(0, blockTrapList.Length);
        currentlyMoving = blockTrapList[trapWall];
               
        StartCoroutine(FadeUp());
        
    }

    IEnumerator FadeUp() // Coroutine used to make the wall slowly climb up
    {
        for (;;)
        {
            isBlocking = true;
            if (currentlyMoving.transform.position.y < trapHeight-0.5)
            {
                currentlyMoving.transform.position = Vector3.Lerp(currentlyMoving.transform.position, new Vector3(currentlyMoving.transform.position.x, trapHeight, currentlyMoving.transform.position.z), Time.deltaTime * speed);
            }
            else
            {
                StartCoroutine(BlockTrapActiveTimer());
                yield break;
            }
            yield return null;
        }              
    }

    IEnumerator FadeDown() // Coroutine used to make the wall slowly climb up
    {
        for (;;)
        {
            if (currentlyMoving.transform.position.y > -2)
            {
                currentlyMoving.transform.position = Vector3.Lerp(currentlyMoving.transform.position, new Vector3(currentlyMoving.transform.position.x, -3, currentlyMoving.transform.position.z), Time.deltaTime * speed);
            }
            else
            {
                blockTrapsCD = Time.timeSinceLevelLoad + 5;
                isBlocking = false;
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator BlockTrapActiveTimer()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(FadeDown());
        yield break;
    }

    private void SlowCurse()
    {
        float enemySpeed = enemyPlayer.GetComponent<playerAnimation>().speedMultiplier;
        enemyPlayer.GetComponent<playerAnimation>().speedMultiplier = 2;
        slowCurseParticles.Play();
        StartCoroutine(SlowTimer(enemySpeed));

    }

    IEnumerator SlowTimer(float initial)
    {
        yield return new WaitForSeconds(7);
        enemyPlayer.GetComponent<playerAnimation>().speedMultiplier = initial;
        slowCurseCD = Time.timeSinceLevelLoad + 15;
        slowCurseParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        yield break;
    }
}
