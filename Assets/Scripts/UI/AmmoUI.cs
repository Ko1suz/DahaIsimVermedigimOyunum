using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    public Slider slider;
    public Image ammoFill;




    void Update()
    {

    }



    public void SetMaxAmmohUI(float ammo)
    {
        slider.maxValue = ammo;
        slider.value =ammo;
        
    }
    public void SetAmmoUI(float ammo)
    {
        slider.value = ammo;
    }
}
