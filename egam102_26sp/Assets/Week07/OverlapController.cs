using UnityEngine;

public class OverlapController : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float timer;
    public float radius;

    void Update()
    {
        bool isOverlappingPlayer = false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<DummyPlayer>())
            {
                isOverlappingPlayer = true;
            }
        }

        if (isOverlappingPlayer)
        {
            timer += Time.deltaTime;
            sprite.color = Color.green;
        }
        else
        {
            timer = 0;
            sprite.color = Color.red;
        }
    }
}
