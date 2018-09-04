using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonUI : MonoBehaviour {

    // Use this for initialization
    //Panel
    public GameObject subPanel;

    void OnMouseOver()
    {
        subPanel.SetActive(true);
    }

    void OnMouseExit()
    {
        subPanel.SetActive(false);
    }
}
