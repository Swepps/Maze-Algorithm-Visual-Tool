using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveBacktracker : GenerationAlgorithm
{
    public RecursiveBacktracker(CellManager cm) : base(cm)
    {
        cellStack = new Stack<Cell>();
        cellStack.Push(cm.RandomCell());
        cellStack.Peek().SetFillColor(Color.red);
    }

    private Stack<Cell> cellStack;
    private List<Cell> neighbours;
    private Cell neighbour;

    override public bool Step()
    {
        if (cellStack.Count == 0)
            return true;

        // sets the neighbours list to contain the given cell's available neighbours
        GetAvailableNeighbours(cellStack.Peek());
        if (neighbours.Count == 0)
        {
            cellStack.Pop().fillSprite.SetActive(false);
        }            
        else
        {
            neighbour = neighbours[Random.Range(0, neighbours.Count )];
            cellStack.Peek().Link(neighbour);
            cellStack.Push(neighbour);
            cellStack.Peek().SetFillColor(Color.red);
        }       
        return false;
    }

    override public void Generate()
    {
        
    }

    private void GetAvailableNeighbours(Cell cell)
    {
        neighbours = new List<Cell>();

        foreach (Cell c in cell.neighbours)
        {
            if (c != null)
            {
                if (c.numLinks == 0)
                    neighbours.Add(c);
            }
        }
    }
}
