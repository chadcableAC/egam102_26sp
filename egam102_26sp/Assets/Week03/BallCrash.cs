using UnityEngine;

public class BallCrash : MonoBehaviour
{
    public GameObject crashLeft;
    public GameObject crashRight;

    void Start()
    {
        // Turn both sides off to start
        crashLeft.SetActive(false);
        crashRight.SetActive(false);
    }

    public void Crash(bool isLeft)
    {
        // Turn on the correct side
        crashLeft.SetActive(isLeft);
        crashRight.SetActive(isLeft == false);

        // Find ALL of the BallArcs and disable their script (This stops the arc from moving)
        BallArc[] allArcs = FindObjectsByType<BallArc>(FindObjectsSortMode.None);
        foreach (BallArc arc in allArcs)
        {
            arc.enabled = false;
        }
    }
}
