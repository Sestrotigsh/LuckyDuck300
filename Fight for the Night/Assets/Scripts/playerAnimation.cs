using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class playerAnimation : NetworkBehaviour {

	Transform cameraLookTarget;
	[SerializeField] Vector3 cameraOffset;
	[SerializeField] float damping;
	[SerializeField] Vector2 mouseDamping;
	[SerializeField] Vector2 mouseSensitivity;
	Transform mainCamera;
	Vector3 targetPosition;
	[SerializeField] Vector2 mouseInput;
	private float shootTimer;
	private bool shooting;

	public float rotateAmount;
	public float speedMultiplier;
	private Transform shootingPoint;

	Animator anim;

	private bool lookingAtScreen;
	[SerializeField] Vector3 checkPositionP1;
	[SerializeField] Vector3 checkPositionP2;
	[SerializeField] Vector3 checkRotationP1;
	[SerializeField] Vector3 checkRotationP2;
	[SerializeField] float verticalLimit;
	[SerializeField] float horizontalLimit;

	//[SerializeField] float Midpoint;

	private GameObject ScreenKeyHint;

	private float initialDistance;
	private float currentDistance;

	// Use this for initialization
	void Start () {
		shootTimer = 0;
		shooting = false;
		if (!isLocalPlayer) {
			GetComponent<Animator> ().enabled = false;
			return;
		}
		shootingPoint = GameObject.FindWithTag("ShootingPoint").transform;
		// set up the third person camera
		anim = GetComponent<Animator>();
		setupCamera();
		







		initialDistance = Vector3.Distance(mainCamera.position, this.transform.position);
		// CODE HERE TO FIND THE INITIAL DISTANCE FROM THE PLAYER TO USE LATER
		// OTHER POSSIBLE METHODS FOR SOLUTION ???
		// CHECK IF THE PLAYER HAS MOVED AND ONLY THEN MOVE THEN MOVE THE CAMERA
		//distanceToPlayer = this.transform.position.x - mainCamera.position.x;
		






		ScreenKeyHint = GameObject.Find ("Screen Key Prompt");
		if (this.gameObject.CompareTag ("Player0")) {
			ScreenKeyHint.transform.position = new Vector3 (ScreenKeyHint.transform.position.x, ScreenKeyHint.transform.position.y, 260f);
		} else if (this.gameObject.CompareTag ("Player1")) {
			ScreenKeyHint.transform.position = new Vector3 (ScreenKeyHint.transform.position.x, ScreenKeyHint.transform.position.y, 270f);
		}

		//if (this.transform.position.z > Midpoint) {
			//mainCamera.Rotate(0,180,0);
			//shootingPoint.Rotate(0,180,0);
		//}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetKeyUp ("z")) {
			lookingAtScreen = false;
			ScreenKeyHint.gameObject.SetActive (true);
		}

		if (Input.GetKey ("z")) {
			CheckScreen ();
		}
		if (lookingAtScreen == true) {
			ScreenKeyHint.gameObject.SetActive (false);
			return;
		}

		MoveCamera();
		float h = CrossPlatformInputManager.GetAxis ("Horizontal");
		float v = CrossPlatformInputManager.GetAxis ("Vertical");
		anim.SetFloat ("Speed", v);
		//transform.Rotate (0, h * rotateAmount, 0);
		transform.Rotate (0, h / 2.0f, 0);
		mainCamera.transform.RotateAround(this.transform.position,Vector3.up, h / 2.0f);

		//mainCamera.transform.Rotate (0, h / 2.0f, 0);
		if (Input.GetKeyDown (KeyCode.Space)) {
            if (shooting == false) {
                anim.SetTrigger ("Jump");
            }
			
		}
		if (!(Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))) {
			if (Input.GetMouseButton (0)) {
				anim.SetTrigger ("Shoot");
				shooting = true;
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			shooting = false;
			shootTimer = Time.timeSinceLevelLoad + 0.2f;
		}

		if (shooting == false && Time.timeSinceLevelLoad > shootTimer) {



















			transform.position += transform.forward * Time.deltaTime * v*speedMultiplier;
			currentDistance = Vector3.Distance(mainCamera.position, this.transform.position);

			// ISSUE IS WITH IF STATEMENT - DOESN'T REGISTER 
			//if (Mathf.Abs(currentDistance) > Mathf.Abs(initialDistance + 0.1f) || Mathf.Abs(currentDistance) < Mathf.Abs(initialDistance - 0.1f)) {
			if (currentDistance != initialDistance) {
				targetPosition = cameraLookTarget.position + (transform.forward * cameraOffset.z) + (transform.up * cameraOffset.y) + (transform.right * cameraOffset.x);
				mainCamera.position = Vector3.Lerp(mainCamera.position, targetPosition, 0.5f);
			//mainCamera.position = targetPosition;
				//mainCamera.transform.position = Vector3.MoveTowards(mainCamera.position, this.transform.position, (Time.deltaTime * v*speedMultiplier));
				//mainCamera.transform.position += transform.forward * Time.deltaTime * v*speedMultiplier;
			}



			//if ((currentDistToPlayer - distanceToPlayer - transform.forward * Time.deltaTime * v*speedMultiplier) =! Vector3.zero) {
				//mainCamera.transform.position += transform.forward * Time.deltaTime * v*speedMultiplier;
			//}




			//if (Mathf.Abs
				//(distanceToPlayer - currentDistToPlayer) >= Mathf.Abs(Time.deltaTime * v*speedMultiplier)) {
				//mainCamera.transform.position += transform.forward * Time.deltaTime * v*speedMultiplier;
			//}



			//if (distanceToPlayer != currentDistToPlayer) {
				//Debug.Log("player Moving");
				//if (currentDistToPlayer != holdDistToPlayer) {
					//Debug.Log("Camera Moving");
					//mainCamera.transform.position += transform.forward * Time.deltaTime * v*speedMultiplier;
					//holdDistToPlayer = currentDistToPlayer;
				//}
			//}
				









			//currentDistToPlayer = mainCamera.position.x - this.transform.position.x;


			//if (currentDistToPlayer > (distanceToPlayer + cameraDistanceTol)) {
				//mainCamera.transform.position += transform.forward * Time.deltaTime * v*speedMultiplier;
			//}
			// CODE HERE TO CHECK THE DISTANCE FROM PLAYER





			
		}   
	}

	public void CheckScreen() {
		lookingAtScreen = true;
		if (this.gameObject.CompareTag ("Player0")) {
			mainCamera.position = checkPositionP1;
			mainCamera.transform.eulerAngles = checkRotationP1;
		} else if (this.gameObject.CompareTag ("Player1")) {
			mainCamera.position = checkPositionP2;
			mainCamera.transform.eulerAngles = checkRotationP2;
		}


	}






	public void setupCamera() {
		mainCamera = Camera.main.transform;
		cameraLookTarget = transform.Find ("CameraTarget");
		targetPosition = cameraLookTarget.position + (transform.forward * cameraOffset.z) + (transform.up * cameraOffset.y) + (transform.right * cameraOffset.x);
		mainCamera.position = targetPosition;
	}






	void MoveCamera() {
		if (!isLocalPlayer) {
			return;
		}
		if (lookingAtScreen == true) {
			return;
		}





		//targetPosition = cameraLookTarget.position + (transform.forward * cameraOffset.z) + (transform.up * cameraOffset.y) + (transform.right * cameraOffset.x);
		//Quaternion targetRotation = Quaternion.LookRotation (cameraLookTarget.position - targetPosition);
		//mainCamera.position = Vector3.Lerp (mainCamera.transform.position, targetPosition, damping * Time.deltaTime);


		// ISSUE IS HERE - REVERTS IT BACK TO TARGET POSITION WITHOUT CONSIDERING MOUSE ROTATION
		// mainCamera.rotation = Quaternion.Lerp (mainCamera.rotation, targetRotation, damping * Time.deltaTime);

		mouseInput.x = Input.GetAxis ("Mouse X");
		mouseInput.y = Input.GetAxis ("Mouse Y");

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

		transform.Rotate (0, mouseInput.x * 2.0f, 0);
		mainCamera.transform.RotateAround(this.transform.position,Vector3.up, mouseInput.x * 2.0f);

		// ADD SOMETHING HERE THAT CHECKS IF THE CAMERA IS BEHIND THE PLAYER AND IF NOT MOVES THE CAMERA BEHIND THEM



		//difference = mainCamera.transform.rotation.y - this.transform.rotation.y + mouseInput.y;

		//mainCamera.transform.rotation.x;
		//if (difference < horizontalLimit && difference > (-1f*horizontalLimit)) {
		//	mainCamera.transform.Rotate(0,mouseInput.x,0);
		//}

		//difference = mainCamera.transform.rotation.y - this.transform.rotation.y + mouseInput.y;

		//if (difference < verticalLimit && difference > (-1f*verticalLimit)) {
		//	mainCamera.transform.Rotate((-1*mouseInput.y),0,0);
		//}

		//mainCamera.transform.Rotate(0,mouseInput.x,0);
		//mainCamera.transform.Rotate((-1*mouseInput.y),0,0);





		//mainCamera.transform.Rotate (Vector3.up * mouseInput.x);
		//mainCamera.transform.Rotate (Vector3.left * mouseInput.y);


		//mouseInput.x = Mathf.Lerp (mouseInput.x, Input.GetAxis ("Mouse X"), 1f / mouseDamping.x);
		//transform.Rotate (Vector3.up * mouseInput.x * mouseSensitivity.x);
		//mouseInput.y = Mathf.Lerp (mouseInput.y, Input.GetAxis ("Mouse Y"), 1f / mouseDamping.y);
		//mainCamera.transform.Rotate (Vector3.left * mouseInput.y * mouseSensitivity.y);

		//mouseInput.x = Mathf.Lerp (mouseInput.x, Input.GetAxis ("Mouse X"), 1f / mouseDamping.x);





	}

    public void ForcePush() {
        anim.SetTrigger("Push");
    }

}
