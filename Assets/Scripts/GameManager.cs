using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public  ParticleSystem explosion;
    public  ParticleSystem bulletExplosion;
    public  ParticleSystem enemyBulletExplosion;
    public ParticleSystem DamageExplosion;
    public ParticleSystem AsteroidExplosion;
    public Transform PlayerPrefab;
    public Transform ReSpawnPoint;
    public int RespawnBackCount = 3;
    public int score;
    public int scoreIncreas = 100;
    public static GameObject gameOverUI;
    public GameObject gameOverUIRef;
    public static GameObject scoreUI;
    public GameObject scoreUIRef;
    

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

        Instantiate(PlayerPrefab,ReSpawnPoint.position,ReSpawnPoint.rotation);

    }

    public static void KillPlayer(Player player){
        gameOverUI.SetActive(true);
        scoreUI.SetActive(false);
        gm.explosion.transform.position = player.transform.position;
        ExplosionEffect(player.transform);
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.ReSpawnPlayer());
        gm.score = 0;
    }
    public static void KillEnemy(EnemyStats enemy){
        ExplosionEffect(enemy.transform);
        Destroy(enemy.gameObject);
        if (Player.dashStatus)
        {
            PlayerStatsScript.instance.SetPlayerEnergy(25);
        }
    }
    public void DestroyAsteroid(Asteroid asteroid){
        AsteroidExplosionEffect(asteroid.transform,asteroid.size);
        Destroy(asteroid.gameObject);
    }

    public static void ExplosionEffect(Transform objectTransform){
        gm.explosion.transform.position = objectTransform.transform.position;
        gm.explosion.Play();
    }
    public static void AsteroidExplosionEffect(Transform objectTransform,float size){
        gm.AsteroidExplosion.startSize = size/5;
        gm.AsteroidExplosion.transform.position = objectTransform.transform.position;
        gm.AsteroidExplosion.Play();
    }
    public static void DamageExplosionEffect(Transform objectTransform,float size){
        gm.DamageExplosion.startSize = size/5;
        gm.DamageExplosion.transform.position = objectTransform.transform.position;
        gm.DamageExplosion.Play();
    }
    public static void BulletExplosionEffect(Transform objectTransform){
        gm.bulletExplosion.transform.position = objectTransform.transform.position;
        gm.bulletExplosion.Play();
    }
    public static void CollisonPointEffect(Vector2 CarpısmaNoktası){
        gm.bulletExplosion.transform.position = CarpısmaNoktası;
        gm.bulletExplosion.Play();
    }
     public static void EnemyCollisonPointEffect(Vector2 CarpısmaNoktası){
        gm.enemyBulletExplosion.transform.position = CarpısmaNoktası;
        gm.enemyBulletExplosion.Play();
    }
    public void EndGame(){
        gameOverUI.SetActive(true);
    }
}
