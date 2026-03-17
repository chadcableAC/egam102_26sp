using UnityEngine;

public class GridTile : MonoBehaviour
{
    // Visual info
    public SpriteRenderer myRenderer;

    // Grid info
    public int gridX;
    public int gridY;
    public int gridWidth;
    public int gridHeight;

    public void SetGridPosition(int x, int y, int width, int height)
    {
        // Remember where in the grid we are
        gridX = x;
        gridY = y;
        gridWidth = width;
        gridHeight = height;

        // ColorToCheckerboard();
        // ColorToGradient();
    }

    void ColorToCheckerboard()
    {
         // On odd X, color odd Y tiles dark
        if (gridX % 2 == 1)
        {
            if (gridY % 2 == 1)
            {
                myRenderer.color = Color.grey;
            }
        }

        // On even X, color even Y tiles dark
        if (gridX % 2 == 0)
        {
            if (gridY % 2 == 0)
            {
                myRenderer.color = Color.grey;
            }
        }
    }

    void ColorToGradient()
    {
        // Turn the X poistion into an "interp" (value between 0 and 1)
        float interpX = gridX / (float) gridWidth;
        float interpY = gridY / (float) gridHeight;

        // Use the interp to provide the color (White = 0, Grey = 1)
        Color gridColorX = Color.Lerp(Color.black, Color.grey, interpX);
        Color gridColorY = Color.Lerp(Color.black, Color.grey, interpY);

        Color gridColorFinal = gridColorX + gridColorY;
        gridColorFinal.a = 1;

        myRenderer.color = gridColorFinal;
    }
}
