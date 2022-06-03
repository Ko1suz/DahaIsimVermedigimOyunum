using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    public static PlayerStatsScript instance;
    public float thrustSpeed = 1f; 
    public float turnSpeed = 1f;
    public int maxHealth = 100;
    public float maxEnergy = 150;
    public int attackDamage = 10;
    public int DashAttackDamage = 100;
    private int _currnetHealth;
    private float _currnetEnergy;
    public EnergyUI energyUI;
    

    void Start()
    {
        
    }
    public int currnetHealth
    {
        get{ return _currnetHealth;}
        set{_currnetHealth = Mathf.Clamp(value,0,maxHealth);}
    }

    public float currnetEnergy
    {
        get { return _currnetEnergy; }
        set { _currnetEnergy = Mathf.Clamp(value, 0, maxEnergy); }
    }

    private void Awake() 
        {
            if (instance==null)
            {
                instance = this;
            }
            //  currnetHealth =maxHealth;
        }

    public void SetPlayerEnergy(float value)
    {
        PlayerStatsScript.instance.currnetEnergy += value;
        energyUI.SetEnergyUI(PlayerStatsScript.instance.currnetEnergy);
        //Debug.LogWarning(value + " Kadar enerji kaybettin");

    }

    

   



}
