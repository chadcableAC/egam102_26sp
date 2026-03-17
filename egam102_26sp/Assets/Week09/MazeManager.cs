using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    // Maze info
    public MazeTile tilePrefab;

    public int mazeWidth = 10;
    public int mazeHeight = 7;

    public Vector2 tileSize = Vector2.one;

    public List<MazeTile> tiles = new();

    void Start()
    {
        LayoutMaze();   
    }

    public void LayoutMaze()
    {
        // Loop through X
        for (int x = 0; x < mazeWidth; x++)
        {
            // Loop through Y
            for (int y = 0; y < mazeHeight; y++)
            {
                // Create a tile
                MazeTile newTile = Instantiate(tilePrefab);

                // Setup the tile values
                newTile.SetMazePosition(x, y);
                newTile.RandomizeType();

                // Add it to our list of tiles
                tiles.Add(newTile);
                
                // Place it correctly as one of our children
                newTile.transform.SetParent(transform);

                Vector2 position;
                position.x = x * tileSize.x;
                position.y = y * tileSize.y;
                newTile.transform.localPosition = position;
            }
        }
    }

    public Vector2 GetMazePosition(int mazeX, int mazeY)
    {
        Vector2 finalPosition = transform.position;

        // Look through the tiles, and find the matching position
        foreach (MazeTile tile in tiles)
        {
            if (tile.gridX == mazeX && tile.gridY == mazeY)
            {
                finalPosition = tile.transform.position;
                break;
            }
        }

        return finalPosition;
    }

    public bool IsValidPosition(int mazeX, int mazeY)
    {
        // Assume there is no grid position
        bool isValid = false;

        // Look through the tiles, and find the matching position
        foreach (MazeTile tile in tiles)
        {
            // This matches the X and Y?
            if (tile.gridX == mazeX && tile.gridY == mazeY)
            {
                switch (tile.currentType)
                {
                    case MazeTile.TileType.Normal:
                        isValid = true;
                        break;
                }

                break;
            }
        }

        return isValid;
    }
}
