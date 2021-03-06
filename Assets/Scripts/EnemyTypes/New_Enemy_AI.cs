using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Enemy_AI : EnemyStats
{
    public Transform target;
    public Rigidbody2D rb;
    public float angleOffset = 0f;
    public float distanceOffset = 0f;

    public float backOffFuckerPower = 2;

    private void Awake() {
        target = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody2D>();
        
    }


    protected void Update() {
        PlayerDistance();
        FaceDirection();
        
    }

    private void FixedUpdate() {
        Movment();
    }

    public void FaceDirection()
    {
        if (target == null)
        {
            Debug.LogWarning("Sakin ol ");
        }else
        {
            Vector2 faceDirection = target.position - transform.position;
            float angle = Mathf.Atan2(faceDirection.x, faceDirection.y) * Mathf.Rad2Deg;
            rb.rotation = -angle+angleOffset;
        }
       
    }

    public void Movment()
    {
        if (PlayerDistance() < distanceOffset)
        {
            rb.AddForce(this.gameObject.transform.up*-Speed/2);
        }
        //if Character Is FarAway I Accelerate
        else if (PlayerDistance() >50)
        {
            rb.AddForce(this.gameObject.transform.up*50); 
        }
        else
        {
            rb.AddForce(this.gameObject.transform.up*Speed);
        }
    }

    protected void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            rb.velocity = this.gameObject.transform.up * -1 * backOffFuckerPower;
        }
    }

    protected float PlayerDistance()
    {
        float playerDistance = Vector2.Distance(transform.position, target.position);
        return playerDistance;
    }

    
}
