using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour
{

    public Vector3 offset;
    // Update is called once per frame
    private Animator anim;
    private bool animStart = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animStart = true;
            anim.SetBool("Tetikle",true);
        }
        // else
        // {
        //     anim.SetBool("Tetikle",false);
        // }

        if (animStart)
        {
            anim.SetBool("Tetikle",true);
        }

        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward - offset);
    }
}
