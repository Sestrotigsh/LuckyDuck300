using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSetting : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateGraphics(Slider slider) {
		 QualitySettings.SetQualityLevel(Mathf.RoundToInt(slider.value-1), true);
	}
}
