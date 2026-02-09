using UnityEngine;

public class FlashRoutine : MonoBehaviour
{
    // The sprite we fade out
    public SpriteRenderer flashSprite = null;

    // How long to fade out
    public float flashDuration = 1f;
    float flashTimer;

    void Start()
    {
        SetAlpha(0);
    }

    void Update()
    {
        // If the timer is greater than zero
        if (flashTimer > 0)
        {
            // Decrement the timer
            flashTimer -= Time.deltaTime;

            // If the timer falls below zero, turn invisible
            if (flashTimer <= 0)
            {
                SetAlpha(0);
            }
            // Otherwise fade out the sprite
            else
            {
                // Turn our duration into an "interp" (Value between 0 and 1)
                float interp = flashTimer / flashDuration;

                // Directly apply this interp as an alpha
                SetAlpha(interp);
            }
        }
    }

    public void SetAlpha(float alpha)
    {
        // Get the sprite color
        Color spriteColor = flashSprite.color;

        // Adjust the alpha
        spriteColor.a = alpha;

        // Reassign the color
        flashSprite.color = spriteColor;
    }

    public void Flash()
    {
        // Start the timer
        flashTimer = flashDuration;
    }
}
