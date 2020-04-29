using System.Collections;
using System.Collections.Generic;

public class SquareCellManager : CellManager
{
    public SquareCellManager(int width, int height) : base(width, height)
    {
        
    }

    protected override void InitialiseArray()
    {
        // initialising the array with squarecells
        cellArray = new Cell[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                cellArray[i, j] = new SquareCell();
                cellArray[i, j].x = i;
                cellArray[i, j].y = j;
            }
        }

        // setting the neighbours of each cell
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (j < height - 1) // north neighbour
                    cellArray[i, j].neighbours[0] = cellArray[i, j + 1];
                if (i < width - 1)  // east neighbour
                    cellArray[i, j].neighbours[1] = cellArray[i + 1, j];
                if (j > 0)          // south neighbour
                    cellArray[i, j].neighbours[2] = cellArray[i, j - 1];
                if (i > 0)          // west neighbour
                    cellArray[i, j].neighbours[3] = cellArray[i - 1, j];
            }
        }
    }
}
