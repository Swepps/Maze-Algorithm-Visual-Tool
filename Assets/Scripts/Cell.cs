using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell
{
    public List<Cell> neighbours;   // array of all neighbouring cells
    public List<bool> links;        // array of which neighbouring cells are linked (true = linked)
    public int numLinks = 0;        // counts the number of links a cell has
    public int x, y;                // the co-ords of the cell on the grid
    public List<GameObject> walls;  // array of line object references
    public GameObject fillSprite;

    // links this cell with a neighbouring cell index
    public abstract void Link(int neighbour);

    // links this cell with a neighbouring cell index
    public abstract void Link(Cell neighbour);

    // unlinks this cell to the given neighbouring cell index
    public abstract void Unlink(int neighbour);

    // links this cell with a neighbouring cell without doing the walls
    // -- for simulation
    public abstract void SimLink(int neighbour);

    // unlinks this cell to the given neighbouring cell
    // -- for simulation
    public abstract void SimUnlink(int neighbour);

    // returns true if the given neighbour is linked
    public bool Linked(Cell neighbour)
    {
        return links[neighbours.IndexOf(neighbour)];
    }

    public void SetFillColor(Color color)
    {
        fillSprite.GetComponent<SpriteRenderer>().color = color;
    }

    public Color GetFillColor()
    {
        return fillSprite.GetComponent<SpriteRenderer>().color;
    }
}
