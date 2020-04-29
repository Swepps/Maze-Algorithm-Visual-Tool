using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SolveAlgorithm
{
    protected CellManager cellManager;

    protected int startRow, startCol, endRow, endCol;

    public SolveAlgorithm(CellManager cm)
    {
        cellManager = cm;

        startRow = 0;
        endRow = cm.height - 1;

        startCol = (int) Mathf.Floor((cm.width - 1) / 2f);
        endCol = (int) Mathf.Ceil((cm.width) / 2f);
    }

    // returns false if algorithm isn't finished and true if it is
    public abstract bool Step();

    // solves the entire maze with no breaks - for simulations
    public abstract void Solve();
}
