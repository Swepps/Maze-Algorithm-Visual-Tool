using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationHandler : MonoBehaviour
{    
    public enum State { PLAY, PAUSE, FINISHED }
    public static State generateState = State.PLAY;
    public static bool paused = false;
    public static bool step = false;
    private GenerationAlgorithm generationAlgorithm;

    // reference to the handler for calling handler functions
    private UIHandler uiHandler;

    private static float iterations;
    public static float genSpeed;

    // Start is called before the first frame update
    void Start()
    {
        iterations = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (generateState)
        {
            case State.PLAY:
                if (paused)
                {
                    generateState = State.PAUSE;
                    break;
                }

                if (step)
                {
                    generationAlgorithm.Step();
                    generateState = State.PAUSE;
                    paused = true;
                    break;
                }

                iterations += genSpeed;
                while (iterations >= 1f)
                {
                    iterations -= 1f;
                    if (generationAlgorithm.Step())
                    {
                        generateState = State.FINISHED;
                        break;
                    }
                }                                 
                break;

            case State.FINISHED:
                uiHandler.FinishedGeneration();
                break;

            default:
                break;
        }
    }

    public void Generate(GenerationAlgorithm generationAlgorithm, UIHandler uiHandler)
    {
        this.generationAlgorithm = generationAlgorithm;
        this.uiHandler = uiHandler;
    }

    public static void Pause()
    {
        if (generateState == State.PLAY)
            generateState = State.PAUSE;
        else
        {
            generateState = State.PLAY;
            step = false;
        }
            
    }
}
