using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySquare : New_Enemy_AI
{
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currnetHealth = maxHealth;
    }



}
