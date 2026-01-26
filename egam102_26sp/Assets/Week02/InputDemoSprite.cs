using UnityEngine;
using UnityEngine.InputSystem;

public class InputDemoSprite : MonoBehaviour
{
    InputAction attackAction;
    InputAction moveAction;

    public SpriteRenderer sprite;

    void Start()
    {
        // Assign the input actions based on the name
        attackAction = InputSystem.actions.FindAction("Attack");
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if the attack is pressed or released
        if (attackAction.WasPressedThisFrame())
        {
            sprite.color = Color.red;
        }

        if (attackAction.WasReleasedThisFrame())
        {
            sprite.color = Color.white;
        }

        // Get the controller movement value (x and y)
        Vector2 movement = moveAction.ReadValue<Vector2>();

        // Turn this movement into "frame safe" value
        Vector2 movementThisFrame = movement * Time.deltaTime;
        
        // Adjust our position
        transform.position += (Vector3) movementThisFrame;
    }
}
