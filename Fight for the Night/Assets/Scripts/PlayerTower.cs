using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerTower : NetworkBehaviour {
///// CONTROLS PLAYERS TOWER SPAWNING
	[Header ("UI Variables")]
	public GameObject alienGhost;
	public GameObject alienGhostRenderer;
	public GameObject slasherGhost;
	public GameObject ghost;
    public GameObject towerSelectUI;
    public GameObject iceTowerUI;
    public GameObject baseTowerUI;
    public GameObject bombTowerUI;

    [Header ("Alien Towers")]
    public GameObject alienBase1;
	public GameObject alienBase2;
	public GameObject alienBase3;
	public GameObject alienBase4;
    public GameObject alienBomb1;
    public GameObject alienBomb2;
    public GameObject alienBomb3;
    public GameObject alienBomb4;
    public GameObject alienIce1;
    public GameObject alienIce2;
    public GameObject alienIce3;
    public GameObject alienIce4;


    [Header ("Slasher Towers")]
    public GameObject slasherBase1;
	public GameObject slasherBase2;
	public GameObject slasherBase3;
	public GameObject slasherBase4;
    public GameObject slasherBomb1;
    public GameObject slasherBomb2;
    public GameObject slasherBomb3;
    public GameObject slasherBomb4;
    public GameObject slasherIce1;
    public GameObject slasherIce2;
    public GameObject slasherIce3;
    public GameObject slasherIce4;

    [Header ("Cost and Other")]
	public GameObject currentlyTouching;

	public int baseCost = 25;
	public int level2Cost = 50;
	public int level3Cost = 75;
	public int level4Cost = 100;

	private bool isAlien;
	private PlayerNetwork PlayerNet;


	// Use this for initialization
	void Start () {
		PlayerNet = this.GetComponent<PlayerNetwork> ();
		if (isLocalPlayer == false) {
			this.enabled = false;
			return;
		}
		
		currentlyTouching = null;
		ghost = GameObject.FindWithTag("GameController");
        baseTowerUI = GameObject.FindGameObjectWithTag("TowerBase");
        iceTowerUI = GameObject.FindGameObjectWithTag("TowerIce");
        bombTowerUI = GameObject.FindGameObjectWithTag("TowerBomb");
        ghost.SetActive(false);
        
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.Find("AlienClothes").gameObject.activeSelf == true) {
			isAlien = true;
		}

		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift)) {
            ghost.SetActive(true);
            iceTowerUI.SetActive(true);
            bombTowerUI.SetActive(true);
            baseTowerUI.SetActive(true);
		} 

		if (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift)) {
			ghost.SetActive(false);
		}
	}
    // Alien Towers

	[Command]
	void CmdcreateTower1Alien(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (alienBase1, position, rotation) as GameObject;        
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower2Alien(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (alienBase2, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower3Alien(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (alienBase3, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower4Alien(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (alienBase4, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

    [Command]
    void CmdcreateBomb1Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienBomb1, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateBomb2Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienBomb2, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateBomb3Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienBomb3, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateBomb4Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienBomb4, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce1Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienIce1, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce2Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienIce2, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce3Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienIce3, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce4Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienIce4, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }


    //Slasher towers

    [Command]
	void CmdcreateTower1Slasher(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (slasherBase1, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower2Slasher(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (slasherBase1, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower3Slasher(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (slasherBase2, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower4Slasher(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (slasherBase3, position, rotation) as GameObject;
		NetworkServer.Spawn (currentTower);
	}

    [Command]
    void CmdcreateBomb1Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherBomb1, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateBomb2Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherBomb2, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateBomb3Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherBomb3, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateBomb4Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherBomb4, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce1Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherIce1, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce2Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherIce2, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce3Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherIce3, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce4Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherIce4, position, rotation) as GameObject;
        NetworkServer.Spawn(currentTower);
    }


    void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Projectile") != true) {
			currentlyTouching = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == currentlyTouching)
			currentlyTouching = null;
	}

    public void ReceiveDirections(int towerChoice)
    {
        // Create all the level 1 towers
        if (currentlyTouching == null)
        {
            switch (towerChoice)
            {
                case 1:

                    if (this.GetComponent<PlayerManagement>().currentGold >= baseCost)
                    {
                        if (isAlien == true)
                        {
                            CmdcreateTower1Alien(alienGhost.transform.position, alienGhost.transform.rotation);
                        }
                        else
                        {
                            CmdcreateTower1Slasher(slasherGhost.transform.position, slasherGhost.transform.rotation);
                        }

                        this.GetComponent<PlayerManagement>().currentGold -= baseCost;
                    }
                    break;


                case 2:

                    if (this.GetComponent<PlayerManagement>().currentGold >= baseCost)
                    {
                        if (isAlien == true)
                        {
                            CmdcreateBomb1Alien(alienGhost.transform.position, alienGhost.transform.rotation);
                        }
                        else
                        {
                            CmdcreateBomb1Slasher(slasherGhost.transform.position, slasherGhost.transform.rotation);
                        }

                        this.GetComponent<PlayerManagement>().currentGold -= baseCost;
                    }
                    break;


                case 3:

                    if (this.GetComponent<PlayerManagement>().currentGold >= baseCost)
                    {
                        if (isAlien == true)
                        {
                            CmdcreateIce1Alien(alienGhost.transform.position, alienGhost.transform.rotation);
                        }
                        else
                        {
                            CmdcreateIce1Slasher(slasherGhost.transform.position, slasherGhost.transform.rotation);
                        }

                        this.GetComponent<PlayerManagement>().currentGold -= baseCost;
                    }
                    break;
            }
        }

        //////   Rank 2    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (currentlyTouching != null && currentlyTouching.CompareTag("BaseTower1"))
        {
            if (this.GetComponent<PlayerManagement>().currentGold >= level2Cost)
            {
                Destroy(currentlyTouching);
                if (isAlien == true)
                {
                    CmdcreateTower2Alien(alienGhost.transform.position, alienGhost.transform.rotation);
                }
                else
                {
                    CmdcreateTower2Slasher(slasherGhost.transform.position, slasherGhost.transform.rotation);
                }
                this.GetComponent<PlayerManagement>().currentGold -= level2Cost;
            }
        }
        else if (currentlyTouching != null && currentlyTouching.CompareTag("IceTower1"))
        {
            if (this.GetComponent<PlayerManagement>().currentGold >= level2Cost)
            {
                Destroy(currentlyTouching);
                if (isAlien == true)
                {
                    CmdcreateIce2Alien(alienGhost.transform.position, alienGhost.transform.rotation);
                }
                else
                {
                    CmdcreateIce2Slasher(slasherGhost.transform.position, slasherGhost.transform.rotation);
                }
                this.GetComponent<PlayerManagement>().currentGold -= level2Cost;
            }
        }
        else if (currentlyTouching != null && currentlyTouching.CompareTag("BombTower1"))
        {
            if (this.GetComponent<PlayerManagement>().currentGold >= level2Cost)
            {
                Destroy(currentlyTouching);
                if (isAlien == true)
                {
                    CmdcreateBomb2Alien(alienGhost.transform.position, alienGhost.transform.rotation);
                }
                else
                {
                    CmdcreateBomb2Slasher(slasherGhost.transform.position, slasherGhost.transform.rotation);
                }
                this.GetComponent<PlayerManagement>().currentGold -= level2Cost;
            }
        }

        ///// Rank 3 //////////////////////////////////////////////////////////////////////////////////

        else if (currentlyTouching != null && currentlyTouching.CompareTag("BaseTower2"))
        {
            if (this.GetComponent<PlayerManagement>().currentGold >= level3Cost)
            {
                Destroy(currentlyTouching);
                if (isAlien == true)
                {
                    CmdcreateTower3Alien(alienGhost.transform.position, alienGhost.transform.rotation);
                }
                else
                {
                    CmdcreateTower3Slasher(slasherGhost.transform.position, slasherGhost.transform.rotation);
                }
                this.GetComponent<PlayerManagement>().currentGold -= level3Cost;
            }
        }
        else if (currentlyTouching != null && currentlyTouching.CompareTag("IceTower2"))
        {
            if (this.GetComponent<PlayerManagement>().currentGold >= level3Cost)
            {
                Destroy(currentlyTouching);
                if (isAlien == true)
                {
                    CmdcreateIce3Alien(alienGhost.transform.position, alienGhost.transform.rotation);
                }
                else
                {
                    CmdcreateIce3Slasher(slasherGhost.transform.position, slasherGhost.transform.rotation);
                }
                this.GetComponent<PlayerManagement>().currentGold -= level3Cost;
            }
        } else if (currentlyTouching != null && currentlyTouching.CompareTag("BombTower2"))
        {
            if (this.GetComponent<PlayerManagement>().currentGold >= level3Cost)
            {
                Destroy(currentlyTouching);
                if (isAlien == true)
                {
                    CmdcreateBomb3Alien(alienGhost.transform.position, alienGhost.transform.rotation);
                }
                else
                {
                    CmdcreateBomb3Slasher(slasherGhost.transform.position, slasherGhost.transform.rotation);
                }
                this.GetComponent<PlayerManagement>().currentGold -= level3Cost;
            }
        }

        ///// Rank 4 /////////////////////////////////////////////////////////////////////////////////////

        else if (currentlyTouching != null && currentlyTouching.CompareTag("BaseTower3"))
        {
            if (this.GetComponent<PlayerManagement>().currentGold >= level4Cost)
            {
                Destroy(currentlyTouching);
                if (isAlien == true)
                {
                    CmdcreateTower4Alien(alienGhost.transform.position, alienGhost.transform.rotation);
                }
                else
                {
                    CmdcreateTower4Slasher(slasherGhost.transform.position, slasherGhost.transform.rotation);
                }
                this.GetComponent<PlayerManagement>().currentGold -= level4Cost;
            }
        }

        else if (currentlyTouching != null && currentlyTouching.CompareTag("IceTower3"))
        {
            if (this.GetComponent<PlayerManagement>().currentGold >= level4Cost)
            {
                Destroy(currentlyTouching);
                if (isAlien == true)
                {
                    CmdcreateIce4Alien(alienGhost.transform.position, alienGhost.transform.rotation);
                }
                else
                {
                    CmdcreateIce4Slasher(slasherGhost.transform.position, slasherGhost.transform.rotation);
                }
                this.GetComponent<PlayerManagement>().currentGold -= level4Cost;
            }
        }

        else if (currentlyTouching != null && currentlyTouching.CompareTag("BombTower3"))
        {
            if (this.GetComponent<PlayerManagement>().currentGold >= level4Cost)
            {
                Destroy(currentlyTouching);
                if (isAlien == true)
                {
                    CmdcreateBomb4Alien(alienGhost.transform.position, alienGhost.transform.rotation);
                }
                else
                {
                    CmdcreateBomb4Slasher(slasherGhost.transform.position, slasherGhost.transform.rotation);
                }
                this.GetComponent<PlayerManagement>().currentGold -= level4Cost;
            }
        }
    }
}