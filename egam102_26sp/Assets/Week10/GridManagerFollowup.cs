using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManagerFollowup : MonoBehaviour
{
    public GridTileFollowup tilePrefab;

    public int gridWidth;
    public int gridHeight;

    public Vector2 tileSize = Vector2.one;

    public List<GridTileFollowup> tileList;

    // List of positions NOT to make
    public List<Vector2Int> gridSpacesToSkip;

    public Vector2Int enterSpace;
    public Vector2Int exitSpace;

    // Slowly delete the grid
    public float secondsBetweenDelete = 3;


    void Start()
    {
        // Build the grid
        LayoutGrid();

        // Start deleting tiles
        StartCoroutine(DeleteGridRoutine());
    }

    void LayoutGrid()
    {
        // We need to loop over all X and Y positions
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Check to see if this should be skipped
                Vector2Int thisSpace = new(x, y);

                // If it's in the list, skip this tile!
                if (gridSpacesToSkip.Contains(thisSpace))
                {
                    // Do nothing
                }
                // Otherwise, make the tile as normal
                else
                {
                    // Create the tile
                    GridTileFollowup newTile = MakeGridTile(x, y);
                    
                    // Add it to our list
                    tileList.Add(newTile);
                }
            }
        }

        // Make the start tile
        GridTileFollowup enterTile = MakeGridTile(enterSpace.x, enterSpace.y);
        enterTile.SetToEnter();

        // Make the end tile
        GridTileFollowup exitTile = MakeGridTile(exitSpace.x, exitSpace.y);
        exitTile.SetToExit();
    }

    public GridTileFollowup MakeGridTile(int x, int y)
    {
        // Create
        GridTileFollowup newTile = Instantiate(tilePrefab);
        
        // Setup the script
        newTile.SetPosition(x, y);

        // Reparent to us
        newTile.transform.SetParent(transform);

        // Set the position
        Vector2 position;
        position.x = x * tileSize.x;
        position.y = y * tileSize.y;
        newTile.transform.localPosition = position;

        return newTile;
    }

    IEnumerator DeleteGridRoutine()
    {
        // We want to delete a tile every x seconds
        while (tileList.Count > 0)
        {
            // Pick a random tile to remove
            int randomIndex = Random.Range(0, tileList.Count);

            // Play the "tell" - warn players this tile will dissapear
            yield return StartCoroutine(tileList[randomIndex].DeleteRoutine());

            // Then delete the tile
            Destroy(tileList[randomIndex].gameObject);
            tileList.RemoveAt(randomIndex);

            // Wait to delete the next tile
            yield return new WaitForSeconds(secondsBetweenDelete);            
        }
    }

    public Vector3 GetGridPosition(int x, int y)
    {
        Vector3 position = transform.position;
        foreach (GridTileFollowup tile in tileList)
        {
            if (tile.gridX == x && tile.gridY == y)
            {
                position = tile.transform.position;
                break;
            }
        }
        return position;
    }

    public bool IsValidPosition(int x, int y)
    {
        bool isValid = false;
        foreach (GridTileFollowup tile in tileList)
        {
            if (tile.gridX == x && tile.gridY == y)
            {
                isValid = true;
                break;
            }
        }
        return isValid;
    }
}
