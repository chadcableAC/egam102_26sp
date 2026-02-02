using UnityEngine;

public class BallCollision : MonoBehaviour
{
    // Timing
    public float catchTimer;
    public float catchDuration = 1.0f;

    public bool isValidAttempt = false;

    public bool isCaught = false;
    public bool isLeftSide = false;

    void Update()
    {
        // Only on valid attempts
        if (isValidAttempt)
        {
            // Only if we have NOT caught the ball
            if (isCaught == false)
            {
                // Increment the timer
                float frameDuration = Time.deltaTime;
                catchTimer += frameDuration;

                // Once we pass the catch duration, we must have lost
                if (catchTimer >= catchDuration)
                {
                    // Find the BallCrash in the scene
                    BallCrash crash = FindFirstObjectByType<BallCrash>();
                    if (crash != null)
                    {
                        // Tell it which side crashed
                        crash.Crash(isLeftSide);

                        // Turn ourselves off
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D()
    {
        // Only on valid attempts
        if (isValidAttempt)
        {
            // We've collided with the hand, bounce the ball back
            isCaught = true;

            // Find our parent BallArc script, tell it to turn around
            BallArc parentArc = GetComponentInParent<BallArc>();
            if (parentArc != null)
            {
                parentArc.TurnAround();
            }
        }
    }

    void OnEnable()
    {
        // Assume this attempt is invalid
        isValidAttempt = false;

        // Found our parent BallArc
        BallArc parentArc = GetComponentInParent<BallArc>();
        if (parentArc != null)
        {
            // See if we're moving in the same direction
            bool isMovingRight = parentArc.isCountingUp;

            // Moving right AND we're on the right? Valid attempt
            if (isMovingRight && !isLeftSide)
            {
                isValidAttempt = true;
            }
            // Moving left AND we're on the left? Valid attempt
            else if (!isMovingRight && isLeftSide)
            {
                isValidAttempt = true;
            }
        }

        // We've turned on, start our timer
        catchTimer = 0;
        isCaught = false;
    }
}
