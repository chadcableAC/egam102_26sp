using UnityEngine;
using UnityEngine.InputSystem;

public class MazeCharacter : MonoBehaviour
{
    // Grid info
    public MazeManager mazeManager;

    public int mazeX;
    public int mazeY;

    // Input
    InputAction moveAction;

    // Movement type
    public enum MovementMethod
    {
        Grid,
        Raycast
    }

    public MovementMethod currentMethod;
    
    void Start()
    {
        // Find this automatically
        mazeManager = FindFirstObjectByType<MazeManager>();
        transform.position = mazeManager.transform.position;

        // Input values
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        switch (currentMethod)
        {
            case MovementMethod.Grid:
                UpdateGrid();
                break;
            case MovementMethod.Raycast:
                UpdateRaycast();
                break;
        }
    }

    void UpdateGrid()
    {
        // Listen for a key press
        if (moveAction.WasPressedThisFrame())
        {
            Vector2 movement = moveAction.ReadValue<Vector2>();
            
            // Turn the movement into ints
            int xValue = Mathf.RoundToInt(movement.x);
            int yValue = Mathf.RoundToInt(movement.y);

            // Ask the grid about this new position - is it a good spot?
            if (mazeManager.IsValidPosition(mazeX + xValue, mazeY + yValue))
            {
                // Add this to our maze position
                mazeX += xValue;
                mazeY += yValue;
            }
        }

        // Match the grid position each frame
        Vector2 mazePosition = mazeManager.GetMazePosition(mazeX, mazeY);
        transform.position = mazePosition;
    }

    void UpdateRaycast()
    {
        // Listen for a key press
        if (moveAction.WasPressedThisFrame())
        {
            Vector2 movement = moveAction.ReadValue<Vector2>();

            Color debugColor = Color.red;

            // Fire a ray in this direction to see if we hit anything
            LayerMask layerMask = 1 << LayerMask.NameToLayer("Grid");

            Collider2D overlappingCollider = Physics2D.OverlapPoint(transform.position + (Vector3) movement, layerMask);
            if (overlappingCollider != null)
            {
                MazeTile hitTile = overlappingCollider.transform.GetComponent<MazeTile>();
                if (hitTile)
                {
                    switch (hitTile.currentType)
                    {
                        case MazeTile.TileType.Normal:
                            transform.position = hitTile.transform.position;
                            debugColor = Color.green;
                            break;
                        default:
                            debugColor = Color.yellow;
                            break;
                    }
                }
            }

            Debug.DrawRay(transform.position, movement, debugColor, 2f);
        }
    }
}
