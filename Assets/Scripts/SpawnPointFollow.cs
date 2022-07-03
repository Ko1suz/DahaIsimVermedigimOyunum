using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointFollow : MonoBehaviour
{
    private Transform playerTransform;

    private void Awake() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnPointsMove();
    }

    private void FixedUpdate() {
        
    }

    void spawnPointsMove()
    {
        transform.position = playerTransform.position;
    }
}
