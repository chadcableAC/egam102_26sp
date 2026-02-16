using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyController : MonoBehaviour
{
    // Define the list of possible states
    public enum EnemyStates
    {
        Idle,
        Patrol,
        Chase,
        RunAway
    }

    public EnemyStates currentState;

    // Visuals
    public SpriteRenderer enemySprite;
    Color originalColor;

    // Game state variables
    public SimplePlayer player;
    
    // Movement variables
    public float moveSpeed = 1f;

    // STATE - idle
    float idleTimer = 0;
    public float idleDuration = 2;
    public Color idleColor = Color.grey;

    // STATE - chase
    public Color chaseColor = Color.red;
    public float maximumChaseDistance = 6f;

    // STATE - patrol
    public List<Transform> patrolHandles;
    public int patrolIndex;

    public float minimumPatrolDistance = 0.5f;
    public float minimumChaseDistance = 2f;

    // STATE - run away
    public Color runawayColor = Color.blue;

    void Start()
    {
        // Remember our original color
        originalColor = enemySprite.color;

        // Find the player so we can chase / run away from them
        player = FindFirstObjectByType<SimplePlayer>();
    }

    void Update()
    {
        // Break up the Update loop based on the current state
        switch (currentState)
        {
            case EnemyStates.Idle:
                UpdateIdle();
                break;

            case EnemyStates.Patrol:
                UpdatePatrol();
                break;

            case EnemyStates.Chase:
                UpdateChase();
                break;

            case EnemyStates.RunAway:
                UpdateRunAway();
                break;
        }
    }

    void UpdateIdle()
    {
        // Stay in a idle for a certain amount of time
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            // After the duration, go back to patrol
            SetCurrentState(EnemyStates.Patrol);
        }

        // Check for player position
        CheckPlayerDistance();

        // Check for player powerup
        CheckPlayerPowerup();
    }

    void UpdatePatrol()
    {
        // Find the current patrol point
        Vector2 patrolPoint = patrolHandles[patrolIndex].position;

        // Chase that point
        // A to B = B - A
        Vector2 ourPosition = transform.position;
        Vector2 distanceFromEnemyToPoint = patrolPoint - ourPosition;
        Vector2 directionFromEnemyToPoint = distanceFromEnemyToPoint.normalized;

        // Move this distance
        Vector2 moveThisFrame = directionFromEnemyToPoint * moveSpeed * Time.deltaTime;
        transform.position += (Vector3) moveThisFrame;

        // When we get close enough, start chasing the next point
        if (distanceFromEnemyToPoint.magnitude < minimumPatrolDistance)
        {
            // Go to the next point
            patrolIndex += 1;

            // If we're the size of the list, reset the index back to 0
            if (patrolIndex >= patrolHandles.Count)
            {
                patrolIndex = 0;
            }
        }

        // Check for player position
        CheckPlayerDistance();

        // Check for player powerup
        CheckPlayerPowerup();
    }

    void UpdateChase()
    {
        // First, find the player position
        Vector2 playerPosition = player.transform.position;

        // Then, draw a line from us to the player
        Vector2 ourPosition = transform.position;

        // The direction from point A to point B = B - A
        // from enemy to player = A to B, A = enemy, B = player
        Vector2 distanceFromEnemyToPlayer = playerPosition - ourPosition;

        // Finally, follow that line
        Vector2 directionFromEnemyToPlayer = distanceFromEnemyToPlayer.normalized;

        Vector2 moveThisFrame = directionFromEnemyToPlayer * moveSpeed * Time.deltaTime;
        transform.position += (Vector3) moveThisFrame;

        // If the player is too far away, give up and idle
        if (distanceFromEnemyToPlayer.magnitude > maximumChaseDistance)
        {
            SetCurrentState(EnemyStates.Idle);
        }

        // Check for player powerup
        CheckPlayerPowerup();
    }

    void UpdateRunAway()
    {
        // Find the player position
        Vector2 playerPosition = player.transform.position;

        // Draw a line from enemy to player
        // A to B = B - A
        Vector2 ourPosition = transform.position;
        Vector2 distanceFromEnemyToPlayer = playerPosition - ourPosition;
        Vector2 directionFromEnemyToPlayer = distanceFromEnemyToPlayer.normalized;

        // Move in the opposite direction
        Vector2 moveThisFrame = directionFromEnemyToPlayer * moveSpeed * Time.deltaTime;
        transform.position -= (Vector3) moveThisFrame;

        // Stop running away once the player is no longer powered up
        if (player.isPoweredUp == false)
        {
            SetCurrentState(EnemyStates.Idle);
        }
    }

    public void SetCurrentState(EnemyStates newState)
    {
        // Remember the new state
        currentState = newState;

        // Anytime the state changes, reset our timers
        idleTimer = 0;

        // Change the enemy color based on their current state
        enemySprite.color = originalColor;
        switch (currentState)
        {
            case EnemyStates.Idle:
                enemySprite.color = idleColor;
                break;

            case EnemyStates.Chase:
                enemySprite.color = chaseColor;
                break;

            case EnemyStates.RunAway:
                enemySprite.color = runawayColor;
                break;
        }

        // When switching to patrol, find the closest point
        switch (currentState)
        {
            case EnemyStates.Patrol:
                FindClosestPatrolPoint();
                break;
        }
    }

    void CheckPlayerDistance()
    {
        // Check to see if the player is too close to us
        Vector2 playerPosition = player.transform.position;
        Vector2 ourPosition = transform.position;
        Vector2 enemyToPlayer = playerPosition - ourPosition;
        if (enemyToPlayer.magnitude < minimumChaseDistance)
        {
            // Player got too close, start chasing
            SetCurrentState(EnemyStates.Chase);
        }
    }

    void CheckPlayerPowerup()
    {
        // If the player is powered up, run away
        if (player.isPoweredUp)
        {
            SetCurrentState(EnemyStates.RunAway);
        }
    }

    void FindClosestPatrolPoint()
    {
        // Remember the closest point
        // Start with a HUGE distance, so that one of the patrol points will be closer
        int closestIndex = 0;
        float closestDistance = 100000;

        // Look through all of the patrol points, and find the one closest to us
        Vector2 ourPosition = transform.position;
        for (int i = 0; i < patrolHandles.Count; i++)
        {
            // First get the position of this point
            Vector2 patrolPosition = patrolHandles[i].position;

            // Find the distance between the point and us
            Vector2 enemyToPatrol = patrolPosition - ourPosition;

            // If this is closer, choose this point
            float patrolDistance = enemyToPatrol.magnitude;
            if (patrolDistance < closestDistance)
            {
                closestIndex = i;
                closestDistance = patrolDistance;
            }
        }

        // Use the closestIndex as the new patrol point
        patrolIndex = closestIndex;
    }
}
