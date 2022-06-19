using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoReloadUI : MonoBehaviour
{
    public Image ammoReloadTimerFill;
    public Slider slider;


    public void SetAmmoReloadUI(float ammo)
    {
        slider.value = ammo;
    }
}
