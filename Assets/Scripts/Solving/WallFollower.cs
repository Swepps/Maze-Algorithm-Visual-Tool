using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFollower : SolveAlgorithm
{

    public WallFollower(CellManager cm) : base(cm)
    {
        facing = Direction.NORTH;
        row = startRow;
        col = startCol;

        for (int col = 0; col < cellManager.width; col++)
        {
            for (int row = 0; row < cellManager.height; row++)
            {
                cellManager.Cell(col, row).fillSprite.SetActive(true);
                cellManager.Cell(col, row).SetFillColor(Color.white);
            }
        }
    }

    private enum Direction { NORTH, EAST, SOUTH, WEST};
    private Direction facing;
    private int row, col;

    // returns false if algorithm isn't finished and true if it is
    override public bool Step()
    {
        cellManager.Cell(col, row).SetFillColor(Color.Lerp(cellManager.Cell(col, row).GetFillColor(), Color.blue, 0.2f));

        if (row == endRow && col == endCol)
            return true;        

        // rotates the direction to the left once
        facing -= 1;
        if (facing < 0)
            facing = Direction.WEST;

        // rotates the direction clockwise until a link is found
        while (!cellManager.Cell(col, row).links[(int) facing])
        {
            facing++;

            if ((int) facing > 3)
                facing = Direction.NORTH;
        }

        switch (facing)
        {
            case Direction.NORTH:
                row++;
                break;
            case Direction.EAST:
                col++;
                break;
            case Direction.SOUTH:
                row--;
                break;
            case Direction.WEST:
                col--;
                break;
        }

        return false;
    }

    // solves the entire maze with no breaks - for simulations
    override public void Solve()
    {

    }
}
