using System.Collections;
using System.Collections.Generic;

public class HexagonCell : Cell
{
    public HexagonCell()
    {
        neighbours = new List<Cell>();
        links = new List<bool>();
    }

    public override void Link(int neighbour)
    {
        // hexagon cells can only have neighbour indexes between 0 and 5
        if (neighbour > 5 || neighbour < 0)
            throw new System.IndexOutOfRangeException();
        else
        {
            // checking the cell's neighbour actually exists
            if (neighbours[neighbour] != null)
            {
                if (neighbour > 2)
                    neighbours[neighbour].links[neighbour - 3] = true;
                else
                    neighbours[neighbour].links[neighbour + 3] = true;
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
        // hexagon cells can only have neighbour indexes between 0 and 5
        if (neighbour > 5 || neighbour < 0)
            throw new System.IndexOutOfRangeException();
        else
        {
            // checking the cell's neighbour actually exists
            if (neighbours[neighbour] != null)
            {
                if (neighbour > 2)
                    neighbours[neighbour].links[neighbour - 3] = false;
                else
                    neighbours[neighbour].links[neighbour + 3] = false;
                links[neighbour] = false;
            }
            else
                throw new System.NullReferenceException();
        }
    }

    public override void SimLink(int neighbour)
    {
        // hexagon cells can only have neighbour indexes between 0 and 5
        if (neighbour > 5 || neighbour < 0)
            throw new System.IndexOutOfRangeException();
        else
        {
            // checking the cell's neighbour actually exists
            if (neighbours[neighbour] != null)
            {
                if (neighbour > 2)
                    neighbours[neighbour].links[neighbour - 3] = true;
                else
                    neighbours[neighbour].links[neighbour + 3] = true;
                links[neighbour] = true;
            }
            else
                throw new System.NullReferenceException();
        }
    }

    public override void SimUnlink(int neighbour)
    {
        // hexagon cells can only have neighbour indexes between 0 and 5
        if (neighbour > 5 || neighbour < 0)
            throw new System.IndexOutOfRangeException();
        else
        {
            // checking the cell's neighbour actually exists
            if (neighbours[neighbour] != null)
            {
                if (neighbour > 2)
                    neighbours[neighbour].links[neighbour - 3] = false;
                else
                    neighbours[neighbour].links[neighbour + 3] = false;
                links[neighbour] = false;
            }
            else
                throw new System.NullReferenceException();
        }
    }

    
}
