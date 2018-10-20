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

     private GameObject[,] refArray = new GameObject[2,12];
     private GameObject previousTower;
     private bool loop = false;
     private string loopString = "";
     private int loopcost;
     private int loopType;


	// Use this for initialization
	void Start () {
        refArray[0,0] = alienBase1;
        refArray[0,1] = alienBase2;
         refArray[0,2] = alienBase3;
         refArray[0,3] = alienBase4;
         refArray[0,4] = alienBomb1;
         refArray[0,5] = alienBomb2;
         refArray[0,6] = alienBomb3;
         refArray[0,7] = alienBomb4;
         refArray[0,8] = alienIce1;
         refArray[0,9] = alienIce2;
         refArray[0,10] = alienIce3;
         refArray[0,11] = alienIce4;

         refArray[1,0] = slasherBase1;
         refArray[1,1] = slasherBase2;
         refArray[1,2] = slasherBase3;
         refArray[1,3] = slasherBase4;
         refArray[1,4] = slasherBomb1;
        refArray[1,5] = slasherBomb2;
        refArray[1,6] = slasherBomb3;
        refArray[1,7] = slasherBomb4;
        refArray[1,8] = slasherIce1;
        refArray[1,9] = slasherIce2;
        refArray[1,10] = slasherIce3;
        refArray[1,11] = slasherIce4;



		PlayerNet = this.GetComponent<PlayerNetwork> ();
		if (PlayerNet.local == false) {
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
        if(loop == true) {
            loop = false;
            RecalculateCurrentlyTouching(loopString, loopcost, loopType);
        }

		if (transform.Find("AlienClothes").gameObject.activeSelf == true) {
			isAlien = true;
		}

		if ((Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift)) && currentlyTouching != null) {
            if (currentlyTouching != null && currentlyTouching.CompareTag("TowerLocation"))
            {
                if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Base4 || currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Bomb4 || currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Ice4)
                {
                    
                } else
                {
                    Cursor.visible = true;
                    ghost.SetActive(true);
                    iceTowerUI.SetActive(true);
                    bombTowerUI.SetActive(true);
                    baseTowerUI.SetActive(true);
                }
            }
            
		} 

		if (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift)) {
			ghost.SetActive(false);
            Cursor.visible = false;
		}
	}
    
    // Unified spawn command
    [Command]
    void CmdCreateTower(Vector3 position, Quaternion rotation, int team, int towerRef, NetworkInstanceId id, bool level1) {
        GameObject currentTower = Instantiate (refArray[team,towerRef], position, rotation);
        NetworkServer.Spawn (currentTower);
        if (!level1) {
            GameObject oldTower = NetworkServer.FindLocalObject(id);
            NetworkServer.Destroy(oldTower);
        }
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
        if (loop == false) {
        //////   Rank 2    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (currentlyTouching != null && currentlyTouching.CompareTag("TowerLocation") && currentlyTouching.GetComponent<TowerSpot>().isOccupied == true )
        {
            
            if(currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Base1)
            {                
                if (this.GetComponent<PlayerManagement>().currentGold >= baselevel2Cost)
                {
                    previousTower = currentlyTouching.GetComponent<TowerSpot>().towerOn;
                    NetworkInstanceId previousTowerId = previousTower.GetComponent<NetworkIdentity>().netId;
                    if (isAlien == true)
                    {
                        CmdCreateTower(currentlyTouching.transform.position, alienGhost.transform.rotation, 0, 1, previousTowerId, false);
                        Debug.Log("AlienBase2");
                    }
                    else
                    {
                        CmdCreateTower(currentlyTouching.transform.position, slasherGhost.transform.rotation, 1,2, previousTowerId, false);
                    }
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.loading;
                    RecalculateCurrentlyTouching("BaseTower2", baselevel2Cost, 2);
                    
                }
            }           

            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Ice1)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= icelevel2Cost)
                {
                    previousTower = currentlyTouching.GetComponent<TowerSpot>().towerOn;
                    NetworkInstanceId previousTowerId = previousTower.GetComponent<NetworkIdentity>().netId;
                    if (isAlien == true)
                    {
                        CmdCreateTower(currentlyTouching.transform.position, alienGhost.transform.rotation, 0,9, previousTowerId, false);
                    }
                    else
                    {
                        CmdCreateTower(currentlyTouching.transform.position, slasherGhost.transform.rotation, 1,9, previousTowerId, false);
                    }
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.loading;
                    RecalculateCurrentlyTouching("IceTower2", icelevel2Cost, 6);
                    
                }
            }              
       
            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Bomb1)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= bomblevel2Cost)
                {
                    previousTower = currentlyTouching.GetComponent<TowerSpot>().towerOn;
                    NetworkInstanceId previousTowerId = previousTower.GetComponent<NetworkIdentity>().netId;
                    if (isAlien == true)
                    {
                        CmdCreateTower(currentlyTouching.transform.position, alienGhost.transform.rotation, 0,5, previousTowerId, false);
                    }
                    else
                    {
                        CmdCreateTower(currentlyTouching.transform.position, slasherGhost.transform.rotation, 1,5, previousTowerId, false);
                    }
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.loading;
                    RecalculateCurrentlyTouching("BombTower2", bomblevel2Cost, 10);
                    
                }
            }              
        

        ///// Rank 3 //////////////////////////////////////////////////////////////////////////////////

      
            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Base2)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= baselevel3Cost)
                {
                    previousTower = currentlyTouching.GetComponent<TowerSpot>().towerOn;
                    NetworkInstanceId previousTowerId = previousTower.GetComponent<NetworkIdentity>().netId;
                    if (isAlien == true)
                    {
                        CmdCreateTower(currentlyTouching.transform.position, alienGhost.transform.rotation, 0,2, previousTowerId, false);
                    }
                    else
                    {
                        CmdCreateTower(currentlyTouching.transform.position, slasherGhost.transform.rotation, 1,2, previousTowerId, false);
                    }
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.loading;
                    RecalculateCurrentlyTouching("BaseTower3", baselevel3Cost, 3);
                    
                }
            }               
       
            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Ice2)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= icelevel3Cost)
                {
                    previousTower = currentlyTouching.GetComponent<TowerSpot>().towerOn;
                    NetworkInstanceId previousTowerId = previousTower.GetComponent<NetworkIdentity>().netId;
                    if (isAlien == true)
                    {
                        CmdCreateTower(currentlyTouching.transform.position, alienGhost.transform.rotation, 0,10, previousTowerId, false);
                    }
                    else
                    {
                        CmdCreateTower(currentlyTouching.transform.position, slasherGhost.transform.rotation, 1, 10, previousTowerId, false);
                    }
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.loading;
                    RecalculateCurrentlyTouching("IceTower3", icelevel3Cost, 7);
                    
                }
            }            
        
            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Bomb2)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= bomblevel3Cost)
                {
                    previousTower = currentlyTouching.GetComponent<TowerSpot>().towerOn;
                    NetworkInstanceId previousTowerId = previousTower.GetComponent<NetworkIdentity>().netId;
                    if (isAlien == true)
                    {
                        CmdCreateTower(currentlyTouching.transform.position, alienGhost.transform.rotation, 0,6, previousTowerId, false);
                    }
                    else
                    {
                        CmdCreateTower(currentlyTouching.transform.position, slasherGhost.transform.rotation, 1,6, previousTowerId, false);
                    }
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.loading;
                    RecalculateCurrentlyTouching("BombTower3", bomblevel3Cost, 11);
                    
                }
            }              
       
        ///// Rank 4 /////////////////////////////////////////////////////////////////////////////////////

      
            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Base3)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= baselevel4Cost)
                {
                    previousTower = currentlyTouching.GetComponent<TowerSpot>().towerOn;
                    NetworkInstanceId previousTowerId = previousTower.GetComponent<NetworkIdentity>().netId;
                    if (isAlien == true)
                    {

                        CmdCreateTower(currentlyTouching.transform.position, alienGhost.transform.rotation, 0,3, previousTowerId, false);
                    }
                    else
                    {
                        CmdCreateTower(currentlyTouching.transform.position, slasherGhost.transform.rotation, 1,3, previousTowerId, false);
                    }
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.loading;
                    RecalculateCurrentlyTouching("BaseTower4", baselevel4Cost, 4);
                    
                }
            }              
       
            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Ice3)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= icelevel4Cost)
                {
                    previousTower = currentlyTouching.GetComponent<TowerSpot>().towerOn;
                    NetworkInstanceId previousTowerId = previousTower.GetComponent<NetworkIdentity>().netId;
                    if (isAlien == true)
                    {
                        CmdCreateTower(currentlyTouching.transform.position, alienGhost.transform.rotation, 0,11, previousTowerId, false);
                    }
                    else
                    {
                        CmdCreateTower(currentlyTouching.transform.position, slasherGhost.transform.rotation, 1,11, previousTowerId, false);
                    }
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.loading;
                    RecalculateCurrentlyTouching("IceTower4", icelevel4Cost, 8);
                    
                }
            }              

            else if (currentlyTouching.GetComponent<TowerSpot>().currentType == TowerSpot.type.Bomb3)
            {
                if (this.GetComponent<PlayerManagement>().currentGold >= bomblevel4Cost)
                {
                    previousTower = currentlyTouching.GetComponent<TowerSpot>().towerOn;
                    NetworkInstanceId previousTowerId = previousTower.GetComponent<NetworkIdentity>().netId;
                    if (isAlien == true)
                    {
                        CmdCreateTower(currentlyTouching.transform.position, alienGhost.transform.rotation, 0,7, previousTowerId, false);
                    }
                    else
                    {
                        CmdCreateTower(currentlyTouching.transform.position, slasherGhost.transform.rotation, 1,7, previousTowerId, false);
                    }
                    currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.loading;
                    RecalculateCurrentlyTouching("BombTower4", bomblevel4Cost, 12);
                    
                }             
            }
        }

        //////////////////////////////////////// Initial ////////////////////////////////////////////////////////////////

        else if (currentlyTouching != null && currentlyTouching.CompareTag("TowerLocation") && currentlyTouching.GetComponent<TowerSpot>().isOccupied == false)
        {
            NetworkInstanceId placeholder = this.gameObject.GetComponent<NetworkIdentity>().netId;
            switch (towerChoice)
            {
                case 1:

                    if (this.GetComponent<PlayerManagement>().currentGold >= baseCost)
                    {
                        if (isAlien == true)
                        {
                            CmdCreateTower(currentlyTouching.transform.position, alienGhost.transform.rotation, 0,0, placeholder, true);
                        }
                        else
                        {
                            CmdCreateTower(currentlyTouching.transform.position, slasherGhost.transform.rotation, 1,0, placeholder, true);
                        }
                        currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.loading;
                        currentlyTouching.GetComponent<TowerSpot>().isOccupied = true;
                        RecalculateCurrentlyTouching("BaseTower1", baseCost, 1);
                        
                    }
                    break;


                case 2:

                    if (this.GetComponent<PlayerManagement>().currentGold >= bombCost)
                    {
                        if (isAlien == true)
                        {
                            CmdCreateTower(currentlyTouching.transform.position, alienGhost.transform.rotation, 0,4, placeholder, true);
                        }
                        else
                        {
                            CmdCreateTower(currentlyTouching.transform.position, slasherGhost.transform.rotation, 1,4, placeholder, true);
                        }
                        currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.loading;
                        currentlyTouching.GetComponent<TowerSpot>().isOccupied = true;
                        RecalculateCurrentlyTouching("BombTower1", bombCost, 9);
                        
                    }
                    break;


                case 3:

                    if (this.GetComponent<PlayerManagement>().currentGold >= iceCost)
                    {
                        if (isAlien == true)
                        {
                            CmdCreateTower(currentlyTouching.transform.position, alienGhost.transform.rotation, 0,8, placeholder, true);
                        }
                        else
                        {
                            CmdCreateTower(currentlyTouching.transform.position, slasherGhost.transform.rotation, 1,8, placeholder, true);
                        }
                        currentlyTouching.GetComponent<TowerSpot>().currentType = TowerSpot.type.loading;
                        currentlyTouching.GetComponent<TowerSpot>().isOccupied = true;
                        RecalculateCurrentlyTouching("IceTower1", iceCost, 5);
                    }
                break;
            }
        }
    }
    }


    public void RecalculateCurrentlyTouching (string type, int cost, int selectType) {
            GameObject[] towers = GameObject.FindGameObjectsWithTag (type);
            float shortestDistance = Mathf.Infinity;
            GameObject nearestTower = null;
            foreach (GameObject i in towers) {
                float distanceToTower = Vector3.Distance (currentlyTouching.transform.position, i.transform.position);
                if (distanceToTower < shortestDistance) {
                    if ((PlayerNet.team == 0 && i.transform.position.x < PlayerNet.centreLineX) || (PlayerNet.team == 1 && i.transform.position.x > PlayerNet.centreLineX)) {
                        shortestDistance = distanceToTower;
                        nearestTower = i;
                    }
                }
            }
            if (shortestDistance < 3.0f) {
                currentlyTouching.GetComponent<TowerSpot>().towerOn = nearestTower;
                nearestTower.transform.Find("Rotate").GetComponent<TowerAttacking>().enabled = true;
                this.GetComponent<PlayerManagement>().Spend(cost);
                currentlyTouching.GetComponent<TowerSpot>().currentType = (TowerSpot.type)selectType;
            } else {
                loop = true;
                loopString = type;
                loopcost = cost;
                loopType = selectType;

            }
    }
}
