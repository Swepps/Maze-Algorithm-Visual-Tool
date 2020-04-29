using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass : MonoBehaviour
{
    private CellManager scm;
    public GameObject maze;

    public static bool finishedDrawing;

    void Start()
    {
        maze.SetActive(false);
        GameObject mazeClone = Instantiate(maze);
        scm = new SquareCellManager(10, 10);
        mazeClone.SetActive(true);
    }
}
