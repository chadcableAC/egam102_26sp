using UnityEngine;

public class ShakeRoutine : MonoBehaviour
{
    // The object we'll shake
    public Transform shakeHandle = null;

    // How strong to shake (Ho wmuch to move the object)
    public float shakeStrength = 1f;

    // How long to shake
    public float shakeDuration = 1f;

    Vector3 originalPosition;
    float shakeTimer;

    void Update()
    {
        // If the timer is greater than zero
        if (shakeTimer > 0)
        {
            // Decrement the timer
            shakeTimer -= Time.deltaTime;

            // If the timer falls below zero, stop shaking
            if (shakeTimer <= 0)
            {
                shakeHandle.localPosition = originalPosition;
            }
            // Otherwise shake the object
            else
            {
                // Randomize an offset
                Vector3 offset = Vector3.zero;

                offset.x = Random.Range(-shakeStrength, shakeStrength);
                offset.y = Random.Range(-shakeStrength, shakeStrength);

                // Apply this offset to the transform
                shakeHandle.localPosition = originalPosition + offset;
            }
        }
    }

    public void Shake()
    {
        // Remember where the object is
        originalPosition = shakeHandle.localPosition;

        // Start the timer
        shakeTimer = shakeDuration;
    }
}
