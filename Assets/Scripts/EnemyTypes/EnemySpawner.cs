using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public  EnemyTriAngle enemyTriAngle;
    public  EnemySquare enemySquare;
    public float trajectoryVariance = 15.0f;
    public float spawnRate = 2.0f;
    public float spawnAmount = 1.0f;
    public float spawnDistanceRadius = 15.0f;
   

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy),this.spawnRate,this.spawnRate);
    }


    private void SpawnEnemy()
    {
       
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistanceRadius;
        Vector3 spawnPoint =  this.transform.position + spawnDirection;

        float variance =  Random.Range(-trajectoryVariance,trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance,Vector3.forward); 

        for (int i = 0; i < this.spawnAmount; i++)
        {
            EnemyTriAngle enemyTriAngle = Instantiate(this.enemyTriAngle,spawnPoint,rotation);
            EnemySquare enemySquare = Instantiate(this.enemySquare,spawnPoint,rotation);
        }
    }
    private void Update() {
       
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero,spawnDistanceRadius);
    }
}
