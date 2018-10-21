using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class playerAnimation : NetworkBehaviour {
///// CONTROLS THE PLAYERS ANIMATION AND CAMERA MOVEMENT

	Transform cameraLookTarget;
	[SerializeField] Vector3 cameraOffset;
	[SerializeField] float damping;
	[SerializeField] Vector2 mouseDamping;
	[SerializeField] Vector2 mouseSensitivity;
	Transform mainCamera;
	Vector3 targetPosition;
	Quaternion targetRotation;
	[SerializeField] Vector2 mouseInput;
	private float shootTimer;
	private bool shooting;
	public float rotateAmount;
	public float speedMultiplier;
	private Transform shootingPoint;
	public Transform accuracyTarget;
	Animator anim;
	//NetworkAnimator netAnim;
	private bool lookingAtScreen;
	[SerializeField] Vector3 checkPositionP1;
	[SerializeField] Vector3 checkPositionP2;
	[SerializeField] Vector3 checkRotationP1;
	[SerializeField] Vector3 checkRotationP2;
	[SerializeField] float verticalLimit;
	[SerializeField] float horizontalLimit;
	//[SerializeField] float Midpoint;
	//private GameObject ScreenKeyHint;
	private float initialDistance;
	private float currentDistance;
	private PlayerNetwork playerNet;


	float h;
	float v;

	float updateTime = 0.0f;

	[SyncVar (hook = "StateChangedNet")]
	public string changedState;

	private int isSlasher = 0;

	private Vector3 oldPos;
	private Vector3 newPos;

	private bool usingTowers = false;



	// Use this for initialization
	void Start () { 
		if (transform.Find("SlasherClothes").gameObject.activeSelf == true) {
			isSlasher = 2;
		} else if (transform.Find("AlienClothes").gameObject.activeSelf == true){
			isSlasher = 1;
		}
		shootTimer = 0;
		shooting = false;
		playerNet = this.GetComponent<PlayerNetwork>();
		anim = GetComponent<Animator>();
		if (!playerNet.local) {
			oldPos = transform.localPosition;
			this.GetComponent<AudioListener>().enabled = false;
			return;
		}


		GameObject[] shootingPointsList = GameObject.FindGameObjectsWithTag("ShootingPoint");



		shootingPoint = shootingPointsList[playerNet.team].transform;




		//shootingPoint = GameObject.FindWithTag("ShootingPoint").transform;
		// set up the third person camera
		//netAnim = GetComponent<NetworkAnimator>();
		setupCamera();
		
		initialDistance = Vector3.Distance(mainCamera.position, this.transform.position);
		
		//ScreenKeyHint = GameObject.Find ("Screen Key Prompt");
		//if (this.gameObject.CompareTag ("Player0")) {
			//ScreenKeyHint.transform.position = new Vector3 (ScreenKeyHint.transform.position.x, ScreenKeyHint.transform.position.y, 260f);
		//} else if (this.gameObject.CompareTag ("Player1")) {
			//ScreenKeyHint.transform.position = new Vector3 (ScreenKeyHint.transform.position.x, ScreenKeyHint.transform.position.y, 270f);
		//}

		//if (this.transform.position.z > Midpoint) {
			//mainCamera.Rotate(0,180,0);
			//shootingPoint.Rotate(0,180,0);
		//}
	}

	
	// Update is called once per frame
	void Update () {
		if (isSlasher == 0) {
			if (transform.Find("SlasherClothes").gameObject.activeSelf == true) {
				isSlasher = 2;
			} else if (transform.Find("AlienClothes").gameObject.activeSelf == true){
				isSlasher = 1;
			}
		}


		if (!playerNet.local) {
			if (Time.timeSinceLevelLoad > updateTime) {
				newPos = transform.localPosition;
				h = 2.0f*(newPos.x-oldPos.x) / (Time.deltaTime * speedMultiplier);
				v = (newPos.z - oldPos.z) / (Time.deltaTime * speedMultiplier);
				float hOld = h;
				float vOld = v;
				if (Mathf.Abs(h) > 0.3f) {
					h = 1.0f;
				} else {
					h = 0.0f;
				}
				if (Mathf.Abs(v) > 0.3f) {
					v = 1.0f;
				} else {
					v = 0.0f;
				}
				if (v == 1.0f && h == 1.0f) {
					if (Mathf.Abs(vOld) - Mathf.Abs(hOld) > 3.0f) {
						h = 0.0f;
					} else if (Mathf.Abs(hOld) - Mathf.Abs(vOld) > 3.0f) {
						v = 0.0f;
					}
				}
				anim.SetFloat("hSpeed", h);
				anim.SetFloat ("vSpeed", v);
				oldPos = newPos;
				updateTime = Time.timeSinceLevelLoad + 0.2f;
			}
			return;
		}

		//if (Input.GetKeyUp ("z")) {
			//lookingAtScreen = false;
			//ScreenKeyHint.gameObject.SetActive (true);
		//}

		//if (Input.GetKey ("z")) {
			//CheckScreen ();
		//}
		//if (lookingAtScreen == true) {
			//ScreenKeyHint.gameObject.SetActive (false);
			//return;
		//}

		if (mainCamera.transform.position.z < 206.0f) {
			  mainCamera.transform.position += Vector3.forward * Time.deltaTime;
		} else if (mainCamera.transform.position.z > 279.0f) {
				mainCamera.transform.position -= Vector3.forward * Time.deltaTime;
		} 
		if (mainCamera.transform.position.x < 181.0f) {
			mainCamera.transform.position += Vector3.right * Time.deltaTime;
		} else if (mainCamera.transform.position.x > 197.0f) {
			mainCamera.transform.position -= Vector3.right * Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            usingTowers = true;

        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            usingTowers = false;
        }

        if (usingTowers == false)
        {
            MoveCamera();
        }


		//shootingPoint.LookAt(accuracyTarget);
		h = CrossPlatformInputManager.GetAxis ("Horizontal");
		v = CrossPlatformInputManager.GetAxis ("Vertical");
		anim.SetFloat("hSpeed", h);
		anim.SetFloat ("vSpeed", v);
		//transform.Rotate (0, h * rotateAmount, 0);
		transform.Rotate (0, h/1.5f, 0);
		mainCamera.transform.RotateAround(this.transform.position,Vector3.up, h/1.5f);

		//mainCamera.transform.Rotate (0, h / 2.0f, 0);
		/*
		if (Input.GetKeyDown (KeyCode.Space)) {
            if (shooting == false) {
                anim.SetTrigger ("Jump");
                //netAnim.SetTrigger("Jump");
            }
			
		}
		*/

		if (Input.GetMouseButtonDown(0) && usingTowers == false) {
			if (isSlasher == 2) {
				anim.SetTrigger("Basic Trigger");
				if (isServer) {
					changedState = "Basic Trigger";
				} else {
					CmdSecondPlayerAnim("Basic Trigger");
				}
			} else {
				anim.SetBool("Basic Bool", true);
				if (isServer) {
					changedState = "Basic Bool T";
				} else {
					CmdSecondPlayerAnim("Basic Bool T");
				}
			}
		}

		//if (Input.GetKeyDown("3") && h == 0.0f && v == 0.0f) {
			//anim.SetTrigger("Taunt Trigger");
			//changedState = "Taunt Trigger";
		//}

		if (Input.GetMouseButtonUp (0)) {
			anim.SetBool("Basic Bool", false);
			if (isServer) {
				changedState = "Basic Bool F";
			} else {
				CmdSecondPlayerAnim("Basic Bool F");
			}
			shooting = false;
			shootTimer = Time.timeSinceLevelLoad + 0.2f;
		}

		if (shooting == false && Time.timeSinceLevelLoad > shootTimer) {










			transform.position += transform.forward * Time.deltaTime * v*speedMultiplier;
			transform.position += transform.right * Time.deltaTime * (h/2.0f) * speedMultiplier;
			






			currentDistance = Vector3.Distance(mainCamera.position, this.transform.position);
			if (currentDistance != initialDistance) {
				targetPosition = cameraLookTarget.position + (transform.forward * cameraOffset.z) + (transform.up * cameraOffset.y) + (transform.right * cameraOffset.x);
				mainCamera.position = Vector3.Lerp(mainCamera.position, targetPosition, 0.5f);
			}
		}   
	}

	//public void CheckScreen() {
		//lookingAtScreen = true;
		//if (this.gameObject.CompareTag ("Player0")) {
			//mainCamera.position = checkPositionP1;
			//mainCamera.transform.eulerAngles = checkRotationP1;
		//} else if (this.gameObject.CompareTag ("Player1")) {
			//mainCamera.position = checkPositionP2;
			//mainCamera.transform.eulerAngles = checkRotationP2;
		//}


	//}


	public void setupCamera() {
		mainCamera = Camera.main.transform;
		cameraLookTarget = transform.Find ("CameraTarget");
		targetPosition = cameraLookTarget.position + (transform.forward * cameraOffset.z) + (transform.up * cameraOffset.y) + (transform.right * cameraOffset.x);
		targetRotation = cameraLookTarget.rotation;
		mainCamera.position = targetPosition;
		mainCamera.rotation = targetRotation;
	}


	void MoveCamera() {
		if (lookingAtScreen == true) {
			return;
		}

		mouseInput.x = Input.GetAxis ("Mouse X")/1.5f;
		mouseInput.y = Input.GetAxis ("Mouse Y")/1.5f;

		if (mouseInput.y > 0) {
			if (mainCamera.transform.rotation.eulerAngles.x > 0.0f && mainCamera.transform.rotation.eulerAngles.x < 300.0f) {
				mainCamera.transform.Rotate((-1*mouseInput.y),0,0);
				shootingPoint.Rotate((-1*mouseInput.y),0,0);
			}
		} else if (mouseInput.y < 0) {
			if (mainCamera.transform.rotation.eulerAngles.x < 30.0f || mainCamera.transform.rotation.eulerAngles.x > 300.0f) {
				mainCamera.transform.Rotate((-1*mouseInput.y),0,0);
				shootingPoint.Rotate((-1*mouseInput.y),0,0);
			}
		}

		transform.Rotate (0, mouseInput.x, 0);
		mainCamera.transform.RotateAround(this.transform.position,Vector3.up, mouseInput.x);



	}

    public void ForcePush() {
        anim.SetTrigger("Push Trigger");
        if (isServer) {
			changedState = "Push Trigger";
		} else {
			CmdSecondPlayerAnim("Push Trigger");
		}
    }

    public void BigShoot() {
    	anim.SetTrigger("Big Shoot Trigger");
    	if (isServer) {
			changedState = "Big Shoot Trigger";
		} else {
			CmdSecondPlayerAnim("Big Shoot Trigger");
		}
    }

    public void SpinAttack() {
    	anim.SetTrigger("Spin Trigger");
    	if (isServer) {
			changedState = "Spin Trigger";
		} else {
			CmdSecondPlayerAnim("Spin Trigger");
		}
    }

    public void Yell() {
    	anim.SetTrigger("Yell Trigger");
    	 if (isServer) {
			changedState = "Yell Trigger";
		} else {
			CmdSecondPlayerAnim("Yell Trigger");
		}
    }

    public void UpdateShooting() {
    	//anim.SetBool("Shooting Bool", true);
    	//netAnim.SetTrigger("Shoot"); // ISSUE - INTERUPTS NON NETWORKED CHARACTERS ATTACK
    	//shooting = true;
	}

    // Minions push the player
    void OnTriggerStay(Collider other) {
    	if (other.CompareTag("Enemy0") || other.CompareTag("Enemy1")) {
    		if (v > -0.01 && v < 0.01) {
    			transform.position -= transform.forward * Time.deltaTime * speedMultiplier*2.0f;
    		} else {
    			transform.position -= transform.forward * Time.deltaTime * v*speedMultiplier*2.0f;
    		}
   		 }
	}


	public void StateChangedNet(string updateState) {
		if (!playerNet.local) {
			if (updateState == "Basic Bool F") {
				anim.SetBool("Basic Bool", false);
			} else if (updateState == "Basic Bool T") {
				anim.SetBool("Basic Bool", true);
			} else {
				Debug.Log(updateState);
				anim.SetTrigger(updateState);
			}
		}
			//Debug.Log(updateState);
	}

	[Command] 
	void CmdSecondPlayerAnim (string newSet) {
		changedState = newSet;
	}
}