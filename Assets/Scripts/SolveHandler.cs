using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolveHandler : MonoBehaviour
{
    public enum State { PLAY, FINISHED }
    public static State solveState = State.PLAY;
    private SolveAlgorithm solveAlgorithm;

    // reference to the handler for calling handler functions
    private UIHandler uiHandler;

    private static float iterations;
    public static float solveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        iterations = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (solveState)
        {
            case State.PLAY:

                iterations += solveSpeed;
                while (iterations >= 1f)
                {
                    iterations -= 1f;
                    if (solveAlgorithm.Step())
                    {
                        solveState = State.FINISHED;
                        break;
                    }
                }
                break;

            case State.FINISHED:
                uiHandler.FinishedSolving();
                break;

            default:
                break;
        }
    }

    public void Solve(SolveAlgorithm solveAlgorithm, UIHandler uiHandler)
    {
        this.solveAlgorithm = solveAlgorithm;
        this.uiHandler = uiHandler;
    }

}
