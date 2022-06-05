using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponStatsScript : MonoBehaviour
{
    public static WeponStatsScript instance;
    public Bullet MiniGunBulletPrefab;
    public Bullet normalBullet;
    public Bullet SniperBullet;

    public float fireRate = .5f;
    public float miniGunFireRate = .19f;
    public float sniperFireRate = 2;
    // public float nextFire;
    public float nextFire;


    public float maxAmmo = 100;
    private float _currnetAmmo;

    public float currnetAmmo
    {
        get { return _currnetAmmo; }
        set { _currnetAmmo = Mathf.Clamp(value, 0, maxAmmo); }
    }

    void SetPlayerAmmo(float value)
    {
        WeponStatsScript.instance.currnetAmmo += value * Time.deltaTime;
        //TODO Ammo UI yapacaksın
    }
}
