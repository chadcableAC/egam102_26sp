using UnityEngine;

public class ScreenFlowButton : MonoBehaviour
{
    public ScreenFlowManager.Screens screenToActivate;

    public void OnPress()
    {
        // Find ALL screen managers (Returns an array)
        // Use this when we know there's multiple in a scene
        // ScreenFlowManager[] flowManagers = FindObjectsByType<ScreenFlowManager>(FindObjectsSortMode.None);
        
        // Find the FIRST screen manager (Returns a single object)
        // Use this when we know there's only one in the scene
        ScreenFlowManager flowManager = FindFirstObjectByType<ScreenFlowManager>();
        
          FindFirstObjectByType<ScreenFlowManager>();
        if (flowManager)
        {
            flowManager.SetCurrentScreen(screenToActivate);
        }
    }
}
