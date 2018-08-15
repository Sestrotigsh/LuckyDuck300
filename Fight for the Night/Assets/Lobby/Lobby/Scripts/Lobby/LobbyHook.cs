using UnityEngine;
using UnityEngine.Networking;
using System.Collections;



namespace Prototype.NetworkLobby
{
    // Subclass this and redefine the function you want
    // then add it to the lobby prefab
    public class LobbyHook : MonoBehaviour
    {
        public virtual void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer) { 
        	LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        	ChoosePlayer player = gamePlayer.GetComponent<ChoosePlayer>();
        	player.playerTeam = lobby.playerTeam;
        }
    }

}
