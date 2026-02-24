using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public CoinObject coinPrefab;

    public Vector2 spawnSize;

    public float spawnDuration = 1;
    float spawnTimer;

    void Update()
    {
        // Count up the timer
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDuration)
        {
            spawnTimer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        // We want to find a free position on the map
        Vector2 position = transform.position;

        // This loop continues until we find a valid position
        int maxAttempts = 100;

        bool isValidPosition = false;
        while (isValidPosition == false)
        {
            // Pick a random position
            position = transform.position;
            position.x += Random.Range(-spawnSize.x, spawnSize.x);
            position.y += Random.Range(-spawnSize.y, spawnSize.y);

            // We want to make sure this position is free / not taken
            Collider2D overlapCollider = Physics2D.OverlapCircle(position, coinPrefab.radius);
            if (overlapCollider == null)
            {
                isValidPosition = true;
            }
            // Not free? Draw debug to show that we're trying again
            else
            {
                // Everytime we fail to place a coin, take an attempt away
                maxAttempts--;

                // If we run out of attempts, stop the loop
                if (maxAttempts <= 0)
                {
                    // This will exit the current function
                    Debug.Log("Ran out of attempts");
                    return;
                }

                Debug.DrawRay(position, Vector2.right * coinPrefab.radius, Color.red, 2f);
                Debug.DrawRay(position, Vector2.up * coinPrefab.radius, Color.red, 2f);
                Debug.DrawRay(position, -Vector2.right * coinPrefab.radius, Color.red, 2f);
                Debug.DrawRay(position, -Vector2.up * coinPrefab.radius, Color.red, 2f);
            }
        }

        // Put a new coin in this spot
        CoinObject newCoin = Instantiate<CoinObject>(coinPrefab);
        newCoin.transform.position = position;
    }
}
