using System;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private bool thrusting; //itme demek 
    private bool boosting;
    private float turnDireaction;
    private Rigidbody2D rb;
    private PolygonCollider2D pcoll2D;
    private PlayerStatsScript playerStats;

    public HealthUI healthUI;
    public EnergyUI energyUI;
    public LittleEnergyBar littleEnergyBar;





    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public static bool dashStatus = false;
    public GameObject arrow;
    public GameObject arrow2;
    public ParticleSystem brustEffect;
    public ParticleSystem classicBrustEffect;







    // public float thrustSpeed = 1f; 
    // public float turnSpeed = 1f;
    // public Transform kıc;
    public Bullet bulletPrefab;
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
        healthUI.SetMaxHealthUI(PlayerStatsScript.instance.maxHealth);
        energyUI.SetMaxEnergyhUI(PlayerStatsScript.instance.maxEnergy);
        // InvokeRepeating("RegenEnergy",1,1);
    }

    void Update()
    {

        Vector3 realDirection = firePoint.position - transform.position;

        if (direction == 0)
        {
            if (PlayerStatsScript.instance.currnetEnergy > 25)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Time.timeScale = 0.00f;
                    Time.fixedDeltaTime = 0.02f * Time.timeScale;

                    if (Input.GetMouseButton(1))
                    {

                        arrow.SetActive(true);
                        arrow2.SetActive(true);
                        if (Input.GetMouseButtonUp(1))
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

                            StartCoroutine(DashTargetingTime());


                        }
                    }



                }
                else if (Input.GetMouseButton(1))
                {
                    FaceMouse();
                    if (Input.GetMouseButtonUp(1))
                    {
                        dashStatus = true;
                        Time.timeScale = 1f;
                        Time.fixedDeltaTime = 0.02f * Time.timeScale;
                        PlayerStatsScript.instance.SetPlayerEnergy(-25);
                        direction = 1;
                        dashStatus = true;
                        pcoll2D.isTrigger = true;
                    }
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
            dashStatus = false;
            pcoll2D.isTrigger = false;

        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = rb.velocity / 10;
            }
            else
            {
                dashTime -= Time.deltaTime;
                if (direction == 1)
                {
                    rb.velocity = realDirection * dashSpeed * Time.fixedDeltaTime * 100;
                }
            }

        }



        IEnumerator DashTargetingTime()
        {
            yield return new WaitForSecondsRealtime(0.7f);
            dashStatus = true;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            PlayerStatsScript.instance.SetPlayerEnergy(-25);
            direction = 1;
            dashStatus = true;
            pcoll2D.isTrigger = true;
            arrow.SetActive(false);
            arrow2.SetActive(false);
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

        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            // Shoot();
        }

    }

    private void FaceMouse()
    {
        Vector3 mousePositon = Input.mousePosition;
        mousePositon = Camera.main.ScreenToWorldPoint(mousePositon);

        Vector2 direction = new Vector2(mousePositon.x - transform.position.x, mousePositon.y - transform.position.y);

        transform.up = direction;
    }
    bool boostWait = false;
    IEnumerator EnergyRestoreTime()
    {
        yield return new WaitForSeconds(PlayerStatsScript.instance.burstRestoreTime);
        boostWait = false;
    }

    public float countDown =10;
    public void brustCountDown()
    {
        
        Debug.LogError("brustCountDown if bloguna gırdım");
        
        countDown += Time.deltaTime * 1;
        Debug.LogWarning("  kalan süre  "+countDown);
        littleEnergyBar.SetRefillEnergyUI(countDown);
        // if (countDown<=0 && boostWait)
        // {
        //     countDown = 10;
        //     littleEnergyBar.SetRefillEnergyUI(countDown);
        // }
    }

    void FixedUpdate()
    {
        if (PlayerStatsScript.instance.currnetEnergy <= 0)
        {
            boostWait = true;
            StartCoroutine(EnergyRestoreTime());
        }
        if (boostWait)
        {
            brustCountDown();
        }
        else
        {
            countDown = 0;
            littleEnergyBar.SetRefillEnergyUI(countDown);
        }
        

        if (boosting)
        {
            if (!boostWait)
            {
                rb.AddForce(this.gameObject.transform.up * playerStats.thrustSpeed * 5);
                // brustEffect.Play();
                classicBrustEffect.startSize = 1.8f;
                PlayerStatsScript.instance.SetPlayerEnergy(-1 * Time.deltaTime * 10);
            }
            else
            {
                rb.AddForce(this.gameObject.transform.up * playerStats.thrustSpeed * 0);
                // brustEffect.Play();
                classicBrustEffect.startSize = .3f;
                PlayerStatsScript.instance.SetPlayerEnergy(+1 * Time.deltaTime);

            }

            // else
            // {
            //     rb.AddForce(this.gameObject.transform.up * playerStats.thrustSpeed * 5);
            //     // brustEffect.Play();
            //     classicBrustEffect.startSize = 1.5f;
            //     PlayerStatsScript.instance.SetPlayerEnergy(-1 * Time.deltaTime * 3);
            // }
        }
        else if (thrusting)
        {
            rb.AddForce(this.gameObject.transform.up * playerStats.thrustSpeed);
            // brustEffect.Stop();
            classicBrustEffect.startSize = 0.8f;
            PlayerStatsScript.instance.SetPlayerEnergy(+1 * Time.deltaTime);
        }
        else
        {
            classicBrustEffect.startSize = 0.8f;
            PlayerStatsScript.instance.SetPlayerEnergy(+1 * Time.deltaTime);
            //    brustEffect.Stop();
        }

        //if (turnDireaction != 0f)
        //{
        //    rb.AddTorque(turnDireaction*playerStats.turnSpeed);
        //}
    }
    // public void Shoot()
    // {
    //     Bullet bullet = Instantiate(this.bulletPrefab, firePoint.transform.position, this.transform.rotation);
    //     bullet.Project(this.gameObject.transform.up);
    // }

    public void SetPlayerHealth(int damage)
    {
        PlayerStatsScript.instance.currnetHealth -= damage;
        healthUI.SetHealthUI(PlayerStatsScript.instance.currnetHealth);

        Debug.LogWarning(damage + " Kadar hasar yedin");

        if (PlayerStatsScript.instance.currnetHealth <= 0)
        {
            GameManager.KillPlayer(this);
        }
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            SetPlayerHealth(EnemyStats.bulletAttackDamage);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            SetPlayerHealth(other.gameObject.GetComponent<EnemyStats>().attackDamage);
        }
    }


    public void RegenEnergy()
    {
        PlayerStatsScript.instance.currnetEnergy += 1;
        //Debug.LogWarning(value + " Kadar enerji kaybettin");
    }
}
