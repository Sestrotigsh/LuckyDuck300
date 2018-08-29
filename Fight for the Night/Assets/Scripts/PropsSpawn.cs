using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsSpawn : MonoBehaviour {

	private GameObject propsLeft;
	private GameObject propsRight;
	private bool left = false;

	// Use this for initialization
	void Start () {
		propsLeft = GameObject.FindWithTag("Props0");
		propsRight = GameObject.FindWithTag("Props1");

		if (this.tag == "Player0") {
			left = true;
			if (propsLeft != null) {
				if (this.transform.Find("AlienClothes").gameObject.activeSelf == true) {
					propsLeft.transform.Find("AlienProps").gameObject.SetActive(true);
				} else if (this.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
					propsLeft.transform.Find("SlasherProps").gameObject.SetActive(true);
				}
			}
		} else if (this.tag == "Player1") {
			left = false;
			if (propsRight != null) {
				if (this.transform.Find("AlienClothes").gameObject.activeSelf == true) {
					propsRight.transform.Find("AlienProps").gameObject.SetActive(true);
				} else if (this.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
					propsRight.transform.Find("SlasherProps").gameObject.SetActive(true);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(left == true) {
			if (propsLeft == null) {
				propsLeft = GameObject.FindWithTag("Props0");
			}
			if (propsLeft != null) {
				if (this.transform.Find("AlienClothes").gameObject.activeSelf == true) {
					propsLeft.transform.Find("AlienProps").gameObject.SetActive(true);
				} else if (this.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
					propsLeft.transform.Find("SlasherProps").gameObject.SetActive(true);
				}
			}
		} else if (left == false) {
			if (propsRight == null) {
				propsRight = GameObject.FindWithTag("Props1");
			}
			propsRight = GameObject.FindWithTag("Props1");
			if (propsRight != null) {
				if (this.transform.Find("AlienClothes").gameObject.activeSelf == true) {
					propsRight.transform.Find("AlienProps").gameObject.SetActive(true);
				} else if (this.transform.Find("SlasherClothes").gameObject.activeSelf == true) {
					propsRight.transform.Find("SlasherProps").gameObject.SetActive(true);
				}
			}
		}
	}
}
