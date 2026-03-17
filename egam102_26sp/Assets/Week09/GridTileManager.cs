using System.Collections.Generic;
using UnityEngine;

public class GridTileManager : MonoBehaviour
{
    // Grid values
    public GridTile tilePrefab;

    public int gridWidth = 8;
    public int gridHeight = 8;

    public Vector2 tileSize = Vector2.one;

    public List<GridTile> tiles = new();

    void Start()
    {
        // Make sure our grid is in place
        BuildGrid();
    }

    // Build a grid using the provided tiles
    void BuildGrid()
    {
        // We want to loop over all x positions
        for (int x = 0; x < gridWidth; x++)
        {
            // We want to loop over all y positions
            for (int y = 0; y < gridHeight; y++)
            {
                // Create and place the tile at each position
                GridTile newTile = Instantiate(tilePrefab);
                tiles.Add(newTile);

                // Tell the new tile where it is in the grid
                newTile.SetGridPosition(x, y, gridWidth, gridHeight);

                // Make the tile a child, so that it moves with us
                newTile.transform.SetParent(transform);

                Vector2 position = Vector2.zero;
                position.x = x * tileSize.x;
                position.y = y * tileSize.y;
                newTile.transform.localPosition = position;
            }
        }
    }

    public Vector2 SnapWorldToGridPosition(Vector2 worldPosition)
    {
        // Turn the world position into a grid-friendly position

        // Turn the WORLD position into a LOCAL position
        // Treat this position like a child
        Vector2 localPosition = transform.InverseTransformPoint(worldPosition);

        Vector2 gridPosition;
        gridPosition.x = Mathf.RoundToInt(localPosition.x / tileSize.x) * tileSize.x;
        gridPosition.y = Mathf.RoundToInt(localPosition.y / tileSize.y) * tileSize.y;

        // Move from LOCAL back to WORLD position
        Vector2 finalWorldPosition = transform.TransformPoint(gridPosition);

        return finalWorldPosition;
    }

    public Vector2 SnapWorldToGridPositionWithSearch(Vector2 worldPosition)
    {
        int bestIndex = -1;
        float bestDistance = 0;
        for (int i = 0; i < tiles.Count; i++)
        {
            // Look at the tiles one by one
            GridTile thisTile = tiles[i];
            
            // Determine the delta between the two positions (us to them)
            Vector2 thisDelta = (Vector2) thisTile.transform.position - worldPosition;
            
            // Magnitude is "math" for distance
            float thisDistance = thisDelta.magnitude;

            // If the "bestIndex" is invalid (i.e. it equals -1) OR
            // This distance is smaller than our best distance
            if (bestIndex == -1 || thisDistance < bestDistance)
            {
                bestIndex = i;
                bestDistance = thisDistance;
            }
        }

        GridTile closestTile = tiles[bestIndex];
        return closestTile.transform.position;
    }

    public Vector2 CircleToGridPosition(Vector2 worldPosition, float radius)
    {
        // We need a "mask" to only look for elements on teh GRID layer
        int layerMask = 1 << LayerMask.NameToLayer("Grid");
        int invertedMask = ~layerMask;

        // Find all of the overlapping tiles
        Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(worldPosition, radius, layerMask);

        // If there are any, find the closest
        if (overlappingColliders.Length > 0)
        {
            int bestIndex = -1;
            float bestDistance = 0;
            for (int i = 0; i < overlappingColliders.Length; i++)
            {
                // Look at the colliders one by one
                Collider2D thisCollider = overlappingColliders[i];
                
                // Determine teh delta between the two positions (us to them)
                Vector2 thisDelta = (Vector2) thisCollider.transform.position - worldPosition;
                
                // Magnitude is "math" for distance
                float thisDistance = thisDelta.magnitude;

                // If the "bestIndex" is invalid (i.e. it equals -1) OR
                // This distance is smaller than our best distance
                if (bestIndex == -1 || thisDistance < bestDistance)
                {
                    bestIndex = i;
                    bestDistance = thisDistance;
                }
            }

            Collider2D closestCollider = overlappingColliders[bestIndex];
            return closestCollider.transform.position;
        }
        // If there are not, return our position
        else
        {
            return worldPosition;
        }
    }

    public Vector2 ColliderToGridPosition(Collider2D collider)
    {
        // We need a "mask" to only look for elements on teh GRID layer
        ContactFilter2D filter = ContactFilter2D.noFilter;
        filter.layerMask = 1 << LayerMask.NameToLayer("Grid");

        // See which tiles this collider is overlapping with
        List<Collider2D> overlappingColliders = new();
        int overlappingCount = Physics2D.OverlapCollider(collider, filter, overlappingColliders);

        // Did we find any overlapping colliders?
        // (That means more than 0)
        if (overlappingCount > 0)
        {
            // Let's assume the first one in the list is closest
            // Collider2D overlappingCollider = overlappingColliders[0];
            // return overlappingCollider.transform.position;

            // Find the closest tile to this collider
            
            // We need to look through the list one by one and figure out who's closest
            int bestIndex = -1;
            float bestDistance = 0;
            for (int i = 0; i < overlappingCount; i++)
            {
                // Look at the colliders one by one
                Collider2D thisCollider = overlappingColliders[i];
                
                // Determine teh delta between the two positions (us to them)
                Vector2 thisDelta = thisCollider.transform.position - collider.transform.position;
                
                // Magnitude is "math" for distance
                float thisDistance = thisDelta.magnitude;

                // If the "bestIndex" is invalid (i.e. it equals -1) OR
                // This distance is smaller than our best distance
                if (bestIndex == -1 || thisDistance < bestDistance)
                {
                    bestIndex = i;
                    bestDistance = thisDistance;
                }
            }

            Collider2D closestCollider = overlappingColliders[bestIndex];
            return closestCollider.transform.position;
        }
        // In the case we overlap nothing, just return the collider's position
        else
        {
            return collider.transform.position;
        }
    }
}
