using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prototype.NetworkLobby
{
    //Main menu, mainly only a bunch of callback called by the UI (setup throught the Inspector)
    public class LobbyMainMenu : MonoBehaviour 
    {
        public LobbyManager lobbyManager;

        public RectTransform lobbyServerList;
        public RectTransform lobbyPanel;

        public GameObject startMenu;
        public GameObject mainLight;
        public GameObject props;
        public GameObject mainPanel;
        public GameObject quit;
        public GameObject returnButton;
        public GameObject serverList;
        public GameObject navPanel;




        //public InputField ipInput;
        public InputField matchNameInput;

        public void OnEnable()
        {
            //lobbyManager.topPanel.ToggleVisibility(true);

            //ipInput.onEndEdit.RemoveAllListeners();
            //ipInput.onEndEdit.AddListener(onEndEditIP);

            matchNameInput.onEndEdit.RemoveAllListeners();
            matchNameInput.onEndEdit.AddListener(onEndEditGameName);
        }

        public void OnClickHost()
        {
            lobbyManager.StartHost();
        }

        public void OnClickJoin()
        {
            lobbyManager.ChangeTo(lobbyPanel);

            //lobbyManager.networkAddress = ipInput.text;
            lobbyManager.StartClient();

            lobbyManager.backDelegate = lobbyManager.StopClientClbk;
            //lobbyManager.DisplayIsConnecting();

            //lobbyManager.SetServerInfo("Connecting...", lobbyManager.networkAddress);
        }

        //public void OnClickDedicated()
        //{
            //lobbyManager.ChangeTo(null);
            //lobbyManager.StartServer();

            //lobbyManager.backDelegate = lobbyManager.StopServerClbk;

            //lobbyManager.SetServerInfo("Dedicated Server", lobbyManager.networkAddress);
        //}

        public void OnClickCreateMatchmakingGame()
        {
            lobbyManager.StartMatchMaker();
            lobbyManager.matchMaker.CreateMatch(
                matchNameInput.text,
                (uint)lobbyManager.maxPlayers,
                true,
				"", "", "", 0, 0,
				lobbyManager.OnMatchCreate);

            lobbyManager.backDelegate = lobbyManager.StopHost;
            lobbyManager._isMatchmaking = true;
            //lobbyManager.DisplayIsConnecting();

            //lobbyManager.SetServerInfo("Matchmaker Host", lobbyManager.matchHost);
        }

        public void OnClickOpenServerList()
        {
            lobbyManager.StartMatchMaker();
            lobbyManager.backDelegate = lobbyManager.SimpleBackClbk;
            lobbyManager.ChangeTo(lobbyServerList);
            navPanel.SetActive(true);

        }

        void onEndEditIP(string text)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClickJoin();
            }
        }

        void onEndEditGameName(string text)
        {
            /*
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClickCreateMatchmakingGame();
            }
            */
        }

        public void OnClickStart() {

            if (props == null) {
                var fooGroup = Resources.FindObjectsOfTypeAll(typeof(GameObject));
                     foreach(GameObject t in fooGroup){
                        if(t.name == "Props"){
                            props = t;
                        }
                        if (t.name == "DirectionalLight") {
                            mainLight = t;
                        }
                    }
            }




            startMenu.SetActive(false);
            props.SetActive(false);







            mainLight.SetActive(true);
            mainPanel.SetActive(true);
            quit.SetActive(true);
        }

        public void ClickBack() {
            // navPanel.SetActive(false);
        startMenu.SetActive(false);
        props.SetActive(false);
        mainPanel.SetActive(true);
        quit.SetActive(true);
        navPanel.SetActive(false);
        returnButton.SetActive(false);
        serverList.SetActive(false);
        lobbyPanel.gameObject.SetActive(false);

        }

    }
}
