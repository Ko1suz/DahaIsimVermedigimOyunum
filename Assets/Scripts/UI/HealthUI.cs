using UnityEngine.UI;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    // public Text healthText;
    public Slider slider;
    public Text healthText;
    public Gradient gradient;
    public Image fill;
    

    void Start()
    {
       
    }

    void Update()
    {
        healthText.text =PlayerStatsScript.instance.currnetHealth.ToString();
        healthText.color = gradient.Evaluate(slider.normalizedValue); 
    }

    public void SetMaxHealthUI(int health)
    {
        slider.maxValue = health;
        slider.value =health;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealthUI(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
