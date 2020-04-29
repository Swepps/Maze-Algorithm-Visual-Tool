using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static int rows, cols;
    public Slider colsSlider, rowsSlider, speedSlider;
    public InputField colsInput, rowsInput;
    public Button genButton, pauseButton, stepButton, skipButton, solveButton, resetButton;
    public Dropdown cellTypeDropdown, genAlgorithmDropdown, solveAlgorithmDropdown;

    private CellManager cellManager;
    private GameObject mazeClone;
    public GameObject maze;

    public GameObject distanceText;

    public GenerationHandler generationHandler;
    private GenerationHandler generationHandlerClone;
    private GenerationAlgorithm genAlgorithm;

    public SolveHandler solveHandler;
    private SolveHandler solveHandlerClone;
    private SolveAlgorithm solveAlgorithm;

    public GameObject statsButton, statsPanel;
    public Text algorithm, deadEnds, longestPassage, meanPassage, stdDevPassage, intersections;
    private bool statsGenerated, biasDisplayed;

    private void Start()
    {
        rows = (int) rowsSlider.value;
        cols = (int) colsSlider.value;

        cellTypeDropdown.transform.GetComponentInChildren<Text>().text = "Cell Type";
        genAlgorithmDropdown.transform.GetComponentInChildren<Text>().text = "Generation Algorithm";
        solveAlgorithmDropdown.transform.GetComponentInChildren<Text>().text = "Solve Algorithm";

        mazeClone = Instantiate(maze);
        statsGenerated = false;
    }

    public void GenerateOnClick()
    {        
        genButton.interactable = false;
        colsSlider.interactable = false;
        colsInput.interactable = false;
        rowsSlider.interactable = false;
        rowsInput.interactable = false;
        solveButton.interactable = false;

        stepButton.interactable = true;
        skipButton.interactable = true;
        resetButton.interactable = true;

        cellManager = new SquareCellManager(cols, rows);
        mazeClone.GetComponent<RenderMaze>().DrawSquareMaze(cellManager);

        statsButton.GetComponent<Button>().interactable = false;
        statsGenerated = false;
        statsPanel.SetActive(false);

        switch (genAlgorithmDropdown.value)
        {
            case 0:
                throw new IndexOutOfRangeException();
            case 1:
                genAlgorithm = new BinaryTree(cellManager);
                algorithm.text = "Binary Tree";
                break;
            case 2:
                genAlgorithm = new RecursiveBacktracker(cellManager);
                algorithm.text = "Recursive Backtracker";
                break;
            case 3:
                genAlgorithm = new Wilsons(cellManager);
                algorithm.text = "Wilson's";
                break;
            default:
                break;

        }

        GenerationHandler.genSpeed = speedSlider.value;
        if (GenerationHandler.paused)
            GenerationHandler.generateState = GenerationHandler.State.PAUSE;
        else
            GenerationHandler.generateState = GenerationHandler.State.PLAY;

        generationHandlerClone = Instantiate(generationHandler);
        generationHandlerClone.Generate(genAlgorithm, this);
    }

    public void PauseOnClick()
    {
        if (GenerationHandler.paused)
            pauseButton.transform.GetComponentInChildren<Text>().text = "PAUSE";
        else
            pauseButton.transform.GetComponentInChildren<Text>().text = "RESUME";
        GenerationHandler.paused = !GenerationHandler.paused;
        GenerationHandler.Pause();
    }

    public void StepOnClick()
    {
        GenerationHandler.step = true;
        GenerationHandler.paused = false;
        GenerationHandler.generateState = GenerationHandler.State.PLAY;
        pauseButton.transform.GetComponentInChildren<Text>().text = "RESUME";
    }

    public void SkipOnClick()
    {
        skipButton.interactable = false;
        GenerationHandler.genSpeed = rows * cols;
        SolveHandler.solveSpeed = rows * cols;
    }

    public void SolveOnClick()
    {
        solveButton.interactable = false;
        skipButton.interactable = true;

        switch (solveAlgorithmDropdown.value)
        {
            case 0:
                throw new IndexOutOfRangeException();
            case 1:
                solveAlgorithm = new WallFollower(cellManager);
                break;
            case 2:
                solveAlgorithm = new Dijkstras(cellManager, distanceText);
                break;
            default:
                break;

        }

        SolveHandler.solveSpeed = speedSlider.value;
        SolveHandler.solveState = SolveHandler.State.PLAY;

        solveHandlerClone = Instantiate(solveHandler);
        solveHandlerClone.Solve(solveAlgorithm, this);
    }

    public void ResetOnClick()
    {
        GenerationHandler.paused = true;
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("resetable"))
        {
            Destroy(o);
        }
        genButton.interactable = true;
        pauseButton.interactable = true;
        colsSlider.interactable = true;
        colsInput.interactable = true;
        rowsSlider.interactable = true;
        rowsInput.interactable = true;

        statsButton.GetComponent<Button>().interactable = false;
        statsPanel.SetActive(false);

        if (pauseButton.transform.GetComponentInChildren<Text>().text == "PAUSE")
            GenerationHandler.paused = false;
    }

    public void StatsOnClick()
    {
        if (!statsGenerated)
        {
            cellManager.GatherStats();
            statsGenerated = true;

            deadEnds.text = "" + cellManager.numDeadEnds;
            longestPassage.text = "" + cellManager.longestPassage;
            meanPassage.text = "" + cellManager.avgPassageLength;
            stdDevPassage.text = "" + cellManager.stdDevPassageLength;
            intersections.text = "" + cellManager.intersections;
        }

        statsPanel.SetActive(!statsPanel.activeSelf);
        biasDisplayed = false;
    }

    public void BiasOnClick()
    {
        biasDisplayed = !biasDisplayed;

        if (biasDisplayed)
        {
            for (int col = 0; col < cellManager.width; col++)
            {
                for (int row = 0; row < cellManager.height; row++)
                {
                    cellManager.Cell(col, row).fillSprite.SetActive(true);
                    cellManager.Cell(col, row).SetFillColor(new Color((255 - (50f * (cellManager.distances[col, row] / (float)cellManager.maxDistance))) / 255f, (255 - (255f * (cellManager.distances[col, row] / (float)cellManager.maxDistance))) / 255f, 1));
                }
            }
        }
        else
        {
            for (int col = 0; col < cellManager.width; col++)
            {
                for (int row = 0; row < cellManager.height; row++)
                {
                    cellManager.Cell(col, row).SetFillColor(Color.white);
                }
            }
        }        
    }

    public void ColsValueChanged()
    {
        colsInput.text = "" + colsSlider.value;
        cols = (int) colsSlider.value;
    }

    public void ColsInputEnd()
    {
        if (Regex.IsMatch(colsInput.text, @"^\d+$"))
        {
            cols = Int32.Parse(colsInput.text);
            if (cols < 3)
                cols = 3;
            else if (cols > 100)
                cols = 100;
            
            colsInput.text = "" + cols;
            colsSlider.value = cols;    
        }
        else
        {
            colsInput.text = "invalid";
        }
    }

    public void RowsValueChanged()
    {
        rowsInput.text = "" + rowsSlider.value;
        rows = (int)rowsSlider.value;
    }

    public void RowsInputEnd()
    {
        if (Regex.IsMatch(rowsInput.text, @"^\d+$"))
        {
            rows = Int32.Parse(rowsInput.text);
            if (rows < 3)
                rows = 3;
            else if (rows > 100)
                rows = 100;

            rowsInput.text = "" + rows;
            rowsSlider.value = rows;
        }
        else
        {
            rowsInput.text = "invalid";
        }
    }

    public void SpeedValueChanged()
    {
        GenerationHandler.genSpeed = speedSlider.value;
        SolveHandler.solveSpeed = speedSlider.value;
        speedSlider.transform.GetComponentInChildren<Text>().text = "" + System.Math.Round(speedSlider.value, 2);
    }

    public void OnDropdownChanged(Dropdown dropdown)
    {
        if (dropdown.value == 0)
            dropdown.value = 1;
        if (cellTypeDropdown.value > 0 && genAlgorithmDropdown.value > 0)
            genButton.interactable = true;
        else
            genButton.interactable = false;
        if (solveAlgorithmDropdown.value > 0 && GenerationHandler.generateState == GenerationHandler.State.FINISHED)
            solveButton.interactable = true;
        else
            solveButton.interactable = false;
    }

    public void FinishedGeneration()
    {
        if (solveAlgorithmDropdown.value > 0)
            solveButton.interactable = true;

        stepButton.interactable = false;
        pauseButton.interactable = false;

        statsButton.GetComponent<Button>().interactable = true;
    }

    public void FinishedSolving()
    {
        skipButton.interactable = false;
        solveButton.interactable = false;
    }
}
