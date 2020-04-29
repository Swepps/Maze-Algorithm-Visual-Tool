using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree : GenerationAlgorithm
{
    public BinaryTree(CellManager cm) : base(cm)
    {
        row = 0;
        col = 0;
    }

    private int row, col;

    override public bool Step()
    {
        // if last cell
        if (row == cellManager.height - 1 && col == cellManager.width - 1)
        {
            cellManager.Cell(col, row).fillSprite.SetActive(false);
            return true;
        }
        // if not at the end of a row or column
        else if (row < cellManager.height - 1 && col < cellManager.width - 1)
        {
            if (Random.value > 0.5f)
            {
                cellManager.Cell(col, row).Link(0);
            }
            else
            {
                cellManager.Cell(col, row).Link(1);
            }
            cellManager.Cell(col, row).fillSprite.SetActive(false);
            row++;
            return false;
        }  
        // top of a column
        else if (row == cellManager.height - 1)
        {
            cellManager.Cell(col, row).Link(1);
            cellManager.Cell(col, row).fillSprite.SetActive(false);
            row = 0;
            col++;
            return false;
        }
        // end of a row
        else
        {
            cellManager.Cell(col, row).Link(0);
            cellManager.Cell(col, row).fillSprite.SetActive(false);
            row++;            
            return false;
        }
        
    }

    override public void Generate()
    {
        while (row < cellManager.height && col < cellManager.width) { 
            // if not at the end of a row or column
            if (row < cellManager.height - 1 && col < cellManager.width - 1)
            {
                if (Random.value > 0.5f)
                {
                    cellManager.Cell(col, row).Link(0);
                }
                else
                {
                    cellManager.Cell(col, row).Link(1);
                }
                row++;
            }
            // top of a column
            else if (row == cellManager.height - 1)
            {
                cellManager.Cell(col, row).Link(1);
                row = 0;
                col++;
            }
            // end of a row
            else
            {
                cellManager.Cell(col, row).Link(0);
                row++;
            }
        }
    }
}
