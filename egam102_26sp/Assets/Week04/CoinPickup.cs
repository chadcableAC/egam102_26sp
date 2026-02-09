using UnityEngine;
using UnityEngine.InputSystem;

public class CoinPickup : MonoBehaviour
{
    public ParticleSystem collectPrefab;
    public Collider2D myCollider;

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.wasPressedThisFrame)
            {
                // Get the current mouse position
                Vector2 mousePosition = mouse.position.value;

                // Convert from the screen (input) to the world
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                // We want to see if we overlap with this point
                Collider2D overlap = Physics2D.OverlapPoint(worldPosition);
                if (overlap != null)
                {
                    if (overlap == myCollider)
                    {
                        Collect();
                    }
                }
            }
        }
    }

    void Collect()
    {
        // Create and place this particle
        ParticleSystem collectFx = Instantiate(collectPrefab);
        collectFx.transform.position = transform.position;

        // Destroy ourselves
        Destroy(gameObject);
    }
}
