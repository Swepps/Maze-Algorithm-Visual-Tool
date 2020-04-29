using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CellManager
{
    // 2D array which holds the cells
    protected Cell[,] cellArray;
    readonly public int width;
    readonly public int height;
    public int numDeadEnds;
    public int longestPassage;
    public float avgPassageLength;
    public float stdDevPassageLength;
    public int intersections;
    public int[,] distances;
    public int maxDistance;

    public CellManager(int width, int height)
    {
        this.width = width;
        this.height = height;
             
        InitialiseArray();
    }

    // initialising the array
    protected abstract void InitialiseArray();

    // returns the cells specified by the x and y
    public Cell Cell(int x, int y)
    {
        return cellArray[x, y];
    }

    public Cell[,] GetArray()
    {
        return cellArray;
    }

    public Cell RandomCell()
    {
        return cellArray[Random.Range(0, width), Random.Range(0, height)];
    }

    public void GatherStats()
    {
        avgPassageLength = 0;
        longestPassage = 0;
        intersections = 0;

        float passageLength;
        Cell current;
        bool[,] visited = new bool[width, height];
        List<float> passageLengths = new List<float>();
        List<float> store = new List<float>();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (cellArray[i, j].numLinks == 1)
                {
                    // number of dead ends
                    numDeadEnds++;

                    // passage lengths
                    passageLength = 1;
                    current = cellArray[i, j];
                    visited[i, j] = true;

                    while (current.numLinks <= 2)
                    {
                        foreach (Cell c in current.neighbours)
                        {
                            if (c != null)
                            {
                                if (current.Linked(c) && !visited[c.x, c.y] && c.numLinks <= 2)
                                {
                                    current = c;
                                    passageLength++;
                                    visited[current.x, current.y] = true;
                                    break;
                                }
                                else if (c.numLinks > 2)
                                {
                                    current = c;
                                    break;
                                }
                            }
                        }
                    }
                    passageLengths.Add(passageLength);

                    // longest passage
                    if (passageLength > longestPassage)
                        longestPassage = (int)passageLength;

                    // avg passage length
                    if (avgPassageLength == 0)
                        avgPassageLength = passageLength;
                    else
                        avgPassageLength = (avgPassageLength + passageLength) / 2f;
                }
                // intersections
                else if (cellArray[i, j].numLinks >= 3)
                    intersections++;
            }
        }

        // standard deviation
        foreach (float p in passageLengths)        
            store.Add((p - passageLengths.Average()) * (p - passageLengths.Average()));        

        stdDevPassageLength = Mathf.Sqrt(store.Sum() / store.Count);

        // distance array
        Dijkstras dijkstras = new Dijkstras(this);        
        distances = dijkstras.Distances();
        maxDistance = dijkstras.maxDistance;
    }
}
