using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wepon : MonoBehaviour
{

    public static Wepon instance;
    float timeToFire =0;
    float fireRate = 5f;
    public Player player;
    


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Fire()
    {
        if (Input.GetMouseButton(0) && Time.time > timeToFire)
        {
            timeToFire = Time.time + 1/fireRate;
            Shoot();
        }
    }

    public void Shoot()
    {
        Bullet bullet = Instantiate(player.bulletPrefab, player.firePoint.transform.position, player.transform.rotation);
        bullet.Project(this.gameObject.transform.up);
    }
}
