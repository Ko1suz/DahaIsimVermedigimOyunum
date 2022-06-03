using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class EnergyyUI : MonoBehaviour
{
    public Text energyText;

    void Start()
    {
        energyText = GetComponent<Text>();
    }

    void Update()
    {
        energyText.text = "Energy : " + ((int)(PlayerStatsScript.instance.currnetEnergy)).ToString();
    }

}
