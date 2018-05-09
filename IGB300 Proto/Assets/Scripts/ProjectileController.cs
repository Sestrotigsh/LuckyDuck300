using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ProjectileController : NetworkBehaviour {

	[SerializeField] float lifetime = 2f;
	[SerializeField] bool canKill = false;

	bool isLive = true;
	float age;
	//ParticleSystem explosionParticles;
	MeshRenderer projectileRenderer;

	// Use this for initialization
	void Start () {
		//explosionParticles = GetComponentInChildren<ParticleSystem> ();
		projectileRenderer = GetComponent<MeshRenderer> ();
	}

	[ServerCallback]
	void Update () {
		age += Time.deltaTime;
		if (age > lifetime) {
			NetworkServer.Destroy (gameObject);
		}
	}

	void OnCollisionEnter (Collision other) {
		if (!isLive)
			return;
		isLive = false;
		projectileRenderer.enabled = false;
		//explosionParticles.Play (true);

		if (!isServer)
			return;

		if (!canKill || other.gameObject.tag != "Enemy")
			return;
		
	}
}
