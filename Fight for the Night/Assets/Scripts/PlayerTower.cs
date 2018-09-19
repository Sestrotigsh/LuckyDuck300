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

	public int baseCost;
	public int baselevel2Cost;
	public int baselevel3Cost;
	public int baselevel4Cost;

    public int bombCost;
    public int bomblevel2Cost;
    public int bomblevel3Cost;
    public int bomblevel4Cost;

    public int iceCost;
    public int icelevel2Cost;
    public int icelevel3Cost;
    public int icelevel4Cost;

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
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower2Alien(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (alienBase2, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower3Alien(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (alienBase3, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower4Alien(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (alienBase4, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn (currentTower);
	}

    [Command]
    void CmdcreateBomb1Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienBomb1, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateBomb2Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienBomb2, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateBomb3Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienBomb3, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateBomb4Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienBomb4, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce1Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienIce1, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce2Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienIce2, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce3Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienIce3, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce4Alien(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(alienIce4, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }


    //Slasher towers

    [Command]
	void CmdcreateTower1Slasher(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (slasherBase1, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower2Slasher(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (slasherBase1, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower3Slasher(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (slasherBase2, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn (currentTower);
	}

	[Command]
	void CmdcreateTower4Slasher(Vector3 position, Quaternion rotation) {
		var currentTower = Instantiate (slasherBase3, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn (currentTower);
	}

    [Command]
    void CmdcreateBomb1Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherBomb1, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateBomb2Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherBomb2, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateBomb3Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherBomb3, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateBomb4Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherBomb4, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce1Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherIce1, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce2Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherIce2, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce3Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherIce3, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
        NetworkServer.Spawn(currentTower);
    }

    [Command]
    void CmdcreateIce4Slasher(Vector3 position, Quaternion rotation)
    {
        var currentTower = Instantiate(slasherIce4, position, rotation) as GameObject;
        currentlyTouching.GetComponent<TowerSpot>().towerOn = currentTower;
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
        //////   Rank 2    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (currentlyTouching != null && currentlyTouching.CompareTag("TowerLocation") && currentlyTouching.GetComponent<TowerSpot>().isOccupied == true)
        {
            if(currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Base1)
            {                
                if (this.GetComponent<PlayerManagement>().currentGold >= baselevel2Cost)
                {
                    Destroy(currentlyTouching.GetComponent<TowerSpot>().towerOn);
                    if (isAlien == true)
                    {
                        CmdcreateTower2Alien(currentlyTouching.transform.position, alienGhost.transform.rotation);
                    }
                    else
                    {
                        CmdcreateTower2Slasher(currentlyTouching.transform.position, slasherGhost.transform.rotation);
                    }
                    this.GetComponent<PlayerManagement>().Spend(baselevel2Cost);
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.Base2;
                }
            }           

            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Ice1)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= icelevel2Cost)
                {
                    Destroy(currentlyTouching.GetComponent<TowerSpot>().towerOn);
                    if (isAlien == true)
                    {
                        CmdcreateIce2Alien(currentlyTouching.transform.position, alienGhost.transform.rotation);
                    }
                    else
                    {
                        CmdcreateIce2Slasher(currentlyTouching.transform.position, slasherGhost.transform.rotation);
                    }
                    this.GetComponent<PlayerManagement>().Spend(icelevel2Cost);
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.Ice2;
                }
            }              
       
            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Bomb1)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= bomblevel2Cost)
                {
                    Destroy(currentlyTouching.GetComponent<TowerSpot>().towerOn);
                    if (isAlien == true)
                    {
                        CmdcreateBomb2Alien(currentlyTouching.transform.position, alienGhost.transform.rotation);
                    }
                    else
                    {
                        CmdcreateBomb2Slasher(currentlyTouching.transform.position, slasherGhost.transform.rotation);
                    }
                    this.GetComponent<PlayerManagement>().Spend(bomblevel2Cost);
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.Bomb2;
                }
            }              
        

        ///// Rank 3 //////////////////////////////////////////////////////////////////////////////////

      
            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Base2)
            {
                Debug.Log("Test1");
                if (this.GetComponent<PlayerManagement>().currentGold >= baselevel3Cost)
                {
                    Destroy(currentlyTouching.GetComponent<TowerSpot>().towerOn);
                    if (isAlien == true)
                    {
                        CmdcreateTower3Alien(currentlyTouching.transform.position, alienGhost.transform.rotation);
                    }
                    else
                    {
                        CmdcreateTower3Slasher(currentlyTouching.transform.position, slasherGhost.transform.rotation);
                    }
                    this.GetComponent<PlayerManagement>().Spend(baselevel3Cost);
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.Base3;
                }
            }               
       
            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Ice2)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= icelevel3Cost)
                {
                    Destroy(currentlyTouching.GetComponent<TowerSpot>().towerOn);
                    if (isAlien == true)
                    {
                        CmdcreateIce3Alien(currentlyTouching.transform.position, alienGhost.transform.rotation);
                    }
                    else
                    {
                        CmdcreateIce3Slasher(currentlyTouching.transform.position, slasherGhost.transform.rotation);
                    }
                    this.GetComponent<PlayerManagement>().Spend(icelevel3Cost);
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.Ice3;
                }
            }            
        
            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Bomb2)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= bomblevel3Cost)
                {
                    Destroy(currentlyTouching.GetComponent<TowerSpot>().towerOn);
                    if (isAlien == true)
                    {
                        CmdcreateBomb3Alien(currentlyTouching.transform.position, alienGhost.transform.rotation);
                    }
                    else
                    {
                        CmdcreateBomb3Slasher(currentlyTouching.transform.position, slasherGhost.transform.rotation);
                    }
                    this.GetComponent<PlayerManagement>().Spend(bomblevel3Cost);
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.Bomb3;
                }
            }              
       
        ///// Rank 4 /////////////////////////////////////////////////////////////////////////////////////

      
            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Base3)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= baselevel4Cost)
                {
                    Destroy(currentlyTouching.GetComponent<TowerSpot>().towerOn);
                    if (isAlien == true)
                    {
                        CmdcreateTower4Alien(currentlyTouching.transform.position, alienGhost.transform.rotation);
                    }
                    else
                    {
                        CmdcreateTower4Slasher(currentlyTouching.transform.position, slasherGhost.transform.rotation);
                    }
                    this.GetComponent<PlayerManagement>().Spend(baselevel4Cost);
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.Base4;
                }
            }              
       
            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Ice3)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= icelevel4Cost)
                {
                    Destroy(currentlyTouching.GetComponent<TowerSpot>().towerOn); ;
                    if (isAlien == true)
                    {
                        CmdcreateIce4Alien(currentlyTouching.transform.position, alienGhost.transform.rotation);
                    }
                    else
                    {
                        CmdcreateIce4Slasher(currentlyTouching.transform.position, slasherGhost.transform.rotation);
                    }
                    this.GetComponent<PlayerManagement>().Spend(icelevel4Cost);
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.Ice4;
                }
            }              

            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Bomb3)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= bomblevel4Cost)
                {
                    Destroy(currentlyTouching.GetComponent<TowerSpot>().towerOn);
                    if (isAlien == true)
                    {
                        CmdcreateBomb4Alien(currentlyTouching.transform.position, alienGhost.transform.rotation);
                    }
                    else
                    {
                        CmdcreateBomb4Slasher(currentlyTouching.transform.position, slasherGhost.transform.rotation);
                    }
                    this.GetComponent<PlayerManagement>().Spend(bomblevel4Cost);
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.Bomb4;
                }
            }             
        }

        //////////////////////////////////////// Initial ////////////////////////////////////////////////////////////////

        else if (currentlyTouching != null && currentlyTouching.CompareTag("TowerLocation") && currentlyTouching.GetComponent<TowerSpot>().isOccupied == false)
        {
            switch (towerChoice)
            {
                case 1:

                    if (this.GetComponent<PlayerManagement>().currentGold >= baseCost)
                    {
                        if (isAlien == true)
                        {
                            CmdcreateTower1Alien(currentlyTouching.transform.position, alienGhost.transform.rotation);
                        }
                        else
                        {
                            CmdcreateTower1Slasher(currentlyTouching.transform.position, slasherGhost.transform.rotation);
                        }

                        this.GetComponent<PlayerManagement>().Spend(baseCost);
                        currentlyTouching.GetComponent<TowerSpot>().isOccupied = true;
                        currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.Base1;

                    }
                    break;


                case 2:

                    if (this.GetComponent<PlayerManagement>().currentGold >= bombCost)
                    {
                        if (isAlien == true)
                        {
                            CmdcreateBomb1Alien(currentlyTouching.transform.position, alienGhost.transform.rotation);
                        }
                        else
                        {
                            CmdcreateBomb1Slasher(currentlyTouching.transform.position, slasherGhost.transform.rotation);
                        }

                        this.GetComponent<PlayerManagement>().Spend(bombCost);
                        currentlyTouching.GetComponent<TowerSpot>().isOccupied = true;
                        currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.Bomb1;
                    }
                    break;


                case 3:

                    if (this.GetComponent<PlayerManagement>().currentGold >= iceCost)
                    {
                        if (isAlien == true)
                        {
                            CmdcreateIce1Alien(currentlyTouching.transform.position, alienGhost.transform.rotation);
                        }
                        else
                        {
                            CmdcreateIce1Slasher(currentlyTouching.transform.position, slasherGhost.transform.rotation);
                        }

                        this.GetComponent<PlayerManagement>().Spend(iceCost);
                        currentlyTouching.GetComponent<TowerSpot>().isOccupied = true;
                        currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.Ice1;
                    }
                    break;
            }
        }
    }
}