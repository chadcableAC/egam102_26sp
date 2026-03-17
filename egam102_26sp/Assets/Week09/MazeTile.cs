using UnityEngine;

public class MazeTile : MonoBehaviour
{
    // Visuals
    public SpriteRenderer myRenderer;

    // State
    public enum TileType
    {
        Normal,
        Blocked
    }

    public TileType currentType;

    // Grid info
    public int gridX;
    public int gridY;

    public void SetMazePosition(int x, int y)
    {
        gridX = x;
        gridY = y;
    }

    public void RandomizeType()
    {
        // Assume we're the default type
        currentType = TileType.Normal;

        // 10% of the time, become blocked
        if (Random.value < 0.1)
        {
            currentType = TileType.Blocked;
        }

        // Update our visuals
        RefreshVisuals();
    }

    void RefreshVisuals()
    {
        myRenderer.color = Color.white;
        switch (currentType)
        {
            case TileType.Blocked:
                myRenderer.color = Color.red;
                break;
        }
    }
}
