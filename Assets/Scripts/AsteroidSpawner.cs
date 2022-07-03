using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public  Asteroid asteroidPrefab;
    public float trajectoryVariance = 15.0f;
    public float spawnRate = 2.0f;
    public float spawnAmount = 1.0f;
    public float spawnDistanceRadius = 15.0f;
    private Transform spawnerTransform;
    public GameManager gm;
   

    // Start is called before the first frame update
    void Start()
    {
        spawnerTransform = GetComponent<Transform>();
        InvokeRepeating(nameof(SpawnAstreoid),this.spawnRate,this.spawnRate/6);
    }


    private void SpawnAstreoid()
    {
       
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistanceRadius;
        Vector3 spawnPoint =  this.transform.position + spawnDirection;

        float variance =  Random.Range(-trajectoryVariance,trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance,Vector3.forward); 

        for (int i = 0; i < this.spawnAmount; i++)
        {
            Asteroid asteroid = Instantiate(this.asteroidPrefab,spawnPoint,rotation);
            
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
            gm.asteroids.Add(asteroid.gameObject);
        }
    }
    private void Update() {
    //    spawnerTransform.transform.position = Camera.main.transform.position;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Vector3.zero,spawnDistanceRadius);
    }
   

    
}
