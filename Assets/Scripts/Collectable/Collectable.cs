using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3;
    protected GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine(SpeedIncrease());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move(){
        transform.position = Vector2.MoveTowards(transform.position,target.position,moveSpeed*Time.deltaTime);
    }

    IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(.5f);
        moveSpeed *=1.2f;
        StartCoroutine(SpeedIncrease());
    }
}
