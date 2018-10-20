using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSetting : MonoBehaviour {

	float selectTimer = 0.0f;
	public Slider graphicsSlide;

	// Use this for initialization
	void Start () {
		if (graphicsSlide != null)
			graphicsSlide.interactable = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (graphicsSlide != null) {
			if (Time.timeSinceLevelLoad > selectTimer) {
				graphicsSlide.interactable = true;
			}
		}
	}

	public void UpdateGraphics(Slider slider) {
		 QualitySettings.SetQualityLevel(Mathf.RoundToInt(slider.value-1), true);
		 selectTimer = Time.timeSinceLevelLoad + 0.5f;
		 graphicsSlide.interactable = false;
	}

	public void UpdateSound(Slider slider) {
		AudioListener.volume = slider.value;
	}
}
