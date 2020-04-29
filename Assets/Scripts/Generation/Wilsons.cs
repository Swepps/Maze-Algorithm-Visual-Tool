using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wilsons : GenerationAlgorithm
{
    public Wilsons(CellManager cm) : base(cm)
    {
        unvisited = new List<Cell>();

        // adds all cells in the cellManager to the univisted array
        for (int i = 0; i < cellManager.width; i++)
        {
            for (int j = 0; j < cellManager.height; j++)
            {
                unvisited.Add(cellManager.Cell(i, j));
            }
        }

        // removing first cell from the unvisited list
        current = unvisited[Random.Range(0, unvisited.Count)];
        current.SetFillColor(Color.white);
        unvisited.Remove(current);

        // adding first cell to the path
        current = unvisited[Random.Range(0, unvisited.Count)];
        current.SetFillColor(Color.red);
        path = new Stack<Cell>();
        path.Push(current);
    }

    private Cell current;
    private List<Cell> unvisited;
    private Stack<Cell> path;

    override public bool Step()
    {
        
        if (!unvisited.Contains(current))
        {
            current.SetFillColor(Color.white);
            while (path.Count > 1)
            {
                current = path.Pop();
                unvisited.Remove(current);
                current.SetFillColor(Color.white);
                current.Link(path.Peek());
            }
            current = path.Pop();
            unvisited.Remove(current);
            current.SetFillColor(Color.white);

            if (unvisited.Count == 0)
                return true;

            current = unvisited[Random.Range(0, unvisited.Count)];
            current.SetFillColor(Color.red);
            path = new Stack<Cell>();
            path.Push(current);
        }
        else
        {
            
            current = GetRandomNeighbour(current);
            current.SetFillColor(Color.red);

            // if a loop is made then delete the loop from the path
            if (path.Contains(current))
            {
                while (path.Peek() != current)
                {
                    path.Pop().SetFillColor(Color.gray);
                }
            }
            else
            {
                path.Push(current);
            }            
        }

        return false;
    }

    override public void Generate()
    {
        
    }

    private Cell GetRandomNeighbour(Cell cell)
    {
        Cell c = cell.neighbours[Random.Range(0, cell.neighbours.Count)];

        while (c == null || c == path.Peek())
        {
            c = cell.neighbours[Random.Range(0, cell.neighbours.Count)];
        }
        return c;
    }
}
