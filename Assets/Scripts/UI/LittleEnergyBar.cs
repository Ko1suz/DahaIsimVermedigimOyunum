using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LittleEnergyBar : MonoBehaviour
{

    public Image brustRefillTimerFill;
    public Slider slider;   


    public void SetRefillEnergyUI(float energy)
    {
        slider.value = energy;
    }
}
