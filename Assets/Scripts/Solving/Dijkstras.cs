using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstras : SolveAlgorithm
{

    public Dijkstras(CellManager cm, GameObject distanceText) : base(cm)
    {
        this.distanceText = distanceText;

        frontier = new List<Cell>();
        distances = new int[cellManager.width, cellManager.height];
        visited = new bool[cellManager.width, cellManager.height];
        row = endRow;
        col = endCol;

        frontier.Add(cellManager.Cell(startCol, startRow));
        distances[startCol, startRow] = 0;

        if (cellManager.width <= 30 && cellManager.height <= 30)
        {
            distanceTextClone = Object.Instantiate(distanceText);
            distanceTextClone.transform.position = cellManager.Cell(startCol, startRow).fillSprite.transform.position;
            distanceTextClone.transform.localScale = cellManager.Cell(startCol, startRow).fillSprite.transform.localScale;
            distanceTextClone.GetComponent<TextMesh>().text = "0";
        }

        // sets all fills to be active and white, for displaying the solution
        for (int col = 0; col < cellManager.width; col++)
        {
            for (int row = 0; row < cellManager.height; row++)
            {
                cellManager.Cell(col, row).fillSprite.SetActive(true);
                cellManager.Cell(col, row).SetFillColor(Color.white);
            }
        }
    }

    public Dijkstras(CellManager cm) : base (cm)
    {
        frontier = new List<Cell>();
        distances = new int[cellManager.width, cellManager.height];
        visited = new bool[cellManager.width, cellManager.height];

        frontier.Add(cellManager.Cell(startCol, startRow));
        distances[startCol, startRow] = 0;
    }

    private List<Cell> frontier, newFrontier;
    private readonly int[,] distances;
    private readonly bool[,] visited;
    private int row, col;
    public GameObject distanceText;
    private GameObject distanceTextClone;

    public int maxDistance;

    // returns false if algorithm isn't finished and true if it is
    override public bool Step()
    {
        if (frontier.Count == 0)
        {
            cellManager.Cell(col, row).SetFillColor(Color.cyan);

            if (row == startRow && col == startCol)
                return true;

            foreach (Cell neighbour in cellManager.Cell(col, row).neighbours)
            {
                if (neighbour != null)
                {
                    if (cellManager.Cell(col, row).Linked(neighbour))
                    {
                        if (distances[col, row] - 1 == distances[neighbour.x, neighbour.y])
                        {
                            col = neighbour.x;
                            row = neighbour.y;
                            break;
                        }
                    }
                }
            }
        }
        else
        {

            newFrontier = new List<Cell>();

            foreach (Cell c in frontier)
            {
                visited[c.x, c.y] = true;

                c.SetFillColor(Color.yellow);

                foreach (Cell neighbour in c.neighbours)
                {
                    if (neighbour != null)
                    {
                        if (c.Linked(neighbour) && !visited[neighbour.x, neighbour.y])
                        {
                            distances[neighbour.x, neighbour.y] = distances[c.x, c.y] + 1;
                            newFrontier.Add(neighbour);
                            neighbour.SetFillColor(Color.green);

                            if (cellManager.width <= 30 && cellManager.height <= 30)
                            {
                                distanceTextClone = Object.Instantiate(distanceText);
                                distanceTextClone.transform.position = neighbour.fillSprite.transform.position;
                                distanceTextClone.transform.localScale = neighbour.fillSprite.transform.localScale;
                                distanceTextClone.GetComponent<TextMesh>().text = "" + distances[neighbour.x, neighbour.y];
                            }
                        }
                    }
                }
            }

            frontier = newFrontier;
        }

        return false;
    }

    // solves the entire maze with no breaks - for simulations
    override public void Solve()
    {

    }

    // just calculates the distances array without any other features of the algorithm
    public int[,] Distances()
    {
        maxDistance = 0;

        while (frontier.Count > 0)
        {
            newFrontier = new List<Cell>();

            foreach (Cell c in frontier)
            {
                visited[c.x, c.y] = true;

                foreach (Cell neighbour in c.neighbours)
                {
                    if (neighbour != null)
                    {
                        if (c.Linked(neighbour) && !visited[neighbour.x, neighbour.y])
                        {
                            distances[neighbour.x, neighbour.y] = distances[c.x, c.y] + 1;
                            newFrontier.Add(neighbour);
                        }
                    }
                }
            }

            maxDistance++;
            frontier = newFrontier;
        }

        return distances;
    }
}
