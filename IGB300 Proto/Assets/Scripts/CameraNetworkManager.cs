using UnityEngine;
using UnityEngine.Networking;

public class CameraNetworkManager : NetworkManager {

	[Header("Camera variables")]
	[SerializeField] Transform sceneCamera;
	[SerializeField] float cameraRotationRadius = 24.0f;
	[SerializeField] float cameraRotationSpeed = 3.0f;
	[SerializeField] bool canRotate = true;

	float rotation;

	public override void OnStartClient ( NetworkClient client ) {
		canRotate = false;
	}

	public override void OnStartHost() {
		canRotate = false;
	}

	public override void OnStopClient() {
		canRotate = true;
	}

	public override void OnStopHost() {
		canRotate = true;
	}

	void Update() {
		if (!canRotate)
			return;

		rotation += cameraRotationSpeed * Time.deltaTime;
		if (rotation >= 180f)
			rotation -= 180f;
		
		sceneCamera.position = new Vector3 (190f, 45f, 180f);
		sceneCamera.rotation = Quaternion.Euler (0f, rotation, 0f);
		sceneCamera.Translate (0f, cameraRotationRadius, -cameraRotationRadius);
		sceneCamera.LookAt (new Vector3 (190f, 45f, 180f));
	}
}
