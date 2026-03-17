using UnityEngine;
using UnityEngine.InputSystem;

public class GridObjectToggle : MonoBehaviour
{
    // Grid info
    public GridTileManager gridManager;

     // Visuals
    public SpriteRenderer myRenderer;
    public Transform snappedHandle;
    public float colliderRadius = 1f;

    // Physics
    public Collider2D myCollider;

    // Input state machine
    public enum InteractStates
    {
        WaitingForClick,
        Clicked
    }

    public InteractStates currentState = InteractStates.WaitingForClick;

    // Snapping types
    public enum SnappingType
    {
        Collider,
        Circle,
        Search
    }

    public SnappingType currentType;

    void Start()
    {
        // Automatically find this script
        gridManager = FindFirstObjectByType<GridTileManager>();
    }

    void Update()
    {
        // Run the state machine logic
        switch (currentState)
        {
            case InteractStates.WaitingForClick:
                UpdateWaitingForClick();
                break;
            case InteractStates.Clicked:
                UpdateClicked();
                break;
        }

        // Show where we would be snapped to
        snappedHandle.position = GetSnappedPosition();
    }

    void UpdateWaitingForClick()
    {
        // We're waiting for a click AND the mouse overlapping

        var mouse = Mouse.current;
        if (mouse != null)
        {
            Vector2 mousePosition = mouse.position.ReadValue();

            // Convert from SCREEN to WORLD space
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // See if the object overlaps us
            Collider2D overlappingCollider = Physics2D.OverlapPoint(worldPosition);
            if (overlappingCollider != null)
            {
                if (overlappingCollider == myCollider)
                {
                    if (mouse.leftButton.wasPressedThisFrame)
                    {
                        currentState = InteractStates.Clicked;
                    }
                }
            }
        }
    }

    void UpdateClicked()
    {
        // We're following the mouse
        var mouse = Mouse.current;
        if (mouse != null)
        {
            Vector2 mousePosition = mouse.position.ReadValue();

            // Convert from SCREEN to WORLD space
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Match the position of the mouse
            transform.position = worldPosition;

            // Waiting for another click to "release" the object
            if (mouse.leftButton.wasPressedThisFrame)
            {
                currentState = InteractStates.WaitingForClick;

                // Show where we would be snapped to
                transform.position = GetSnappedPosition();
            }
        }
    }

    Vector2 GetSnappedPosition()
    {
        Vector2 worldPosition = transform.position;
        Vector2 snappedPosition = worldPosition;
        switch (currentType)
        {
            case SnappingType.Collider:
                snappedPosition = gridManager.ColliderToGridPosition(myCollider);
                break;
            case SnappingType.Circle:
                snappedPosition = gridManager.CircleToGridPosition(worldPosition, colliderRadius);
                break;
            case SnappingType.Search:
                snappedPosition = gridManager.SnapWorldToGridPositionWithSearch(worldPosition);
                break;
        }
        return snappedPosition;
    }
}
