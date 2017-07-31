using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour {

    [SerializeField] private GameObject tile;
    [SerializeField] private Vector2 Area;
    [SerializeField] private Vector3 spawnPos;

    private bool mazeGenerated;

    private Grid[,] grids;

    [SerializeField] private List<Grid> frontiers = new List<Grid>();

    void Awake()
    {
        grids = new Grid[(int)Area.x, (int)Area.y];
        spawnPos = new Vector3(0, 0, 0);
        for (int i = 0; i < Area.x; i++) 
        {
            for (int j = 0; j < Area.y; j++)
            {
                spawnPos.x = i;
                spawnPos.z = j;
                GameObject g = Instantiate(tile, spawnPos, Quaternion.identity) as GameObject;
                g.name = "GRID " + i + " " + j;
                Grid grid = g.GetComponent<Grid>();
                grid.X = i;
                grid.Y = j;
                if (i == 0 || j == 0)
                    grid.gridState = 0;
                else if (i % 2 == 0 || j % 2 == 0)
                    grid.gridState = 2;
                else
                    grid.gridState = 1;

                grids[i, j] = grid;
            }
        }

        for (int i = 1; i < Area.x - 1; i++)
        {
            for (int j = 1; j < Area.y - 1; j++)
            {
                for (int k = i - 1; k <= i + 1; k++)
                {
                    for (int l = j - 1; l <= j + 1; l++)
                    {
                        if (k >= 1 && k < Area.x - 1 && l >= 1 && l < Area.y - 1 && !(i == k && j == l) && !(i != k && j != l))
                            grids[i, j].AddNeighbour(grids[k, l]);
                    }
                }
            }
        }

        for (int i = 1; i < Area.x - 1; i += 2)
        {
            for (int j = 1; j < Area.y - 1; j += 2)
            {
                for (int k = i - 2; k <= i + 2; k += 2)
                {
                    for (int l = j - 2; l <= j + 2; l += 2)
                    {
                        if (k >= 1 && k < Area.x - 1 && l >= 1 && l < Area.y - 1 && !(i == k && j == l) && !(i != k && j != l))
                            grids[i, j].AddConnector(grids[k, l]);
                    }
                }
            }
        }

    }

	// Use this for initialization
	void Start ()
    {
      
    }

    //public bool isAllVisited()
    //{
    //    for (int i = 1; i <= Area.x - 2; i += 2)
    //    {
    //        for (int j = 1; j <= Area.y - 2; j += 2)
    //        {
    //            if (!grids[i, j].visited)
    //                return false;
    //        }
    //    }
    //    return true;
    //}
	
	// Update is called once per frame
	void Update ()
    {
        if (!mazeGenerated)
        {
            GenerateMaze(grids[1, 1]);
            //stack.Travel(grids[1, 1]);
            mazeGenerated = true;
        }
           
	}

    void GenerateMaze(Grid initial)
    {
        Grid current = initial;
        frontiers.Add(current);
        while(frontiers.Count > 0)
        {
            current.visited = true;
            for (int i = 0; i < current.connectors.Count; i++)
            {
                if(!frontiers.Contains(current.connectors[i]) && !current.connectors[i].visited)
                {
                    current.connectors[i].parent = current;
                    frontiers.Add(current.connectors[i]);
                }
            }
            frontiers.Remove(current);
            if(frontiers.Count > 0)
            {
                int rnd = Random.Range(0, frontiers.Count);
                current = frontiers[rnd];
                current.parent.neighbours[current.parent.connectors.IndexOf(current)].Disable();
            }
        }
    }
}
