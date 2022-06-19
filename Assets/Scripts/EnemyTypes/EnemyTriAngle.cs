using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyTriAngle : New_Enemy_AI
{
    public EnemyBullet bulletPrefab;
    public Transform firePoint;
    float targetDistance;
    public float fireDistance =15;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currnetHealth = maxHealth;
        InvokeRepeating("Shoot", 0f, 1f);
    }
    public void Shoot()
    {
        if (targetDistance < fireDistance)
        {
            EnemyBullet bullet = Instantiate(this.bulletPrefab, firePoint.transform.position, this.transform.rotation);
            bullet.Project(this.gameObject.transform.up);
        }

    }
    private new void Update()
    {
        targetDistance = Vector3.Distance(this.gameObject.transform.position, target.gameObject.transform.position); 
        FaceDirection();
        Movment();  
    }
}
