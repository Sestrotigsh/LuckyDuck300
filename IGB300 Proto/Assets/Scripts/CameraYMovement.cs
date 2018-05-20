using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraYMovement : MonoBehaviour {

	[SerializeField] float damping;
	[SerializeField] Vector2 mouseDamping;
	[SerializeField] Vector2 mouseSensitivity;
	Vector2 mouseInput;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		mouseInput.y = Mathf.Lerp(mouseInput.y, Input.GetAxis("Mouse Y"), 1f/mouseDamping.y);
		transform.Rotate (Vector3.left * mouseInput.y * mouseSensitivity.y);
	}
}
