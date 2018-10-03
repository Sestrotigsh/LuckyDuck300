using UnityEngine;
//using UnityEngine.Networking;

// public class CameraNetworkManager : NetworkManager {
public class CameraNetworkManager : MonoBehaviour {
///// MAKES CAMERA SLOWLY ROTATE WHEN PLAYER IS NOT IN COTROL
	/*
	// Variables for camera rotation
	[Header("Camera variables")]
	[SerializeField] Transform sceneCamera;
	[SerializeField] float cameraRotationRadius = 24.0f;
	[SerializeField] float cameraRotationSpeed = 3.0f;
	[SerializeField] bool canRotate = true;
	float rotation;

	/// <summary>
	/// If the game starts give camera control to the player (player is client)
	/// </summary>
	/// <param name="client">the client</param>
	public override void OnStartClient ( NetworkClient client ) {
		canRotate = false;
	}

	/// <summary>
	/// If the game starts give camera control to the player (player is host)
	/// </summary>
	public override void OnStartHost() {
		canRotate = false;
	}

	/// <summary>
	/// if the game stops return to rotating (player is client)
	/// </summary>
	public override void OnStopClient() {
		canRotate = true;
	}

	/// <summary>
	/// if the game stops return to rotating (player is host)
	/// </summary>
	public override void OnStopHost() {
		canRotate = true;
	}

	void Update() {
		// if player has control do nothing
		if (!canRotate)
			return;
		
		// otherwise rotate slowly
		rotation += cameraRotationSpeed * Time.deltaTime;
		// if rotation limit is reached reset rotation limit
		if (rotation >= 180f)
			rotation -= 180f;
		sceneCamera.position = new Vector3 (190f, 45f, 180f);
		sceneCamera.rotation = Quaternion.Euler (0f, rotation, 0f);
		sceneCamera.Translate (0f, cameraRotationRadius, -cameraRotationRadius);
		sceneCamera.LookAt (new Vector3 (190f, 45f, 180f));
	}
	*/
}
