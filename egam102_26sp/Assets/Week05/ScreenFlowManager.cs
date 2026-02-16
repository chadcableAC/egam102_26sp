using System.Collections.Generic;
using UnityEngine;

public class ScreenFlowManager : MonoBehaviour
{
    // Define our list of possible states
    public enum Screens
    {
        Start,      // 0
        Game,       // 1
        Pause,      // 2
        Win,        // 3
        Lose        // 4
    }

    public Screens currentState;

    // The list of screens for each state
    public List<GameObject> screenHandles;

    void Start()
    {
        // Start with the Start screen
        SetCurrentScreen(Screens.Start);
    }

    public void SetCurrentScreen(Screens newState)
    {
        // Assign the current state
        currentState = newState;

        // Turn the screen game objects on/off based on the current state
        for (int i = 0; i < screenHandles.Count; i++)
        {
            // Turn the index into the Screens enum
            Screens thisState = (Screens) i;

            // Is the screens match, turn on
            if (thisState == currentState)
            {
                screenHandles[i].SetActive(true);
            }
            // Otherwise turn off
            else
            {
                screenHandles[i].SetActive(false);
            }

            // bool isMatch = thisState == currentState;
            // screenHandles[i].SetActive(isMatch);
        }
    }
}
