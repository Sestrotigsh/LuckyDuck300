using UnityEngine;
using UnityEngine.Networking;

public class TESTnet : NetworkManager
{
	public void Start()
	{
		NetworkManager.singleton.StartHost ();
	}
}