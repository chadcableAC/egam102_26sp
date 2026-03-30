using UnityEngine;
using UnityEngine.UI;

public class HpSliderUi : MonoBehaviour
{
    public Slider slider;

    public int hp = 0;
    public int maxHp = 10;

    void Update()
    {
        // We can set this in the inspector as well
        slider.maxValue = maxHp;
        slider.value = hp;
    }
}
