using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLoop : MonoBehaviour {
	// Audio variables
	AudioSource audioS;
	public AudioClip soundEffect;

	// Timekeeping variables
	private float timer = 0.0f;
	public float timeBetween = 5.0f;

	// Use this for initialization
	void Start () {
		// initialise the first waiting period
		timer = timeBetween;
		// Initialise the audio source
		audioS = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		// check if it has been long enough
		if (Time.timeSinceLevelLoad > timer) {
			// Play the sound
			audioS.clip = soundEffect;
			audioS.Play();
			timer = timer + timeBetween;
		}
	}
}
