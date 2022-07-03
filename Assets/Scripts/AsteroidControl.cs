using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour
{
    private Asteroid asteroid;
    // Start is called before the first frame update
    void Start()
    {
        asteroid = GetComponent<Asteroid>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Control()
    {
        if (asteroid.currnetHealth<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
