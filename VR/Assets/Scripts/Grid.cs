using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

    public int X, Y;
    public int gridState;
    public bool visited;

    private MeshRenderer meshRenderer;
    private Collider coll;

    public Grid parent;
    public List<Grid> neighbours = new List<Grid>();
    public List<Grid> connectors = new List<Grid>();

    void Start()
    {
        visited = false;
        meshRenderer = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();

        if(gridState == 1)
        {
            Disable();
        }
    }

    public void Enable()
    {
        if (meshRenderer)
            meshRenderer.enabled = true;
        if (coll)
            coll.enabled = true;
    }

    public void Disable()
    {
        if (meshRenderer)
            meshRenderer.enabled = false;
        if (coll)
            coll.enabled = false;
    }

    public void AddNeighbour(Grid g)
    {
        neighbours.Add(g);
    }

    public void AddConnector(Grid g)
    {
        connectors.Add(g);
    }
}
