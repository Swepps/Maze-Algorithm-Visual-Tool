using System.Collections;
using System.Collections.Generic;

public class SquareCell : Cell
{
    public SquareCell()
    {
        neighbours = new List<Cell>();
        links = new List<bool>();
        walls = new List<UnityEngine.GameObject>();

        for (int i = 0; i < 4; i++)
        {
            neighbours.Add(null);
            links.Add(false);
            walls.Add(null);
        }
    }

    // simCell determines if this cell is for a simulation or not
    public SquareCell(bool simCell)
    {
        if (!simCell)
        {
            walls = new List<UnityEngine.GameObject>();
        }
        neighbours = new List<Cell>();
        links = new List<bool>();

        for (int i = 0; i < 4; i++)
        {
            neighbours.Add(null);
            links.Add(false);
            walls.Add(null);
        }
    }

    public override void Link(int neighbour)
    {
        // square cells can only have neighbour indexes between 0 and 3
        if (neighbour > 3 || neighbour < 0)
            throw new System.IndexOutOfRangeException();
        else
        {
            // checking the cell's neighbour actually exists
            if (neighbours[neighbour] != null)
            {
                if (neighbour > 1)
                    neighbours[neighbour].links[neighbour - 2] = true;
                else
                    neighbours[neighbour].links[neighbour + 2] = true;
                links[neighbour] = true;
                walls[neighbour].SetActive(false);
                numLinks++;
                neighbours[neighbour].numLinks++;
            }
            else
                throw new System.NullReferenceException();
        }
    }

    public override void Link(Cell neighbour)
    {
        if (neighbours.Contains(neighbour))
        {
            links[neighbours.IndexOf(neighbour)] = true;
            numLinks++;
            neighbour.links[neighbour.neighbours.IndexOf(this)] = true;
            neighbour.numLinks++;

            walls[neighbours.IndexOf(neighbour)].SetActive(false);
        }
        else
        {
            throw new System.NullReferenceException();
        }
    }

    public override void Unlink(int neighbour)
    {
        // square cells can only have neighbour indexes between 0 and 3
        if (neighbour > 3 || neighbour < 0)
            throw new System.IndexOutOfRangeException();
        else
        {
            // checking the cell's neighbour actually exists
            if (neighbours[neighbour] != null)
            {
                if (neighbour > 1)
                    neighbours[neighbour].links[neighbour - 2] = false;
                else
                    neighbours[neighbour].links[neighbour + 2] = false;
                links[neighbour] = false;
                walls[neighbour].SetActive(true);
                numLinks--;
                neighbours[neighbour].numLinks--;
            }
            else
                throw new System.NullReferenceException();
        }
    }

    public override void SimLink(int neighbour)
    {
        // square cells can only have neighbour indexes between 0 and 3
        if (neighbour > 3 || neighbour < 0)
            throw new System.IndexOutOfRangeException();
        else
        {
            // checking the cell's neighbour actually exists
            if (neighbours[neighbour] != null)
            {
                if (neighbour > 1)
                    neighbours[neighbour].links[neighbour - 2] = true;
                else
                    neighbours[neighbour].links[neighbour + 2] = true;
                links[neighbour] = true;
                numLinks++;
                neighbours[neighbour].numLinks++;
            }
            else
                throw new System.NullReferenceException();
        }
    }

    public override void SimUnlink(int neighbour)
    {
        // square cells can only have neighbour indexes between 0 and 3
        if (neighbour > 3 || neighbour < 0)
            throw new System.IndexOutOfRangeException();
        else
        {
            // checking the cell's neighbour actually exists
            if (neighbours[neighbour] != null)
            {
                if (neighbour > 1)
                    neighbours[neighbour].links[neighbour - 2] = false;
                else
                    neighbours[neighbour].links[neighbour + 2] = false;
                links[neighbour] = false;
                numLinks--;
                neighbours[neighbour].numLinks--;
            }
            else
                throw new System.NullReferenceException();
        }
    }
}
