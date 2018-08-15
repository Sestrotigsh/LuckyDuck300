using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsAlien : MonoBehaviour
{
///// SPELLS FOR THE ALIEN

	// Find the shooting position of each character. Currently at the front
	private GameObject canv; // The canvas and Text attached with this player 
	private GameObject spell1T; // Text of spell1
	private GameObject spell2T; 

	public GameObject spell1object;
	public Transform frontPos;

	// Audio part
	AudioSource audioS;
	public AudioClip laserSound;
	public AudioClip repulsionSound;
	public AudioClip basicSound;

	// Passive
	private GameObject lastHit = null;

	//Auto attack
	public GameObject projectile;
	public int baseAuto;
	public int autoDamage;
	public float fireRate = 0.4f;
	private float fireTimer = 0.0f;

	// Spell 1
	float power = 1000;
	public int spell1Damage = 10;
	private float CDTimer1 =0; // Next time the spell available
	public int CD1; // The actual CD between spell
	public float remainingTime1 = 0; // The number of seconds left before the next cast available

	// Spell 2
	private Rigidbody rb;
	float power2 = 2500;
	public int spell2Damage = 10;
	private float CDTimer2 = 0; // Next time the spell available
	public int CD2; // The actual CD between spell
	public float remainingTime2 = 0; // The number of seconds left before the next cast available
	public ParticleSystem windSpin;
	private bool isComboable = false;


	// Spell 3
	public List<Enemy> enemyList;
    public int CD3;
    public float CDTimer3 = 0;
    public float remainingTime3 = 0;
    public ParticleSystem alienExecute;
	// Spell 4


	// Use this for initialization
	void Start() {
		if (!this.GetComponent<PlayerNetwork>().local) {
			return;
		}

		audioS = GetComponent<AudioSource>();

		canv = this.transform.Find("Canvas").gameObject;
		spell1T = canv.transform.Find("Spell1").gameObject.transform.Find("Text").gameObject;
		spell2T = canv.transform.Find("Spell2").gameObject.transform.Find("Text").gameObject;
		autoDamage = baseAuto;
		foreach (Transform child in transform) if (child.CompareTag("CameraTarget")) {
			frontPos = child;
		}
		//frontPos = GameObject.FindWithTag("CameraTarget").transform;
	}

	// Update is called once per frame
	void Update()
	{
		if (!this.GetComponent<PlayerNetwork>().local) {
			return;
		}
		if (!(Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))) {
			if (Time.time >= CDTimer1 && Input.GetKeyDown ("1")) {
                if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("pushing") == true) {
                    return;
                }
                Spell1 ();

			} else if (Time.time >= CDTimer2 && Input.GetKeyDown ("2")) {
				Spell2 ();

			} else if (Input.GetMouseButton (0)) {
				if (fireTimer < Time.timeSinceLevelLoad) {
                    if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("pushing") == true) {
                        return;
                    }
					BasicAttack ();
					fireTimer = fireRate + Time.timeSinceLevelLoad;
				}
			} else if (Time.timeSinceLevelLoad >= CDTimer3 && Input.GetKeyDown("2") && isComboable == true)
            {
                ComboRepulsion();
            }
		} 
		RemainingTime();
	}

	public void Passive(GameObject obj)
	{
		if (obj == lastHit)
		{
			autoDamage = autoDamage + 2;
		} else
		{
			autoDamage = baseAuto;
		}
	}

	private void BasicAttack()
	{
		GameObject instance = Instantiate(projectile, frontPos.position, frontPos.rotation) as GameObject;
		instance.GetComponent<ProjectileController>().damage = baseAuto;
		instance.GetComponent<ProjectileController>().caster = "Alien";
		instance.GetComponent<ProjectileController>().player = this.gameObject;
		instance.GetComponent<Rigidbody>().AddForce(frontPos.transform.forward * power);        
		Destroy(instance, 1.0f);
		audioS.clip = basicSound;
		audioS.Play();
	}

	private void Spell1() // Spell 1 of the Alien, the laser
	{
		audioS.clip = laserSound;
		audioS.Play();
		GameObject instance = Instantiate(spell1object, frontPos.transform.position, frontPos.transform.rotation);
		instance.GetComponent<ProjectileController>().damage = spell1Damage;
		instance.GetComponent<Rigidbody>().AddForce(frontPos.transform.forward * power);

		//Timing Management
		CDTimer1 = Time.time + CD1;
		remainingTime1 = CD1;

		// Destroy
		Destroy(instance, 2.5f);
	}

	private void Spell2() { // Spell 2 Celestial push

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);        
		Vector3 directionPush;
		Rigidbody rbody;
		ParticleSystem partInstance = Instantiate(windSpin, transform);

		audioS.clip = repulsionSound;
		audioS.Play();
        this.GetComponent<playerAnimation>().ForcePush();

        int i = 0;
		while (i < hitColliders.Length)
		{
			if (hitColliders[i].tag == "Enemy")
			{
				Enemy enemy;
				directionPush = new Vector3( hitColliders[i].transform.position.x - transform.position.x, 0, hitColliders[i].transform.position.z - transform.position.z);
				rbody = hitColliders[i].GetComponent<Rigidbody>();
				enemy = hitColliders[i].GetComponent<Enemy>();
				enemy.Stun(3);
				rbody.AddForce(directionPush.normalized * power2);

				StartCoroutine(Wait(rbody, directionPush, enemy, partInstance));

			}
			i++;
		}

		//Timing Management
		CDTimer2 = Time.time + CD2;
		remainingTime2 = CD2;
	}

	IEnumerator Wait(Rigidbody rbody, Vector3 push, Enemy enemy, ParticleSystem part)
	{ // Time associated with spell 2
		isComboable = true;
		yield return new WaitForSeconds(2);
		if (rbody != null)
		{
			if (rbody.velocity != Vector3.zero)
			{
				rbody.velocity = Vector3.zero;
				rbody.angularVelocity = Vector3.zero;
				enemy.Die(spell2Damage);
			}
			else
			{
				enemy.Die(spell2Damage);
			}
			isComboable = false;
		}
	}

	 private void ComboRepulsion()
    {

        for (int i = 0; i < enemyList.Count; i++)
        {
            if(enemyList[i] != null)
            {
                ParticleSystem partInstance = Instantiate(alienExecute, new Vector3(enemyList[i].transform.position.x, enemyList[i].transform.position.y + 0.7f, enemyList[i].transform.position.z), Quaternion.Euler(-90, 0, 0));
                enemyList[i].Die(999999999);
            }
           
        }
        enemyList.Clear();
        CDTimer3 = Time.timeSinceLevelLoad + CD3;
        remainingTime3 = CD3;
    }

	private void RemainingTime() // Calculate the remaining time before next use, which will be used for the UI
	{
		if (remainingTime1 > 0)
		{
			remainingTime1 = CDTimer1 - Time.time;
			spell1T.GetComponent<Text>().text = remainingTime1.ToString("F2");
			if (remainingTime1 < 0)
			{
				remainingTime1 = 0;
				spell1T.GetComponent<Text>().text = "";
			}         
		}

		if (remainingTime2 > 0)
		{
			remainingTime2 = CDTimer2 - Time.time;
			spell2T.GetComponent<Text>().text = remainingTime2.ToString("F2");
			if (remainingTime2 < 0)
			{
				remainingTime2 = 0;
				spell2T.GetComponent<Text>().text = "";
			}
		}
	}
}
