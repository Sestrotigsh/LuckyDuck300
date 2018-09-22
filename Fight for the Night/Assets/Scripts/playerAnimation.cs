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
	NetworkAnimator netAnim;
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


	float h;
	float v;


	private Vector3 oldPos;
	private Vector3 newPos;

	private bool usingTowers = false;



	// Use this for initialization
	void Start () { 
		shootTimer = 0;
		shooting = false;
		if (!isLocalPlayer) {
			//oldPos = transform.position;
			this.GetComponent<AudioListener>().enabled = false;
			return;
		}


		GameObject[] shootingPointsList = GameObject.FindGameObjectsWithTag("ShootingPoint");



		shootingPoint = shootingPointsList[this.GetComponent<PlayerNetwork>().team].transform;




		//shootingPoint = GameObject.FindWithTag("ShootingPoint").transform;
		// set up the third person camera
		anim = GetComponent<Animator>();
		netAnim = GetComponent<NetworkAnimator>();
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

	//void LateFixedUpdate() {
		//if (isLocalPlayer) {
			//return;
		//}
		//newPos = transform.position;
		//if(oldPos != newPos) {
			 //anim.SetBool("Moving", true);
		//} else {
			//anim.SetBool("Moving", false);
		//}
		//oldPos = newPos;

	//}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
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


		shootingPoint.LookAt(accuracyTarget);
		h = CrossPlatformInputManager.GetAxis ("Horizontal");
		v = CrossPlatformInputManager.GetAxis ("Vertical");
		anim.SetFloat ("Speed", v);
		//transform.Rotate (0, h * rotateAmount, 0);
		transform.Rotate (0, h/1.5f, 0);
		mainCamera.transform.RotateAround(this.transform.position,Vector3.up, h/1.5f);

		//mainCamera.transform.Rotate (0, h / 2.0f, 0);
		if (Input.GetKeyDown (KeyCode.Space)) {
            if (shooting == false) {
                anim.SetTrigger ("Jump");
                netAnim.SetTrigger("Jump");
            }
			
		}

		if (Input.GetMouseButtonUp (0)) {
			shooting = false;
			shootTimer = Time.timeSinceLevelLoad + 0.2f;
		}

		if (shooting == false && Time.timeSinceLevelLoad > shootTimer) {
			transform.position += transform.forward * Time.deltaTime * v*speedMultiplier;
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
		foreach (Transform child in mainCamera) {
			if (child.CompareTag("GameController") == false) {
				accuracyTarget = child;
			}
		}
	}


	void MoveCamera() {
		if (!isLocalPlayer) {
			return;
		}
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
        anim.SetTrigger("Push");
    }

    public void BigShoot() {
    	anim.SetTrigger("BigShoot");
    }

    public void SpinAttack() {
    	anim.SetTrigger("Spin");
    }

    public void Yell() {
    	anim.SetTrigger("Yell");
    }

    public void UpdateShooting() {
    	anim.SetTrigger ("Shoot");
    	//netAnim.SetTrigger("Shoot"); // ISSUE - INTERUPTS NON NETWORKED CHARACTERS ATTACK
    	shooting = true;
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
}