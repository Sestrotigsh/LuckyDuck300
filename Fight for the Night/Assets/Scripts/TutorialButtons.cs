using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtons : MonoBehaviour {

	// the tutorial canvas
	public GameObject Canvas;
	private tutorial gameTute;

	void Start() {
		gameTute = Canvas.GetComponent<tutorial>();
	}

	// move back a step in the tutorial
	public void Left() {
		gameTute.TutorialPics(-1);
	}

	// move forward a step in the tutorial
	public void Right() {
		gameTute.TutorialPics(1);
	}

	// Quit to the main menu
	public void QuitButton() {
        gameTute.ReturnToMenu();
    }
}
