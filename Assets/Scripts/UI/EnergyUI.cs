using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    public Slider slider;
    public Text energyText;
    public Gradient gradient;
    public Image fill;
    

    void Start()
    {
       
    }

    void Update()
    {
        energyText.text =((int)(PlayerStatsScript.instance.currnetEnergy)).ToString();
        energyText.color = gradient.Evaluate(slider.normalizedValue); 
    }

    public void SetMaxEnergyhUI(float energy)
    {
        slider.maxValue = energy;
        slider.value =energy;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetEnergyUI(float energy)
    {
        slider.value = energy;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
