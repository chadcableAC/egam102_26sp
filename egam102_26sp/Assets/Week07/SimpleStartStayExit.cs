using UnityEngine;

public class SimpleStartStayExit : MonoBehaviour
{
    public SpriteRenderer sprite;

    public Color startColor;
    public Color stayColor;
    public Color exitColor;

    // Enter is called ONCE per collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        sprite.color = startColor;
    }

    // Stay is called EVERY FRAME while the collision is happening
    void OnCollisionStay2D(Collision2D collision)
    {
        sprite.color = stayColor;
    }
    
    // Exit is called ONCE per collision
    void OnCollisionExit2D(Collision2D collision)
    {
        sprite.color = exitColor;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        sprite.color = startColor;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        sprite.color = stayColor;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        sprite.color = exitColor;
    }
}
