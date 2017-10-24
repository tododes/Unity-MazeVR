using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour {

    public static PathGenerator singleton { get; private set; }
    [SerializeField] private List<Grid> paths = new List<Grid>();
    public bool accessible;

    void Awake() {
        singleton = this;

    }

	// Use this for initialization
	void Start () {
        accessible = true;
	}

    public void Initialize(Grid start, Grid target){
        paths = FindPath(start, target);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spawnTeleporter(){
        if(paths.Count > 0){

        }
    }

    private int getDistance(Grid a, Grid b){
        return (int) Vector3.Distance(a.transform.position, b.transform.position);
    }

    public List<Grid> FindPath(Grid startNode, Grid targetNode)
    {
        accessible = false;
        Grid currentNode = startNode;

        List<Grid> neighbours = new List<Grid>();
        List<Grid> openSet = new List<Grid>();
        HashSet<Grid> closedSet = new HashSet<Grid>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost <= currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode){
                return RetracePath(startNode, targetNode);
            }

            neighbours = currentNode.getNeighbour();

            foreach (Grid n in neighbours)
            {
                if (!n.walkable || closedSet.Contains(n))
                    continue;

                int newCostToNeighbour = currentNode.gCost + getDistance(currentNode, n);
                if (n.gCost > newCostToNeighbour || !openSet.Contains(n))
                {
                    n.gCost = newCostToNeighbour;
                    n.hCost = getDistance(n, targetNode);
                    n.parent = currentNode;
                    if (!openSet.Contains(n))
                        openSet.Add(n);
                }
            }
        }
        return null;
        //Debug.Log("terminated");
    }

    private List<Grid> RetracePath(Grid start, Grid end)
    {
        List<Grid> path = new List<Grid>();
        Grid current = end;
        while (current != start){
            path.Add(current);
            current.parent.childNeighbour = current;
            current = current.parent;
        }
        path.Reverse();
        OnFinishPathfinding(MazeGenerator.singleton.getUnwalledGridList());
        return path;
    }

    private void OnFinishPathfinding(List<Grid> gridsToBeReset){
        foreach(Grid g in gridsToBeReset) {
            g.ResetPathStatus();
        }
        accessible = true;
    }
}
