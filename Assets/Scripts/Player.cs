using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool thrusting; //itme demek 
    private bool boosting;
    private float turnDireaction;
    private Rigidbody2D rb;
    private PolygonCollider2D pcoll2D;
    private PlayerStatsScript playerStats;


    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public bool dashStatus = false;


   

    // public float thrustSpeed = 1f; 
    // public float turnSpeed = 1f;
    // public Transform kıc;
    public Bullet  bulletPrefab;
    public Transform firePoint;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pcoll2D = GetComponent<PolygonCollider2D>();
        playerStats = PlayerStatsScript.instance;
    }
    void Start()
    {
        PlayerStatsScript.instance.currnetHealth = PlayerStatsScript.instance.maxHealth;
        PlayerStatsScript.instance.currnetEnergy = PlayerStatsScript.instance.maxEnergy;
        playerStats = PlayerStatsScript.instance;

        dashTime = startDashTime;
    }
    void Update()
    {
        
        Vector3 realDirection = firePoint.position - transform.position;

        if (direction ==0)
        {
            if (PlayerStatsScript.instance.currnetEnergy >0)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Time.timeScale = 0.2f;
                    Time.fixedDeltaTime = 0.02f * Time.timeScale;
                   
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    dashStatus = true;
                    Time.timeScale = 1f;
                    Time.fixedDeltaTime = 0.02f * Time.timeScale;
                    PlayerStatsScript.instance.SetPlayerEnergy(-25);
                    direction = 1;
                    dashStatus = true;
                    pcoll2D.isTrigger = true;
                }
                else
                {
                    FaceMouse();
                    dashStatus = false;
                    pcoll2D.isTrigger = false;
                }

            }
            else
            {
                Debug.LogWarning("ENERJİN KALMADI");
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                FaceMouse();
            }

        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
                if (direction==1)
                {
                    rb.velocity = realDirection * dashSpeed*Time.fixedDeltaTime*100;
                }
            }

        }



        thrusting = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));
        boosting = Input.GetKey(KeyCode.LeftShift);
        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        //{
        //    turnDireaction = 1f;
           
        //}
        //else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        //{
        //    turnDireaction = -1f;
           
        //}
        //else
        //{
        //    turnDireaction = 0;
        //}

        if (Input.GetButtonDown("Fire1")||Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }


        

    }

    private void FaceMouse()
    {
        Vector3 mousePositon = Input.mousePosition;
        mousePositon = Camera.main.ScreenToWorldPoint(mousePositon);

        Vector2 direction = new Vector2(mousePositon.x - transform.position.x, mousePositon.y - transform.position.y);

        transform.up = direction;
    }

    void FixedUpdate()
    {
        if (boosting)
        {
            rb.AddForce(this.gameObject.transform.up * playerStats.thrustSpeed * 5);
        }
        else if (thrusting)
        {    
            rb.AddForce(this.gameObject.transform.up* playerStats.thrustSpeed);
        }
        
        //if (turnDireaction != 0f)
        //{
        //    rb.AddTorque(turnDireaction*playerStats.turnSpeed);
        //}
    }
    public void Shoot(){
        Bullet bullet = Instantiate(this.bulletPrefab,firePoint.transform.position,this.transform.rotation);
        bullet.Project(this.gameObject.transform.up);
    }
    
    public void SetPlayerHealth(int damage)
    {
        PlayerStatsScript.instance.currnetHealth -= damage;
        Debug.LogWarning(damage+" Kadar hasar yedin");

        if (PlayerStatsScript.instance.currnetHealth<=0)
        {
            GameManager.KillPlayer(this);
        }
    }

   

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="Bullet")
        {
            SetPlayerHealth(EnemyStats.bulletAttackDamage);
        }
        else if (other.gameObject.tag=="Enemy")
        {
            SetPlayerHealth(other.gameObject.GetComponent<EnemyStats>().attackDamage);
        }
    }

}