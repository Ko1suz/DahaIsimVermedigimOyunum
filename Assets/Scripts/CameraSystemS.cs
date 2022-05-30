using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystemS : MonoBehaviour
{
   public float moveSpeed;
   public Vector3 offset; 
   public Transform player;

   private float camSize;
   private Camera cam;
   public float maxSize;
   public float minSize;

   static public Transform KameraPozisyonu;

    private void Start() {
        cam = GetComponent<Camera>();
        camSize = cam.orthographicSize;
        KameraPozisyonu = GetComponent<Transform>();
        Invoke("FoundPlayer",1f);
    }
   private void Update() {
       try
       {
           KameraPozisyonu.position = Vector3.Lerp(transform.position,player.transform.position+offset,moveSpeed*Time.deltaTime);
       }
       catch (System.Exception)
       {
           
           Debug.LogWarning("Player ölü ya da yok edildi");
       }
       
       Zoom();
   } 

   void Zoom(){
       
       if (Mathf.Abs(cam.orthographicSize-camSize)>0.1)
       {
            float change = Mathf.Lerp(cam.orthographicSize, camSize,Time.deltaTime*2);
            cam.orthographicSize = change;
       }
      
       if (Input.mouseScrollDelta.y>0 && camSize> minSize)
       {
           camSize--;
       }
       else if (Input.mouseScrollDelta.y<0 && camSize< maxSize)
       {
           camSize++;
       }
   }
    public void FoundPlayer(){
        if (player==null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }
}
