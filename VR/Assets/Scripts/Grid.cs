using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    public int X, Y;
    public int gCost, hCost;
    public int gridState;
    public bool visited;

    private MeshRenderer meshRenderer;
    private Collider coll;

    public Grid parent, childNeighbour;
    public List<Grid> neighbours = new List<Grid>();
    public List<Grid> connectors = new List<Grid>();

    public int fCost { get { return gCost + hCost; } }
    public bool walkable;


    void Start()
    {
        visited = false;
        meshRenderer = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();

        if(gridState == 1){
            Disable();
        }
        else if (gridState == 2){
            Enable();
        }
    }

    public void Enable()
    {
        if (meshRenderer)
            meshRenderer.enabled = true;
        if (coll)
            coll.isTrigger = false;
        walkable = false;
        MazeGenerator.singleton.AddWalledGridToList(this);
        MazeGenerator.singleton.RemoveUnwalledGridToList(this);
    }

    public void SemiEnable()
    {
        if (meshRenderer)
        {
            meshRenderer.enabled = true;
            meshRenderer.material.color = Color.green;
        }
        if (coll)
            coll.isTrigger = true;
        walkable = true;
        MazeGenerator.singleton.AddWalledGridToList(this);
        MazeGenerator.singleton.RemoveUnwalledGridToList(this);
    }

    public void Disable()
    {
        if (meshRenderer)
            meshRenderer.enabled = false;
        if (coll)
            coll.isTrigger = true;
        walkable = true;
        MazeGenerator.singleton.RemoveWalledGridToList(this);
        MazeGenerator.singleton.AddUnwalledGridToList(this);
    }

    public void AddNeighbour(Grid g)
    {
        neighbours.Add(g);
    }

    public void AddConnector(Grid g)
    {
        connectors.Add(g);
    }

    public List<Grid> getNeighbour(){
        return neighbours;
    }

    public void ResetPathStatus(){
        gCost = hCost = 99999;
    }
}
