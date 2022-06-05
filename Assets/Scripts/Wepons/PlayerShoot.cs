using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public AmmoUI ammoUI;
    public Bullet MiniGunBulletPrefab;
    public Bullet normalBullet;
    public Bullet SniperBullet;
    public Transform[] firePoint;
    private enum ChosenWepon { Normal, MiniGun, Sniper };
    private ChosenWepon chosenWepon = ChosenWepon.Normal;


    public float fireRate;
    public float miniGunFireRate;
    public float sniperFireRate;
    public float nextFire;


    // Start is called before the first frame update
    void Start()
    {
        PlayerStatsScript.instance.currnetAmmo = PlayerStatsScript.instance.maxAmmo;
        ammoUI.SetMaxAmmohUI(PlayerStatsScript.instance.maxAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        WeponChose();
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }


    public void WeponChose()
    {
        if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Alpha1))
        {
            chosenWepon = ChosenWepon.Normal;
        }
        else if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Alpha2))
        {
            chosenWepon = ChosenWepon.MiniGun;
        }
        else if (Input.GetKey(KeyCode.Keypad3) || Input.GetKey(KeyCode.Alpha3))
        {
            chosenWepon = ChosenWepon.Sniper;
        }
    }

    public void Shoot()
    {
        if (Time.time > nextFire && chosenWepon == ChosenWepon.Normal)
        {
            nextFire = Time.time + PlayerStatsScript.instance.fireRate;
            Bullet bullet = Instantiate(this.normalBullet, firePoint[0].transform.position, this.transform.rotation);
            bullet.Project(this.gameObject.transform.up);
        }
        else if (Time.time > nextFire && chosenWepon == ChosenWepon.MiniGun)
        {
            nextFire = Time.time + PlayerStatsScript.instance.miniGunFireRate;
            StartCoroutine(MiniGun());
           
        }
        else if (Time.time > nextFire && chosenWepon == ChosenWepon.Sniper)
        {
            nextFire = Time.time + PlayerStatsScript.instance.sniperFireRate;
            Bullet bullet = Instantiate(this.SniperBullet, firePoint[0].transform.position, this.transform.rotation);
            bullet.Project(this.gameObject.transform.up);
        }
    }

    IEnumerator MiniGun()
    {
        Bullet bullet = Instantiate(this.MiniGunBulletPrefab, firePoint[0].transform.position, this.transform.rotation);
        bullet.Project(this.gameObject.transform.up);
        PlayerStatsScript.instance.SetPlayerAmmo(-50*Time.fixedDeltaTime);
        yield return new WaitForSeconds(0.1f);
        Bullet bullet1 = Instantiate(this.MiniGunBulletPrefab, firePoint[1].transform.position, this.transform.rotation);
        bullet1.Project(this.gameObject.transform.up);
        PlayerStatsScript.instance.SetPlayerAmmo(-50*Time.fixedDeltaTime);
        yield return new WaitForSeconds(0.1f);
        Bullet bullet2 = Instantiate(this.MiniGunBulletPrefab, firePoint[2].transform.position, this.transform.rotation);
        bullet2.Project(this.gameObject.transform.up);
        PlayerStatsScript.instance.SetPlayerAmmo(-50*Time.fixedDeltaTime);
    }


}
