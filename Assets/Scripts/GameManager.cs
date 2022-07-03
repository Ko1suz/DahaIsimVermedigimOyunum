using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public ParticleSystem explosion;
    public ParticleSystem CollisionExplosion;
    public ParticleSystem bulletExplosion;
    public ParticleSystem enemyBulletExplosion;
    public ParticleSystem DamageExplosion;
    public ParticleSystem AsteroidExplosion;
    public ParticleSystem CollectableEffect;
    public Transform PlayerPrefab;
    public Transform ReSpawnPoint;
    public int RespawnBackCount = 3;
    public int score;
    public int scoreIncreas = 100;
    public static GameObject gameOverUI;
    public GameObject gameOverUIRef;
    public static GameObject scoreUI;
    public GameObject scoreUIRef;
    public List<GameObject> asteroids = new List<GameObject>();


    void Awake()
    {
        gameOverUI = gameOverUIRef;
        scoreUI = scoreUIRef;
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }

    }

    public IEnumerator ReSpawnPlayer()
    {

        yield return new WaitForSeconds(RespawnBackCount);

        Instantiate(PlayerPrefab, ReSpawnPoint.position, ReSpawnPoint.rotation);

    }

    void AsteroidCheck()
    {

        if (/*asteroids == null &&*/ asteroids.Count > 0)
        {

            for (int i = 0; i < asteroids.Count; i++)
            {
                Asteroid asteroid = asteroids[i].GetComponent<Asteroid>();
                
                if (asteroid.GetComponent<PolygonCollider2D>().enabled == false || asteroid.GetComponent<Asteroid>().enabled == false)
                {
                    asteroid.GetComponent<PolygonCollider2D>().enabled = true;
                    asteroid.GetComponent<Asteroid>().enabled = true;
                    Destroy(asteroid.gameObject);
                }
                if (asteroid.currnetHealth <= 0)
                {
                    asteroid.GetComponent<Asteroid>().enabled = true;

                    Destroy(asteroid.gameObject);
                    asteroids.Remove(asteroids[i]);
                }

            }
        }

    }
    public void DestroyAsteroid(Asteroid asteroid)
    {
        AsteroidExplosionEffect(asteroid.transform, asteroid.size);
        asteroids.Remove(asteroid.gameObject);
        Destroy(asteroid.gameObject);
    }

    private void Update()
    {
        AsteroidCheck();
    }

    public static void KillPlayer(Player player)
    {
        gameOverUI.SetActive(true);
        scoreUI.SetActive(false);
        gm.explosion.transform.position = player.transform.position;
        ExplosionEffect(player.transform);
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.ReSpawnPlayer());
        gm.score = 0;
    }
    public static void KillEnemy(EnemyStats enemy)
    {
        ExplosionEffect(enemy.transform);
        Destroy(enemy.gameObject);
        if (Player.dashStatus)
        {
            PlayerStatsScript.instance.SetPlayerEnergy(25);
        }
    }


    public void CollectableHealth(Collectable collectableHealth)
    {
        gm.CollectableEffect.startColor = Color.green;
        gm.CollectableEffect.transform.position = collectableHealth.transform.position;
        gm.CollectableEffect.Play();
        Destroy(collectableHealth.gameObject);
    }

    public void CollectableEnergy(Collectable collectableEnergy)
    {
        gm.CollectableEffect.startColor = Color.cyan;
        gm.CollectableEffect.transform.position = collectableEnergy.transform.position;
        gm.CollectableEffect.Play();
        Destroy(collectableEnergy.gameObject);
    }

    public static void ExplosionEffect(Transform objectTransform)
    {
        gm.explosion.transform.position = objectTransform.transform.position;
        gm.explosion.Play();
    }
    public static void AsteroidExplosionEffect(Transform objectTransform, float size)
    {
        gm.AsteroidExplosion.startSize = size / 5;
        gm.AsteroidExplosion.transform.position = objectTransform.transform.position;
        gm.AsteroidExplosion.Play();
    }
    public static void DamageExplosionEffect(Transform objectTransform, float size)
    {
        gm.DamageExplosion.startSize = size / 5;
        gm.DamageExplosion.transform.position = objectTransform.transform.position;
        gm.DamageExplosion.Play();
    }
    public static void BulletExplosionEffect(Transform objectTransform)
    {
        gm.bulletExplosion.transform.position = objectTransform.transform.position;
        gm.bulletExplosion.Play();
    }
    public static void CollisonPointEffect(Vector2 CarpısmaNoktası)
    {
        gm.CollisionExplosion.transform.position = CarpısmaNoktası;
        gm.CollisionExplosion.Play();
        // gm.bulletExplosion.transform.position = CarpısmaNoktası;
        // gm.bulletExplosion.Play();
    }
    public static void BulletCollisonPointEffect(Vector2 CarpısmaNoktası)
    {
        if (PlayerShoot.isSniper)
        {
            gm.bulletExplosion.startSize = 1f;
            gm.bulletExplosion.startSpeed = 3.5f;
            gm.bulletExplosion.transform.position = CarpısmaNoktası;
            gm.bulletExplosion.Play();
        }
        else
        {
            gm.bulletExplosion.startSize = 0.1f;
            gm.bulletExplosion.startSpeed = 1.5f;
            gm.bulletExplosion.transform.position = CarpısmaNoktası;
            gm.bulletExplosion.Play();
        }

    }
    public static void EnemyCollisonPointEffect(Vector2 CarpısmaNoktası)
    {
        gm.enemyBulletExplosion.transform.position = CarpısmaNoktası;
        gm.enemyBulletExplosion.Play();
    }
    public void EndGame()
    {
        gameOverUI.SetActive(true);
    }


}
