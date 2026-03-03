
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMover : MonoBehaviour
{
    // Controls
    InputAction moveAction;
    InputAction jumpAction;

    // Movement
    public bool isTransform;

    public Transform moveHandle;
    public Rigidbody2D moveRb;

    public float moveStrength;

    public float jumpForce;

    public float groundDistance;
    public float width;

    public Vector2 size;


    void Start()
    {
        // Get the input actions
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is for anything VISUAL
    void Update()
    {
        
    }

    // FixedUpdate is for anything PHSYICAL / PHYSICS BASED
    void FixedUpdate()
    {
        // Get the controller movement value (x and y)
        Vector2 movement = moveAction.ReadValue<Vector2>();

        // Transform move - what NOT to do with physics
        if (isTransform)
        {
            Vector2 movementThisFrame = movement * Time.fixedDeltaTime;
            moveHandle.position += (Vector3) movementThisFrame * moveStrength;
        }
        // Rigidbody move - the most reliable way to move with physics
        else
        {
            Vector2 goalVelocity = movement * moveStrength;

            // Keep the rigidbody's current gravity / y velocity
            goalVelocity.y = moveRb.linearVelocityY;

            moveRb.linearVelocity = goalVelocity;

            // Check to see how far away "ground" is
            bool isGrounded = false;
            for (int i = -1; i <= 1; i++)
            {
                Vector2 origin = moveRb.position;
                origin.x += i * width;

                Vector2 direction = Vector2.down;

                bool isThisGrounded = false;
                RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, groundDistance);
                foreach (RaycastHit2D hit in hits)
                {
                    // First - make sure we're not colliding with ourselves
                    if (hit.rigidbody != moveRb)
                    {
                        isThisGrounded = true;
                    }   
                }

                // RaycastHit2D hit = Physics2D.Raycast(origin, direction, groundDistance);
                if (isThisGrounded)
                {
                    Debug.DrawRay(origin, direction * groundDistance, Color.green);
                }
                else
                {
                    Debug.DrawRay(origin, direction * groundDistance, Color.red);
                }

                if (isThisGrounded)
                {
                    isGrounded = true;
                }
            }

            // RaycastHit2D[] boxHits = Physics2D.BoxCastAll(moveRb.position, size, 0, Vector2.down, groundDistance);
            // foreach (RaycastHit2D hit in boxHits)
            // {
            //     // First - make sure we're not colliding with ourselves
            //     if (hit.rigidbody != moveRb)
            //     {
            //         isGrounded = true;
            //     }   
            // }

            // Vector2 origin = moveRb.position;
            // Vector2 direction = Vector2.down;
            // if (isGrounded)
            // {
            //     Debug.DrawRay(origin, direction * groundDistance, Color.green);
            // }
            // else
            // {
            //     Debug.DrawRay(origin, direction * groundDistance, Color.red);
            // }
            
            // If we hit something, we're close enough to the ground
            if (isGrounded)
            {
                // On jump, add a force
                if (jumpAction.WasPerformedThisFrame())
                {
                    moveRb.AddForceY(jumpForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    
}
