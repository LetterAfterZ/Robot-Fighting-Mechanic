using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Based on Brackeys tutorial https://www.youtube.com/watch?v=BLfNP4Sc_iA

    [SerializeField] private Slider _slider = null;

    public void SetMaxHealth(int health)
    {        
        _slider.maxValue = health;
        _slider.value = health;
    }

    public void SetHealth(int health)
    {
        _slider.value = health;
    }
}
