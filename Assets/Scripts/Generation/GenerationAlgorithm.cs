using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenerationAlgorithm
{
    protected CellManager cellManager;

    public GenerationAlgorithm(CellManager cm)
    {
        cellManager = cm;
    }

    // returns false if algorithm isn't finished and true if it is
    public abstract bool Step();

    // generates the entire maze with no breaks - for simulations
    public abstract void Generate();
}
