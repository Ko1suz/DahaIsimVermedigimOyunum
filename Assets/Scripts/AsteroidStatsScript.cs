using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidStatsScript : MonoBehaviour
{
    
    public int asteroidMaxHealth = 50;
     private int _currnetHealth;

    public int currnetHealth
    {
        get{ return _currnetHealth;}
        set{_currnetHealth = Mathf.Clamp(value,0,asteroidMaxHealth);}
    } 

}
