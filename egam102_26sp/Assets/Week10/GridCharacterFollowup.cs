using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridCharacterFollowup : MonoBehaviour
{
    // Grid info
    public GridManagerFollowup gridManager;

    public int gridX;
    public int gridY;

    public float moveDuration = 1f;
    Coroutine currentMoveRoutine;

    // Input info
    InputAction moveAction;

    void Start()
    {
        // FInd the grid manager
        gridManager = FindFirstObjectByType<GridManagerFollowup>();

        // Get the input
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        if (currentMoveRoutine != null)
        {
            // Don't do anything - the character is still moving
        }
        // The routine is null, which means it's not running
        else
        {
            // Match the grid position each frame
            Vector2 mazePosition = gridManager.GetGridPosition(gridX, gridY);
            transform.position = mazePosition;

            // Listen for a key press
            if (moveAction.WasPressedThisFrame())
            {
                Vector2 movement = moveAction.ReadValue<Vector2>();
                
                // Turn the movement into ints
                int xValue = Mathf.RoundToInt(movement.x);
                int yValue = Mathf.RoundToInt(movement.y);

                // Ask the grid about this new position - is it a good spot?
                if (gridManager.IsValidPosition(gridX + xValue, gridY + yValue))
                {
                    currentMoveRoutine = StartCoroutine(MoveRoutine(gridX + xValue, gridY + yValue));

                    // Add this to our maze position
                    gridX += xValue;
                    gridY += yValue;
                }
            }
        }
    }

    public IEnumerator MoveRoutine(int targetX, int targetY)
    {
        Vector2 fromPosition = transform.position;
        Vector2 targetPosition = gridManager.GetGridPosition(targetX, targetY);

        float moveTimer = 0;
        while (moveTimer < moveDuration)
        {
            // This turns the timer into a value between 0 and 1
            float interp = moveTimer / moveDuration;
            Vector2 position = Vector2.Lerp(fromPosition, targetPosition, interp);
            transform.position = position;

            // This will wait for a SINGLE frame
            yield return null;
            moveTimer += Time.deltaTime;
        }

        // We're done with the routine
        currentMoveRoutine = null;
    }
}
