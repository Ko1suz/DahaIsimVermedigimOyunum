using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    public static PlayerStatsScript instance;
    public float burstRestoreTime = 10;
    public float thrustSpeed = 1f;
    public float turnSpeed = 1f;
    public int maxHealth = 100;
    public float maxEnergy = 150;
    public int attackDamage = 10;
    public int DashAttackDamage = 100;
    private int _currnetHealth;
    private float _currnetEnergy;
    public EnergyUI energyUI;
    public AmmoUI ammoUI;
    public float brustRefillTimer;



    //Wepons
    public float fireRate = .5f;
    public float miniGunFireRate = .19f;
    public float sniperFireRate = 2;
    public float maxAmmo = 100;
    private float _currnetAmmo;


    void Start()
    {
        PlayerStatsScript.instance.brustRefillTimer = PlayerStatsScript.instance.burstRestoreTime;
    }
    public int currnetHealth
    {
        get { return _currnetHealth; }
        set { _currnetHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    public float currnetEnergy
    {
        get { return _currnetEnergy; }
        set { _currnetEnergy = Mathf.Clamp(value, 0, maxEnergy); }
    }

    public float currnetAmmo
    {
        get { return _currnetAmmo; }
        set { _currnetAmmo = Mathf.Clamp(value, 0, maxAmmo); }
    }

    private void Awake()
    {
        if (instance == null)
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

    public void SetPlayerAmmo(float value)
    {
        PlayerStatsScript.instance.currnetAmmo += value;
        ammoUI.SetAmmoUI(PlayerStatsScript.instance.currnetAmmo);
        //TODO Ammo UI yapacaksın
    }







}
