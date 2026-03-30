using UnityEngine;
using UnityEngine.UI;

public class LowHpUi : MonoBehaviour
{
    public Transform scaleHandle;
    public Image scaleImage;

    public float highHpScale = 10;  // Scale when HP is maxed out
    public float lowHpScale = 1;    // Scale when HP in 0 / smallest

    public float highHpAlpha = 0;  // Alpha when HP is maxed out
    public float lowHpAlpha = 1;   // Alpha when HP in 0 / smallest

    public float hp = 10;
    public float maxHp = 10;

    void Update()
    {
        // Turn HP into an interp (between 0 and 1)
        float interp = hp / maxHp;

        // Then we'll lerp between the low and high scales
        float scale = Mathf.Lerp(lowHpScale, highHpScale, interp);
        float alpha = Mathf.Lerp(lowHpAlpha, highHpAlpha, interp);

        // Finally apply the scale value
        scaleHandle.localScale = Vector3.one * scale;
        
        // Update the alpha too
        Color color = scaleImage.color;
        color.a = alpha;
        scaleImage.color = color;
    }
}
