using System.Collections;
using UnityEngine;

public class GridTileFollowup : MonoBehaviour
{
    public SpriteRenderer myRenderer;

    public Color colorEnter;
    public Color colorExit;

    public int gridX;
    public int gridY;

    // Shake values
    public ShakeRoutine shake;

    public Color shakeLowColor;
    public Color shakeBigColor;

    public float shakeLowStrength;
    public float shakeBigStrength;

    public float shakeLowDuration;
    public float shakeBigDuration;

    public void SetPosition(int x, int y)
    {
        gridX = x;
        gridY = y;
    }

    public void SetToEnter()
    {
        myRenderer.color = colorEnter;
    }

    public void SetToExit()
    {
        myRenderer.color = colorExit;
    }

    public IEnumerator DeleteRoutine()
    {
        // Start at a low shake, with a color change
        shake.Shake();

        shake.shakeStrength = shakeLowStrength;
        myRenderer.color = shakeLowColor;

        yield return new WaitForSeconds(shakeLowDuration);

        // End with a big shake, with another color change
        shake.shakeStrength = shakeBigStrength;
        myRenderer.color = shakeBigColor;

        yield return new WaitForSeconds(shakeBigDuration);

        // This is when the coroutine ends
        
    }
}
