using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    
    public Sprite[] sprites;
    public Sprite[] DamagedAsteroid;
    public Sprite[] DamagedAsteroid2;
    public int asteroidMaxHealth = 50;
    private int _currnetHealth;

    public int currnetHealth
    {
        get { return _currnetHealth; }
        set { _currnetHealth = Mathf.Clamp(value, 0, asteroidMaxHealth); }
    }
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 2.5f;
    public float minSpeed = 10f;
    public float maxSpeed = 60f;
    public float maxLifeTime = 60.0f;
    public float halfAsteroidSpeed = 5.0f;
    public int asteroidDamage = 1;
    public int asteroiDamagaReduce = 2;
    public ParticleSystem ExplosionEffect;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    Player player;
    public GameManager gm;
    public Asteroid prefabAsteroid;
    Asteroid asteroid;
    private int RandomSpriteDegeri;
    public Transform playerTransform;
    float distance;
    int secim;
    int olasılık;
    [SerializeField] private GameObject[] Collectable = new GameObject[2];
    // private 


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        asteroid = this;

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _currnetHealth = asteroidMaxHealth;
        int spriteTutucu = Random.Range(0, 4);
        RandomSpriteDegeri = spriteTutucu;
        spriteRenderer.sprite = sprites[RandomSpriteDegeri]; //Objenin Random Sprite/Resim ile oluşmasını sağlıyor
        this.transform.eulerAngles = new Vector3(0f, 0f, (int)Random.value * 360f);
        this.transform.localScale = Vector3.one * this.size; //Altındaki ile aynı işevi yapan kod bu da farklı yazımı

        // new Vector3(this.size,this.size,this.size);

        rb.mass = this.size * 5;

        secim = Random.Range(0, 2);
        olasılık = Random.Range(0, 5);
    }

    void Update()
    {
        distance = Vector3.Distance(this.gameObject.transform.position, playerTransform.gameObject.transform.position);
        if (distance > 240)
        {
            Destroy(this.gameObject);
            gm.asteroids.Remove(this.gameObject);
        }
    }

    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * AsteroidSpeed());
        Destroy(this.gameObject, this.maxLifeTime);
        //gm.asteroids.Remove(this.gameObject);
        
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.gameObject.tag == "Bullet")
        // {

        //    SetAsteroidHealth(PlayerStatsScript.instance.attackDamage);

        // }
        if (other.gameObject.tag == "Player")
        {
            print("Points colliding: " + other.contacts.Length);
            print("First point that collided: " + other.contacts[0].point);
            player = other.gameObject.GetComponent<Player>(); ;
            player.SetPlayerHealth(AsteroidDamage());

            // GameManager.ExplosionEffect(other.transform);

            GameManager.CollisonPointEffect(other.contacts[0].point);
        }
        else if (other.gameObject.tag == "AsteroidBounder")
        {
            gm.DestroyAsteroid(this);
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         if (Player.dashStatus)
    //         {
    //             SetAsteroidHealth(PlayerStatsScript.instance.DashAttackDamage);
    //             PlayerStatsScript.instance.SetPlayerEnergy(5);
    //         }
    //     }
    // }

    public void AsteroidSplit()
    {
        // if (asteroid.size < .75)
        // {
        //     gm.score += gm.scoreIncreas;
        // }
        // else if (asteroid.size < 1)
        // {
        //     gm.score += gm.scoreIncreas * 2;
        // }
        // else
        // {
        //     gm.score += gm.scoreIncreas * 3;
        // }
        asteroidMaxHealth = asteroidMaxHealth / 2;
        // ExplosionEffect.Play();
        if (this.size * 0.5f >= this.minSize)
        {
            CreateSplit();
            CreateSplit();
            gm.DestroyAsteroid(this);
        }
        else
        {
            if (olasılık == 0)
            {
                Instantiate(Collectable[secim], transform.position, transform.rotation);
            }
            gm.DestroyAsteroid(this);

        }


    }

    public void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;
        Asteroid halfAsteroid = Instantiate(prefabAsteroid, position, this.transform.rotation);
        gm.asteroids.Add(halfAsteroid.gameObject);
        halfAsteroid.size = this.size * 0.5f;
        halfAsteroid.SetTrajectory(Random.insideUnitCircle.normalized * halfAsteroidSpeed);
    }

    public float AsteroidSpeed()
    {
        return Random.Range(minSpeed, maxSpeed);
    }
    public int AsteroidDamage()
    {
        return asteroidDamage * (int)(AsteroidSpeed() / asteroiDamagaReduce);
    }
    bool anahtar = true;
    bool anahtar2 = true;
    public void SetAsteroidHealth(int damage)
    {

        currnetHealth -= damage;
        if (currnetHealth <= asteroidMaxHealth * 0.7f && currnetHealth > asteroidMaxHealth * 0.4f)
        {

            spriteRenderer.sprite = DamagedAsteroid[RandomSpriteDegeri];
            if (anahtar)
            {
                GameManager.DamageExplosionEffect(this.transform, this.size);
                anahtar = false;
            }
        }
        if (currnetHealth <= asteroidMaxHealth * 0.4f)
        {

            spriteRenderer.sprite = DamagedAsteroid2[RandomSpriteDegeri];
            if (anahtar2)
            {
                GameManager.DamageExplosionEffect(this.transform, this.size);
                anahtar2 = false;
            }
        }
        if (currnetHealth <= 0)
        {
            AsteroidSplit();
        }
    }

}
