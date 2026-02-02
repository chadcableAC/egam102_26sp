using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallCharacter : MonoBehaviour
{
    // Visuals
    public List<GameObject> bodyGameObjects;
    public int bodyIndex = 0;

    // Input
    InputAction leftAction;
    InputAction rightAction;

    void Start()
    {
        // Default to the middle position
        SetBodyIndex(1);

        // Get a reference to theinput actions
        leftAction = InputSystem.actions.FindAction("Left");
        rightAction = InputSystem.actions.FindAction("Right");
    }

    void Update()
    {
        if (leftAction.WasPressedThisFrame())
        {
            // Move down an index IF that would stay greater than or equal to zero
            if (bodyIndex - 1 >= 0)
            {
                bodyIndex -= 1;   
                SetBodyIndex(bodyIndex);
            }
        }
        
        if (rightAction.WasPressedThisFrame())
        {
            // Move up an index IF that would stay smaller than the list length
            if (bodyIndex + 1 < bodyGameObjects.Count)
            {
                bodyIndex += 1;   
                SetBodyIndex(bodyIndex); 
            }            
        }
    }

    public void SetBodyIndex(int newIndex)
    {
        // Remember which index we're setting to
        bodyIndex = newIndex;

        // Turn the sprites on and off
        for (int i = 0; i < bodyGameObjects.Count; i++)
        {
            // Is this the game object we want to turn on?
            bool isMatch = i == bodyIndex;

            GameObject bodyObject = bodyGameObjects[i];
            bodyObject.SetActive(isMatch);
        }
    }
}
