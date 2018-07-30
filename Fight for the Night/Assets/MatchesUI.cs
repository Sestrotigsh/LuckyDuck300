using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class MatchesUI : MonoBehaviour {

	public Text matchName;

	public MatchInfoSnapshot snapshot;

	public void PressButton() {
		if (snapshot.currentSize < snapshot.maxSize) {
//			NetManager.singl.TryToJoinMatch(snapshot);
		}
	}
}
