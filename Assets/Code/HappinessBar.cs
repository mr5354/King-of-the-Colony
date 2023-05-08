using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappinessBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    
    public void SetMaxHappiness(int happiness)
    {
        slider.maxValue = happiness;
        slider.value = happiness;
        fill.color = gradient.Evaluate(1f);
        
    }
    
    public void SetHappiness(int happiness)
    {
        slider.value = happiness;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    int interval = 1; 
    float nextTime = 0;
     
    // Slowly increased colony hapiness over time
    void Update () {
        if (Time.time >= nextTime) {
            slider.value += 0.2f;
            nextTime += interval; 
        }
    }
}
