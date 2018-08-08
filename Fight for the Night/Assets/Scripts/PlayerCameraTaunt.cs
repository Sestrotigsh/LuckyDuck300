using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraTaunt : MonoBehaviour {


    private bool lookingAtScreen = false;   

    public Camera initialCamera;
    public GameObject enemyCamera;

    private Vector3 futureCameraPos;
    private Vector3 futureCameraRot;

    private GameObject enemyPlayer;

    public float orbitDistance = 3f;
    public float orbitDegreesPerSec = 180.0f;
    public Vector3 relativeDistance = Vector3.zero;

    public GameObject canvasTaunt;
    public GameObject canvasInitial;
    public GameObject taunt1;
    public GameObject taunt2;

    public AudioSource audioS;

    public AudioClip clip1;
    public AudioClip clip2;


    private float tauntCD = 5;
    private float tauntRemainingTime;

    // Use this for initialization
    void Start () {

        initialCamera = Camera.main;
        if (this.gameObject.CompareTag("Player0"))
        {
            enemyPlayer = GameObject.FindGameObjectWithTag("Player1");
            enemyCamera = enemyPlayer.transform.Find("CameraStalk").gameObject;
            
        }
        else if (this.gameObject.CompareTag("Player1"))
        {
            enemyPlayer = GameObject.FindGameObjectWithTag("Player0");
            enemyCamera = enemyPlayer.transform.Find("CameraStalk").gameObject;
        }

        if (enemyCamera != null)
        {
            relativeDistance = enemyCamera.transform.position - enemyPlayer.transform.position;
        }

        audioS = enemyCamera.GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("z"))
        {
            CheckScreen();
        }

        if (lookingAtScreen == true)
        {
            Orbit();
        }

        if (Input.GetKeyDown("3") && tauntRemainingTime < Time.timeSinceLevelLoad)
        {
            audioS.clip = clip1;
            audioS.Play();
            tauntRemainingTime = Time.timeSinceLevelLoad + tauntCD;
        }
        if (Input.GetKeyDown("4") && tauntRemainingTime < Time.timeSinceLevelLoad)
        {
            audioS.clip = clip2;
            audioS.Play();
            tauntRemainingTime = Time.timeSinceLevelLoad + tauntCD;
        }
    }

    public void CheckScreen()
    {
        if (lookingAtScreen == false)
        {
            initialCamera.enabled = false;
            enemyCamera.SetActive(true);
            lookingAtScreen = true;
            canvasInitial.SetActive(false);
            canvasTaunt.SetActive(true);

        } else if (lookingAtScreen == true)
        {
            enemyCamera.SetActive(false);
            initialCamera.enabled = true;            
            lookingAtScreen = false;
            canvasTaunt.SetActive(false);
            canvasInitial.SetActive(true);
        }             
    }

    public void Orbit()
    {
        // Keep us at the last known relative position
        enemyCamera.transform.position = enemyPlayer.transform.position + relativeDistance;
        enemyCamera.transform.RotateAround(enemyPlayer.transform.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
        // Reset relative position after rotate
        relativeDistance = enemyCamera.transform.position - enemyPlayer.transform.position;
    }
}


