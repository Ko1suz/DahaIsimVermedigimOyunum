using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 500.0f;
    public float maxLifeTime = 5f;
    public int bulletAttackDamage = 10;
    private Rigidbody2D rb;
    EnemyStats enemyStats;
    Asteroid asteroid;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
       
    }
    public void Project(Vector2 direction){
        rb.AddForce(direction* this.speed);
        Destroy(this.gameObject,maxLifeTime);
    }
    

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag== "Asteroid")
        {
            asteroid = other.gameObject.GetComponent<Asteroid>();
            asteroid.SetAsteroidHealth(bulletAttackDamage);
        }
        // GameManager.BulletCollisonPointEffect(other.contacts[0].point);
        // Destroy(this.gameObject);
        if (other.gameObject.tag == "Enemy")
        {
            enemyStats = other.gameObject.GetComponent<EnemyStats>();
            enemyStats.SetEnemyHealth(bulletAttackDamage);
        }
        GameManager.BulletCollisonPointEffect(other.contacts[0].point);
        Destroy(this.gameObject);
    }

   

    
}
