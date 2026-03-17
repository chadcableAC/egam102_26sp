using UnityEngine;
using UnityEngine.InputSystem;

public class GridObject : MonoBehaviour
{    
    // Grid info
    public GridTileManager gridManager;

    // Visuals
    public SpriteRenderer myRenderer;
    public Transform snappedHandle;

    // Physics
    public Collider2D myCollider;

    // Touch / input state machine
    public enum InteractStates
    {
        Nothing,
        Down,
        Drag,
        Up
    }

    public InteractStates currentState = InteractStates.Nothing;

    public bool isOverlap = false;

    void Start()
    {
        // Automatically find this script
        gridManager = FindFirstObjectByType<GridTileManager>();
    }

    void Update()
    {
        // Determine our state based on input
        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.wasPressedThisFrame)
            {
                currentState = InteractStates.Down;
            }
            else if (mouse.leftButton.wasReleasedThisFrame)
            {                
                currentState = InteractStates.Up;
            }
            else if (mouse.leftButton.isPressed)
            {
                currentState = InteractStates.Drag;
            }
            else
            {
                currentState = InteractStates.Nothing;
            }
        }

        // Run our state machine
        switch (currentState)
        {
            case InteractStates.Nothing:
                UpdateNothing();
                break;
            case InteractStates.Down:
                UpdateDown();
                break;
            case InteractStates.Drag:
                UpdateDrag();
                break;
            case InteractStates.Up:
                UpdateUp();
                break;
        }

        // Show where we would be snapped to
        Vector3 snappedPosition = gridManager.SnapWorldToGridPosition(transform.position);
        snappedHandle.position = snappedPosition;
    }

    void UpdateNothing()
    {
        // There's nothing to do
    }

    void UpdateDown()
    {
        // See if the mouse is on top of us
        isOverlap = false;

        // Where is the mouse?
        var mouse = Mouse.current;
        if (mouse != null)
        {
            Vector2 mousePosition = mouse.position.ReadValue();

            // We need to move from the SCREEN position to the WORLD position
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Are we overlapping?
            Collider2D overlappingCollider = Physics2D.OverlapPoint(worldPosition);
            if (overlappingCollider != null)
            {
                if (overlappingCollider == myCollider)
                {
                    isOverlap = true;
                }
            }
        }
    }

    void UpdateDrag()
    {
        // Simply follow the mouse position

        if (isOverlap)
        {
            // Where is the mouse?
            var mouse = Mouse.current;
            if (mouse != null)
            {
                Vector2 mousePosition = mouse.position.ReadValue();

                // We need to move from the SCREEN position to the WORLD position
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                transform.position = worldPosition;
            }
        }
    }

    void UpdateUp()
    {
        // We should snap to the grid on releasing the mouse

        if (isOverlap)
        {
            // Where is the mouse?
            var mouse = Mouse.current;
            if (mouse != null)
            {
                Vector2 mousePosition = mouse.position.ReadValue();

                // We need to move from the SCREEN position to the WORLD position
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                Vector2 snappedPosition = gridManager.SnapWorldToGridPosition(worldPosition);
                transform.position = snappedPosition;
            }
        }
    }
}
