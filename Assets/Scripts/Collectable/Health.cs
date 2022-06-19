using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Collectable
{
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            PlayerStatsScript.instance.SetPlayerHealth(50);
            gm.CollectableHealth(this);
        }
    }
}
