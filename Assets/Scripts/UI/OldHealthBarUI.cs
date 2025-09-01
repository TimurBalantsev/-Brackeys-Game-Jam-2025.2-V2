using System;
using UnityEngine;
using UnityEngine.UI;

public class OldHealthBarUI : MonoBehaviour
{
    [Serializable]
    public class HealthBarPercentToColor
    {
        [Range(0, 1)] public float percent;
        public Color color = Color.black;
    }
    
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    //please keep this array sorted in descending order of percent
    [SerializeField] private HealthBarPercentToColor[] percentToColors;
    
    public void SetHealth(float currentHealth, float maxHealth)
    {
        if (slider == null) return;
        
        slider.value = Math.Clamp(currentHealth / maxHealth, 0, 1);
        SetFillColor();
    }

    private void SetFillColor()
    {
        for (int i = percentToColors.Length-1; i > 0; i--)
        {
            if (slider.value <= percentToColors[i].percent)
            {
                fill.color = percentToColors[i].color;
                return;
            }
        }
    }
}
