using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : Collectable
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            PlayerStatsScript.instance.currnetEnergy +=50;
            gm.CollectableEnergy(this);
        }
    }
}
