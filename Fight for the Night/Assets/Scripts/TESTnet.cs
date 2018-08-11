using UnityEngine;
using UnityEngine.Networking;

public class TESTnet : NetworkManager
{
///// NETWORK FOR THE TESTING SCENE
	public void Start()
	{
		NetworkManager.singleton.StartHost ();
	}
}