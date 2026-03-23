using UnityEngine;
using UnityEngine.InputSystem;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource source;

    void Update()
    {
        var mouse = Mouse.current;
        if (mouse != null)
        {
            if (mouse.leftButton.wasPressedThisFrame)
            {
                source.Play();
            }
        }
    }
}
