using UnityEngine;
using UnityEngine.InputSystem;

public class FishingReelButton : MonoBehaviour
{
    public Transform centerHandle;

    public float reelRadius = 1f;

    public Collider2D reelCollider;

    public bool isClicked;

    public float sinkDuration = 1;
    public float sinkDegrees = 720;
    public float sinkTimer = 0;

    public Vector2 viewportPosition = new Vector2(0, 0.5f);

    public Transform rotateHandle;

    void Start()
    {
        PositionReel(reelCollider.transform.position);
    }

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse != null)
        {
            // Convert the mouse from screen to world positioning
            Vector2 mousePosition = mouse.position.ReadValue();
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);                    

            // Look for down events on the reel handle
            if (mouse.leftButton.wasPressedThisFrame)
            {
                // Find all overlaps under the mouse
                Collider2D[] hits = Physics2D.OverlapPointAll(mouseWorldPosition);
                foreach (Collider2D hit in hits)
                {
                    // If one of these is ours, then we've been clicked!
                    if (hit == reelCollider)
                    {
                        isClicked = true;
                    }
                }
            }

            // Listen for the UP event to release the reel
            if (mouse.leftButton.wasReleasedThisFrame)
            {
                isClicked = false;
            }

            if (isClicked)
            {
                // Get the position BEFORE input occurs
                Vector2 lastOffset = reelCollider.transform.localPosition;
                
                PositionReel(mouseWorldPosition);
                
                // Get the position AFTER we have input
                Vector2 currentOffset = reelCollider.transform.localPosition; 

                float degreeDelta = Vector2.SignedAngle(lastOffset, currentOffset);
                Debug.Log(degreeDelta);
            }
            else
            {
                sinkTimer += Time.deltaTime;
                if (sinkTimer < sinkDuration)
                {
                    float degreesPerSecond = sinkDegrees / sinkDuration;
                    float degreesThisFrame = degreesPerSecond * Time.deltaTime;

                    Quaternion offset = Quaternion.Euler(Vector3.forward * degreesThisFrame);
                    Vector2 newOffset = offset * reelCollider.transform.localPosition;
                    
                    Vector2 newWorldPosition = reelCollider.transform.parent.TransformPoint(newOffset);

                    PositionReel(newWorldPosition);
                }
            }
        }

        // Force our position to a specific side of the screen
        Vector2 worldPosition = Camera.main.ViewportToWorldPoint(viewportPosition);
        transform.position = worldPosition;
    }

    public void PositionReel(Vector2 position)
    {
        // Find the delta between the position and the center of the reel
        Vector2 centerPosition = centerHandle.position;
        Vector2 centerToPosDelta = position - centerPosition;

        // Normalize and multiply by the radius
        Vector2 reelOffset = reelRadius * centerToPosDelta.normalized;

        // Offset by this amount (We can use local because of our heirarchy setup)
        reelCollider.transform.localPosition = reelOffset;

        rotateHandle.right = reelOffset.normalized;
    }
}
