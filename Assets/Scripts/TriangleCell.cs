using System.Collections;
using System.Collections.Generic;

public class TriangleCell : Cell
{
    public TriangleCell()
    {
        neighbours = new List<Cell>();
        links = new List<bool>();
    }

    public override void Link(int neighbour)
    {
        // triangle cells can only have neighbour indexes between 0 and 2
        if (neighbour > 2 || neighbour < 0)
            throw new System.IndexOutOfRangeException();
        else
        {
            // checking the cell's neighbour actually exists
            if (neighbours[neighbour] != null)
            {
                neighbours[neighbour].links[neighbour] = true;
                links[neighbour] = true;
            }
            else
                throw new System.NullReferenceException();
        }
    }

    public override void Link(Cell neighbour)
    {
        throw new System.NotImplementedException();
    }

    public override void Unlink(int neighbour)
    {
        // triangle cells can only have neighbour indexes between 0 and 2
        if (neighbour > 2 || neighbour < 0)
            throw new System.IndexOutOfRangeException();
        else
        {
            // checking the cell's neighbour actually exists
            if (neighbours[neighbour] != null)
            {
                neighbours[neighbour].links[neighbour] = false;
                links[neighbour] = false;
            }
            else
                throw new System.NullReferenceException();
        }
    }

    public override void SimLink(int neighbour)
    {
        // triangle cells can only have neighbour indexes between 0 and 2
        if (neighbour > 2 || neighbour < 0)
            throw new System.IndexOutOfRangeException();
        else
        {
            // checking the cell's neighbour actually exists
            if (neighbours[neighbour] != null)
            {
                neighbours[neighbour].links[neighbour] = true;
                links[neighbour] = true;
            }
            else
                throw new System.NullReferenceException();
        }
    }

    public override void SimUnlink(int neighbour)
    {
        // triangle cells can only have neighbour indexes between 0 and 2
        if (neighbour > 2 || neighbour < 0)
            throw new System.IndexOutOfRangeException();
        else
        {
            // checking the cell's neighbour actually exists
            if (neighbours[neighbour] != null)
            {
                neighbours[neighbour].links[neighbour] = false;
                links[neighbour] = false;
            }
            else
                throw new System.NullReferenceException();
        }
    }

    
}
