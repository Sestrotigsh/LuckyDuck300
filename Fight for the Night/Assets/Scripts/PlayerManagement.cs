using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManagement : MonoBehaviour {
///// MANAGES THE PLAYERS CANVAS VARIABLES

	public GameObject canv; // The canvas and Text attached with this player
	public GameObject canvHPT;
	public GameObject goldT;
	public GameObject incomeT;
	public GameObject bankBalanceT;
    public GameObject endGame;

    public Slider healthSlider;
    public Slider GoldSlider;

    private EventTrigger baseHP;
	private PlayerNetwork player;
    private PlayerNetwork player2;

    public int startingGold = 50;
    public int currentIncome;
    public float currentGold;
    public int incomeInterval = 5;
    private int timeCount;

	// Controls for player reset when touching minion
	private GameObject spawnpoint;

	private int maxBank = 100;
	private int currentBank = 0;

    //private Transform mainCamera;

	// Use this for initialization
	void Start () {
        // Manage Gold
        currentGold = currentGold + startingGold;
		timeCount = incomeInterval;
		player = this.GetComponent<PlayerNetwork> ();
        if (player.team == 0)
        {
            //mainCamera = Camera.main.transform;
            //mainCamera.Rotate(0,180,0);

            if (GameObject.FindGameObjectWithTag("Player1"))
            {
                player2 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerNetwork>();
            }           
        } else if (player.team == 1)
        {
            if (GameObject.FindGameObjectWithTag("Player0"))
            {
                player2 = GameObject.FindGameObjectWithTag("Player0").GetComponent<PlayerNetwork>();
            }
        }
        
		spawnpoint = GameObject.Find ("SpawnPoint" + (player.team + 1));
    }
	
	// Update is called once per frame
	void Update () {
		if (!player.local) {
			canv.SetActive(false);
            return;
		}
        UpdateCanvas();
        Income();
	}

    private void UpdateCanvas() // Update the Base HP on the canvas
    {
        healthSlider.value = player.health;
        GoldSlider.value = currentBank + currentIncome;
		canvHPT.GetComponent<Text>().text = player.health.ToString();
        goldT.GetComponent<Text>().text =  Mathf.Ceil(currentGold).ToString();   
        incomeT.GetComponent<Text>().text = currentIncome + "/"+incomeInterval + "s";
        if (player.health <= 0)
        {
            endGame.GetComponent<Text>().text = "Defeat";
        } else if (player2)
        {
            if (player2.health <= 0)
            {
                endGame.GetComponent<Text>().text = "Victory!";
            }          
        }       
    }

    private void Income()
    {
		if (timeCount <= Time.timeSinceLevelLoad)
        {
			if (currentBank < maxBank) {
				currentBank = currentBank + currentIncome;
			}
			//currentGold = currentGold + currentIncome;
            timeCount = timeCount + incomeInterval;
        }
    }
    public void Earn(float money)
    {
        float UITime = Time.time + 2;
        currentGold = currentGold + money;
        GameObject sign = new GameObject("player_Spend");
        sign.transform.parent = this.gameObject.transform;
        sign.transform.rotation = Camera.main.transform.rotation; // Causes the text faces camera.
        TextMesh tm = sign.AddComponent<TextMesh>();
        tm.text = "+" + Mathf.Ceil(money) + " Gold";
        tm.color = new Color(0.8f, 0.8f, 0.8f);
        tm.fontStyle = FontStyle.Bold;
        tm.alignment = TextAlignment.Center;
        tm.anchor = TextAnchor.MiddleCenter;
        tm.characterSize = 0.065f;
        tm.fontSize = 40;
        sign.transform.position = this.transform.position + Vector3.up * 2.5f;
        Destroy(sign, 1.5f);
    }

    public void Spend(float money)
    {
        float UITime = Time.time + 2;
        currentGold = currentGold - money;
        GameObject sign = new GameObject("player_Spend");
        sign.transform.parent = this.gameObject.transform;
        sign.transform.rotation = Camera.main.transform.rotation; // Causes the text faces camera.
        TextMesh tm = sign.AddComponent<TextMesh>();
        tm.text = "-" + Mathf.Ceil(money) +" Gold";
        tm.color = new Color(0.8f, 0.8f, 0.8f);
        tm.fontStyle = FontStyle.Bold;
        tm.alignment = TextAlignment.Center;
        tm.anchor = TextAnchor.MiddleCenter;
        tm.characterSize = 0.065f;
        tm.fontSize = 40;
        sign.transform.position = this.transform.position + Vector3.up * 2.5f;
        Destroy(sign, 1.5f);
        
    }
    IEnumerator MoveUp(float timing, GameObject panel)
    {
        
        if (Time.time < timing)
        {
            Debug.Log("rroke");
            panel.transform.Translate(Vector3.up * Time.deltaTime);
            yield return null;
        }
        else
        {
            Destroy(panel);
            yield break;
        }
    }

	void OnTriggerEnter(Collider other) {
		if (!player.local) {
			return;
		}
        // DUMMY CODE FOR PLAYERS PHYSICAL INTERACTIONS WITH THE ENEMIES - REPLACE SOON
		//if (other.CompareTag ("Enemy")) {
			//transform.position = spawnpoint.transform.position;
			//this.GetComponent<playerAnimation> ().setupCamera ();
		//}

		if (other.CompareTag ("Bank")) {
			currentGold = currentGold + currentBank;
            if (currentBank != 0.0f) {
               other.GetComponent<AudioSource>().Play(); 
               currentBank = 0;
            }
		}
	}
}
