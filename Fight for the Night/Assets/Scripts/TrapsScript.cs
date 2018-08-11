using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapsScript : MonoBehaviour {
///// CONTROLS PLAYERS TRAPS

    private PlayerNetwork player;
    private int team;

    public float trapHeight;
    public GameObject[] blockTrapList;
    private GameObject currentlyMoving;
    public float speed;

    public float blockTrapsCD;
    public bool isBlocking = false;

    // Use this for initialization
    void Start () {
        player = this.GetComponent<PlayerNetwork>();
        team = player.team;
        if (team == 0)
        {
            blockTrapList = GameObject.FindGameObjectsWithTag("BlockTrap0");
        } else if (team ==1)
        {
            blockTrapList = GameObject.FindGameObjectsWithTag("BlockTrap1");
        }
       
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("5") && blockTrapsCD < Time.timeSinceLevelLoad && isBlocking == false)
        {
            BlockTrap();           
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

    private void StartTimer()
    {
        StartCoroutine(BlockTrapActiveTimer());
    }

    private void StartFadeDown()
    {
        StartCoroutine(FadeDown());
    }

}
