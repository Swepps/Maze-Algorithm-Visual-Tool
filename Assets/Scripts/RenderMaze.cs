using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderMaze : MonoBehaviour
{
    public GameObject line;
    public GameObject square;

    private Vector3 mazeCenter = new Vector3(1.666f, 0, 1);
    private float lineLength;

    // draws the initial maze structure before generation has occurred for a square cell maze
    public void DrawSquareMaze(CellManager cellManager)
    {
        // length of a line for the edges of the cells
        lineLength = Mathf.Min(9f / cellManager.width, 9f / cellManager.height);
        line.GetComponent<LineRenderer>().widthMultiplier = 0.3f * lineLength;

        square.transform.localScale = new Vector3(lineLength, lineLength, 1);
        square.GetComponent<SpriteRenderer>().color = Color.gray;

        for (float col = -(cellManager.width / 2f); col < cellManager.width / 2f; col++)
        {
            for (float row = -(cellManager.height / 2f); row < cellManager.height / 2f; row++)
            {
                // north wall
                line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(col * lineLength, row * lineLength + lineLength, 1));
                line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(col * lineLength + lineLength, row * lineLength + lineLength, 1));
                cellManager.Cell((int)(col + cellManager.width / 2f), (int)(row + cellManager.height / 2f)).walls[0] = Instantiate(line);
                cellManager.Cell((int)(col + cellManager.width / 2f), (int)(row + cellManager.height / 2f)).walls[0].transform.position = mazeCenter;
                if (row < cellManager.height / 2 - 1) // sets the north cell to have this line as a south wall
                {
                    cellManager.Cell((int)(col + cellManager.width / 2f), (int)(row + cellManager.height / 2f) + 1).walls[2] =
                        cellManager.Cell((int)(col + cellManager.width / 2f), (int)(row + cellManager.height / 2f)).walls[0];
                }
                if (col >= 0 && col < 1 && row == cellManager.height / 2f - 1)
                {
                    Destroy(cellManager.Cell((int)(col + cellManager.width / 2f), (int)(row + cellManager.height / 2f)).walls[0]);
                }

                // east wall
                line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(col * lineLength + lineLength, row * lineLength, 1));
                line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(col * lineLength + lineLength, row * lineLength + lineLength, 1));
                cellManager.Cell((int)(col + cellManager.width / 2f), (int)(row + cellManager.height / 2f)).walls[1] = Instantiate(line);
                cellManager.Cell((int)(col + cellManager.width / 2f), (int)(row + cellManager.height / 2f)).walls[1].transform.position = mazeCenter;
                if (col < cellManager.width / 2 - 1) // sets the east cell to have this line as a west wall
                {
                    cellManager.Cell((int)(col + cellManager.width / 2f) + 1, (int)(row + cellManager.height / 2f)).walls[3] = 
                        cellManager.Cell((int)(col + cellManager.width / 2f), (int)(row + cellManager.height / 2f)).walls[1];
                }

                // south wall
                if (row == -(cellManager.height / 2f))
                {
                    line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(col * lineLength, row * lineLength, 1));
                    line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(col * lineLength + lineLength, row * lineLength, 1));
                    cellManager.Cell((int)(col + cellManager.width / 2f), (int)(row + cellManager.height / 2f)).walls[2] = Instantiate(line);
                    cellManager.Cell((int)(col + cellManager.width / 2f), (int)(row + cellManager.height / 2f)).walls[2].transform.position = mazeCenter;
                }
                if (col >= -1 && col < 0 && row == -cellManager.height / 2f)
                {
                    Destroy(cellManager.Cell((int)(col + cellManager.width / 2f), (int)(row + cellManager.height / 2f)).walls[2]);
                }
                // west wall
                if (col == -(cellManager.width / 2f))
                {
                    line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(col * lineLength, row * lineLength, 1));
                    line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(col * lineLength, row * lineLength + lineLength, 1));
                    cellManager.Cell((int)(col + cellManager.width / 2f), (int)(row + cellManager.height / 2f)).walls[3] = Instantiate(line);
                    cellManager.Cell((int)(col + cellManager.width / 2f), (int)(row + cellManager.height / 2f)).walls[3].transform.position = mazeCenter;
                }

                // fill sprite
                square.transform.position = new Vector3(col * lineLength + 1.666f + lineLength /2, row * lineLength + lineLength/2, 2);
                cellManager.Cell((int)(col + cellManager.width / 2f), (int)(row + cellManager.height / 2f)).fillSprite = Instantiate(square);
            }
        }
    }
}
