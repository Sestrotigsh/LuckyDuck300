using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour {

    /*
    public LobbyManager lobbyManager;

    public RectTransform lobbyServerList;
    public RectTransform lobbyPanel;
    /*
    public GameObject startMenu;
    public GameObject mainLight;
    public GameObject props;
    public GameObject mainPanel;
    public GameObject quit;
    public GameObject returnButton;
    public GameObject serverList;
    public GameObject navPanel;
    */

    public GameObject controlsPanel;
    public GameObject abilitiesPanel;
    public GameObject towersPanel;
    public GameObject goldPanel;
    public GameObject minionsPanel;



    /*
    public void ClickXPanel()
    {
        // navPanel.SetActive(false);
        controlsPanel.SetActive(false);
        abilitiesPanel.SetActive(false);
        towersPanel.SetActive(false);
        goldPanel.SetActive(false);
        minionsPanel.SetActive(false);
    }*/


    //Controls
    public void ClickControlsPanel()
    {
        // navPanel.SetActive(false);
        controlsPanel.SetActive(true);
        abilitiesPanel.SetActive(false);
        towersPanel.SetActive(false);
        goldPanel.SetActive(false);
        minionsPanel.SetActive(false);
    }
    //Abilities
    public void ClickAbilitiesPanel()
    {
        // navPanel.SetActive(false);
        controlsPanel.SetActive(false);
        abilitiesPanel.SetActive(true);
        towersPanel.SetActive(false);
        goldPanel.SetActive(false);
        minionsPanel.SetActive(false);
    }
    //Towers
    public void ClickTowersPanel()
    {
        // navPanel.SetActive(false);
        controlsPanel.SetActive(false);
        abilitiesPanel.SetActive(false);
        towersPanel.SetActive(true);
        goldPanel.SetActive(false);
        minionsPanel.SetActive(false);
    }

    //Gold
    public void ClickGoldPanel()
    {
        // navPanel.SetActive(false);
        controlsPanel.SetActive(false);
        abilitiesPanel.SetActive(false);
        towersPanel.SetActive(false);
        goldPanel.SetActive(true);
        minionsPanel.SetActive(false);
    }

    //Minions
    public void ClickMinionsPanel()
    {
        // navPanel.SetActive(false);
        controlsPanel.SetActive(false);
        abilitiesPanel.SetActive(false);
        towersPanel.SetActive(false);
        goldPanel.SetActive(false);
        minionsPanel.SetActive(true);
    }

}
