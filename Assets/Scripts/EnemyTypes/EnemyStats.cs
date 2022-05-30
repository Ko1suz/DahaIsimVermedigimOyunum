using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float Speed = 1f; 
    public int maxHealth = 100;
    public int attackDamage = 10;
    public static int bulletAttackDamage = 15;
    private int _currnetHealth;

    public int currnetHealth
    {
        get{ return _currnetHealth;}
        set{_currnetHealth = Mathf.Clamp(value,0,maxHealth);}
    } 

    private void Awake() 
    {
        
            
    }
    void Start()
    {
        
    }

    public void SetEnemyHealth(int damage){
        currnetHealth -= damage;
        if (currnetHealth<=0)
        {
            GameManager.KillEnemy(this);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            SetEnemyHealth(PlayerStatsScript.instance.attackDamage);
        }
       
        Debug.Log(currnetHealth+" Canım");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>().dashStatus)
            {
                SetEnemyHealth(PlayerStatsScript.instance.DashAttackDamage);
            }
        }
    }

}
