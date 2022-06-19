using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Collectable
{   
    public AmmoUI ammoUI;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            PlayerStatsScript.instance.currnetAmmo = PlayerStatsScript.instance.maxAmmo;
            ammoUI.SetAmmoUI(PlayerStatsScript.instance.currnetAmmo);
            Destroy(this.gameObject);
        }
    }
}
