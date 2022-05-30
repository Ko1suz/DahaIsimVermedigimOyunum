using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySquare : Enemy_AI
{
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
       currnetHealth = maxHealth;
        InvokeMethods();
    }
}
