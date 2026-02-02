using System.Collections.Generic;
using UnityEngine;

public class BallArc : MonoBehaviour
{
    // Visuals
    public List<GameObject> ballObjects;
    public float ballTimer;

    public float ballTimerSpeed = 1f;
    public bool isCountingUp = true;

    void Start()
    {
        // Default to the first position
        SetBallIndex(0);
    }

    void Update()
    {
        // Increment the timer
        float frameDuration = Time.deltaTime * ballTimerSpeed;

        // Determine if we're counting up or down
        if (isCountingUp == false)
        {
            frameDuration *= -1f;
        }

        ballTimer += frameDuration;

        // Turn the timer into a index (Remove the decimal point values)
        int ballIndex = Mathf.FloorToInt(ballTimer);
        SetBallIndex(ballIndex);
    }

    public void SetBallIndex(int ballIndex)
    {
        // Turn the sprites on and off
        for (int i = 0; i < ballObjects.Count; i++)
        {
            // Is this the game object we want to turn on?
            bool isMatch = i == ballIndex;

            GameObject ballObject = ballObjects[i];
            ballObject.SetActive(isMatch);
        }
    }

    public void TurnAround()
    {
        // Flip the value of counting up
        if (isCountingUp)
        {
            isCountingUp = false;
        }
        else
        {
            isCountingUp = true;
        }
    }
}
