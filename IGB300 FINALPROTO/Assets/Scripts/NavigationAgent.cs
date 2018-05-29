using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NavigationAgent : MonoBehaviour {

    //Navigation Variables
    public WaypointGraph graphNodes;
    public List<int> openList = new List<int>();
    public List<int> closedList = new List<int>();
    public List<int> currentPath = new List<int>();
    public List<int> greedyPaintList = new List<int>();
    public Dictionary<int, int> cameFrom = new Dictionary<int, int>();
    public int currentPathIndex = 0;
    public int currentNodeIndex = 0;
    private int currentNodePlaceholder;

    public int bestOpenListFScore(int start, int goal)
    {

        int bestIndex = 0;

        for (int i = 0; i < openList.Count; i++)
        {

            if ((Heuristic(openList[i], start) + Heuristic(openList[i], goal)) < (Heuristic(openList[bestIndex], start) + Heuristic(openList[bestIndex], goal)))
            {
                bestIndex = i;
            }
        }

        int bestNode = openList[bestIndex];
        return bestNode;
    }


    // Use this for initialization
    void Start () {
        
        //Initial node index to move to
        currentPath.Add(currentNodeIndex);
    }

    //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    // A-Star Search Method

    public List<int> AStarSearch(int start, int goal) {

        //Clear everything at start

        openList.Clear();
        closedList.Clear();
        cameFrom.Clear();

        openList.Add(start);

        float gScore = 0;
        float fScore = gScore + Heuristic(start, goal);

        while (openList.Count > 0)
        {
            //Find the Node in openList that has the lowest fScore value
            int currentNode = bestOpenListFScore(start, goal);

            //Found the end, reconstruct entire path and return
            if (currentNode == goal)
            {
                return ReconstructPath(cameFrom, currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //For each of the nodes connected to the current node
            for (int i = 0; i < graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex.Length; i++)
            {
                int thisNeighbourNode = graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex[i];

                //Ignore if neighbour node is attached
                if (!closedList.Contains(thisNeighbourNode))
                {

                    //Distance from current to the nextNode
                    float tentativeGScore = Heuristic(start, currentNode) + Heuristic(currentNode, thisNeighbourNode);

                    //Check to see if in openList or if new GScore is more sensible
                    if (!openList.Contains(thisNeighbourNode) || tentativeGScore < gScore)
                        openList.Add(thisNeighbourNode);

                    //Add to Dictionary - this neighbour came from this parent
                    if (!cameFrom.ContainsKey(thisNeighbourNode))
                        cameFrom.Add(thisNeighbourNode, currentNode);

                    gScore = tentativeGScore;
                    fScore = Heuristic(start, thisNeighbourNode) + Heuristic(thisNeighbourNode, goal);

                }

            }
        }


            return null;
    }


    // Heuristic Method
    public float Heuristic(int a, int b)
    {
        return Vector3.Distance(graphNodes.graphNodes[a].transform.position, graphNodes.graphNodes[b].transform.position);
    }

    // Reconstruct Path method / Path tracking
    public List<int> ReconstructPath(Dictionary<int, int> CF, int current)
    {

        List<int> finalPath = new List<int>();

        finalPath.Add(current);

        while (CF.ContainsKey(current))
        {

            current = CF[current];

            finalPath.Add(current);
        }

        finalPath.Reverse();

        return finalPath;
    }

    //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    // Greedy Search Method

    public List<int> GreedySearch(int currentNode, int goal, List<int> path) {

        //Look if the current node is already painted / used
        if (!greedyPaintList.Contains(currentNode))
            greedyPaintList.Add(currentNode);

        //Make a custom list that stores the current node's children nodes and H scores. Sort them by ascending order of Heuristic
        List<GreedyChildren> thisNodesChildren = new List<GreedyChildren>();

       
        for (int i = 0; i < graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex.Length; i++)
        {
            thisNodesChildren.Add(new GreedyChildren(graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex[i],
                Heuristic(graphNodes.graphNodes[currentNode].GetComponent<LinkedNodes>().linkedNodesIndex[i], goal)));
        }

        thisNodesChildren.Sort(); // Sort the list by heuristic values.

        for (int isPainted = 0; isPainted <= thisNodesChildren.Count; isPainted++)
        {
            if (!greedyPaintList.Contains(thisNodesChildren[isPainted].childID))
            {
                path.Add(thisNodesChildren[isPainted].childID);
                currentNode = thisNodesChildren[isPainted].childID;
                isPainted = thisNodesChildren.Count + 1;
                
            }
        }

        if (currentNode != goal)
        {
            GreedySearch(currentNode, goal, path);
        }

        return path;
    }

    

    public class GreedyChildren : IComparable<GreedyChildren>
    {
        public int childID { get; set; }
        public float childHScore { get; set; }

        public GreedyChildren(int childrenID, float childrenHScore)
        {
            this.childID = childrenID;
            this.childHScore = childrenHScore;
        }

        public int CompareTo(GreedyChildren other)
        {
            return this.childHScore.CompareTo(other.childHScore);
        }

    }



}


