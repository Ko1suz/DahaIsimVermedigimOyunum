using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyTriAngle : Enemy_AI
{
    public EnemyBullet bulletPrefab;
    public Transform firePoint;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        currnetHealth = maxHealth;
        InvokeMethods();
        InvokeRepeating("Shoot",0f,1f);
    }
    public void Shoot(){
        EnemyBullet bullet = Instantiate(this.bulletPrefab,firePoint.transform.position,this.transform.rotation);
        bullet.Project(-this.gameObject.transform.right);
    }
}
