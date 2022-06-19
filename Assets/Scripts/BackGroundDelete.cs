using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundDelete : MonoBehaviour
{
    private Collider2D coll2d;

    private void Start() {
        coll2d = GetComponent<Collider2D>();
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "BackGround")
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "BackGround")
        {
            other.gameObject.SetActive(false);
        }
    }

     private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "BackGround")
        {
            other.gameObject.SetActive(true);
        }
    }

    

    
}
