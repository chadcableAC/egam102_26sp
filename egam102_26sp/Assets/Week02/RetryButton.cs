using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void Retry()
    {
        // Get the current scene
        Scene activeScene = SceneManager.GetActiveScene();

        // Reload this scene
        SceneManager.LoadScene(activeScene.buildIndex);
    }
}
