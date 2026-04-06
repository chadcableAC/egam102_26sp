using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Rigidbody2D cannonball;

    public float launchSpeed = 10;

    public float launchDelay = 1;
    float launchTimer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        launchTimer += Time.deltaTime;
        if (launchTimer > launchDelay)
        {
            launchTimer = 0;

            Rigidbody2D newCannonball = Instantiate(cannonball);
            newCannonball.transform.position = transform.position;

            Vector2 launchDirection = transform.right;
            newCannonball.AddForce(launchDirection * launchSpeed, ForceMode2D.Impulse);
        }
    }
}
