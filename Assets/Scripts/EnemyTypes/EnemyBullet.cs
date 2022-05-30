using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 500.0f;
    public float maxLifeTime = 5f;
    private Rigidbody2D rb;
    GameManager gm;

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

    public virtual  void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag== "Asteroid")
        {
          
        }
        Debug.LogWarning("altsınıf");
        GameManager.EnemyCollisonPointEffect(other.contacts[0].point);
        Destroy(this.gameObject);
    }
}
