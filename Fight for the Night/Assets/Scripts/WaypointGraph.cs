using UnityEngine;
using System.Collections;

public class WaypointGraph : MonoBehaviour {
///// CODE USED TO CONTROL WAYPOINT GRAPHS - DO NOT NEED TO ALTER THIS
	public GameObject[] graphNodes;

	// Use this for initialization
	void Start () {

        //graphNodes = GameObject.FindGameObjectsWithTag("node");

		//Assign nodes in array the index that they are in graphNodes array
		for (int i = 0; i < graphNodes.Length; i++) {
			graphNodes[i].GetComponent<LinkedNodes>().index = i;
		}
	}
}
