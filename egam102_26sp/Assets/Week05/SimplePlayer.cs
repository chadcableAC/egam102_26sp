using UnityEngine;
using UnityEngine.InputSystem;

public class SimplePlayer : MonoBehaviour
{
    // Control player
    InputAction moveAction;
    InputAction powerupAction;

    // Contorls for speed
    public float moveSpeed = 1f;

    public bool isPoweredUp = false;

    public SpriteRenderer mySprite;
    public Color powerupColor;
    Color originalColor;

    void Start()
    {
        // Remember the original color
        originalColor = mySprite.color;

        // Get the input actions
        moveAction = InputSystem.actions.FindAction("Move");
        powerupAction = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        // Get the controller movement value (x and y)
        Vector2 movement = moveAction.ReadValue<Vector2>();

        // Turn this movement into "frame safe" value
        Vector2 movementThisFrame = movement * Time.deltaTime;
        
        // Adjust our position
        transform.position += (Vector3) movementThisFrame * moveSpeed;

        // Check to see if we have a powerup
        isPoweredUp = powerupAction.IsPressed();

        // Change our color based on the powerup status
        mySprite.color = originalColor;
        if (isPoweredUp)
        {
            mySprite.color = powerupColor;
        }
    }
}
