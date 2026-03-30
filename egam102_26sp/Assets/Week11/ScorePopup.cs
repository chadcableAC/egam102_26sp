using System.Collections;
using TMPro;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    public Transform moveHandle;

    public TextMeshPro scoreText;

    public float moveAmount;
    public float moveDuration; 

    IEnumerator Start()
    {
        // Wait x seconds
        yield return new WaitForSeconds(moveDuration);

        // Destroy this game object
        Destroy(gameObject);
    }

    public void SetScore(int score)
    {
        // Create the string "+100", etc
        scoreText.text = "+" + score;
    }

    void Update()
    {
        // How much to move THIS frame
        Vector2 moveThisFrame = Vector2.up * moveAmount * Time.deltaTime;
        moveHandle.position += (Vector3) moveThisFrame;
    }
}
