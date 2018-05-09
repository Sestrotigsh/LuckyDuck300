using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour {

	[SerializeField] float power = 800f;
	[SerializeField] GameObject projectile;
	[SerializeField] Transform barrel;

	void reset() {
		barrel = transform.Find ("ShootingPoint");
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;
		if (Input.GetButtonDown ("Fire1")) {
			//CmdSpawnProjectile ();
			SpawnProjectile();

		}
	}

	//[Command]
	// void CmdSpawnProjectile() {
	void SpawnProjectile() {
		GameObject instance = Instantiate (projectile, barrel.position, barrel.rotation) as GameObject;
		instance.GetComponent<Rigidbody> ().AddForce (barrel.forward * power);
		//NetworkServer.Spawn (instance);
		Destroy (instance, 1.0f);
	}
}
