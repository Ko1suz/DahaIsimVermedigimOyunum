using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Enemy_AI : EnemyStats
{
    public Transform target;
    public float distancee;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float angleOffset = 0f;

    Path path;
    int currnetWaypoint = 0;
    bool reachedEndOfPath = false;

    protected Seeker seeker;
    protected Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeMethods();

    }

    public void InvokeMethods()
    {
        InvokeRepeating("UpdatePath", 0f, .5f);
        InvokeRepeating("FindTarget", 0f, 1f);
    }

    public void FindTarget(){
        if (target = null)
        {
           return;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        
    } 

    void Update()
    {
        FaceDirection();
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

    public void FixedUpdate()
    {
        Movment();
    }

    public void Movment()
    {
        if (path == null)
        {
            return;
        }
        if (currnetWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        //Düşmana belli bir mesafe yaklaşırsa haraket yönünü tersine çeviriyor
        if (Vector2.Distance(transform.position, target.position) < distancee)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currnetWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            rb.AddForce(-force);
            // FaceDirection(direction);
        }
        else
        {
            Vector2 direction = ((Vector2)path.vectorPath[currnetWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;
            rb.AddForce(force);
            // FaceDirection(direction);
        }
        // rb.velocity  = direction * speed * Time.deltaTime;



        float distance = Vector2.Distance(rb.position, path.vectorPath[currnetWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currnetWaypoint++;
        }
    }

    // private void FaceDirection(Vector2 direction)
    // {
    //     float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg*Time.frameCount;
    //     rb.rotation = angle;
    // }

    public void UpdatePath()
    {
        // if (Vector2.Distance(transform.position,target.position)< distancee)
        // {
        //     // return;
        // }
        if (seeker.IsDone())
        {
            if (target == null)
            {
                Debug.LogWarning("DOSTUM SAKİN OL AMA HEDEF YOK");
            }
            else
            {
                seeker.StartPath(rb.position, target.position, OnPathComplate);
            }   
        }
        
    }


    public void OnPathComplate(Path p)
    {
        if (!p.error)
        {
            path = p;
            currnetWaypoint = 0;
        }
    }
}
