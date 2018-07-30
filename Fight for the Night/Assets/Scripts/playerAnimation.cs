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
	Vector2 mouseInput;
	private float shootTimer;
	private bool shooting;

	public float rotateAmount;
	public float speedMultiplier;

	Animator anim;

	private bool lookingAtScreen;
	[SerializeField] Vector3 checkPositionP1;
	[SerializeField] Vector3 checkPositionP2;
	[SerializeField] Vector3 checkRotationP1;
	[SerializeField] Vector3 checkRotationP2;


	private GameObject ScreenKeyHint;

	// Use this for initialization
	void Start () {
		shootTimer = 0;
		shooting = false;
		if (!isLocalPlayer) {
			GetComponent<Animator> ().enabled = false;
			return;
		}
		// set up the third person camera
		anim = GetComponent<Animator>();
		setupCamera();
		ScreenKeyHint = GameObject.Find ("Screen Key Prompt");


		if (this.gameObject.CompareTag ("Player0")) {
			ScreenKeyHint.transform.position = new Vector3 (ScreenKeyHint.transform.position.x, ScreenKeyHint.transform.position.y, 260f);
		} else if (this.gameObject.CompareTag ("Player1")) {
			ScreenKeyHint.transform.position = new Vector3 (ScreenKeyHint.transform.position.x, ScreenKeyHint.transform.position.y, 270f);
		}
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
		transform.Rotate (0, h * rotateAmount, 0);
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
		}

//        if (anim.GetCurrentAnimatorStateInfo(0).IsName("pushing") == false) {
 //           this.GetComponent<SpellsAlien>().pushing == false;
//        }
            
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
		targetPosition = cameraLookTarget.position + (transform.forward * cameraOffset.z) + (transform.up * cameraOffset.y) + (transform.right * cameraOffset.x);
		Quaternion targetRotation = Quaternion.LookRotation (cameraLookTarget.position - targetPosition);
		mainCamera.position = Vector3.Lerp (mainCamera.transform.position, targetPosition, damping * Time.deltaTime);
		mainCamera.rotation = Quaternion.Lerp (mainCamera.rotation, targetRotation, damping * Time.deltaTime);
		mouseInput.x = Mathf.Lerp (mouseInput.x, Input.GetAxis ("Mouse X"), 1f / mouseDamping.x);
		transform.Rotate (Vector3.up * mouseInput.x * mouseSensitivity.x);
		mouseInput.y = Mathf.Lerp (mouseInput.y, Input.GetAxis ("Mouse Y"), 1f / mouseDamping.y);
		mainCamera.transform.Rotate (Vector3.left * mouseInput.y * mouseSensitivity.y);
	}

    public void ForcePush() {
        anim.SetTrigger("Push");
    }

}
