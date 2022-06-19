using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{
   public enum SpawnState { SPAWNING, WAITING, COUNTING };
   [System.Serializable]
   public class Wave
   {
       public string name;
       public Transform[] enemy;
       public int enemyType = 0;
       public int count;
       public float rate;
   }

   public Wave[] waves;
   private int nextWave = 0;
   public int  NextWave
   {
       get{return nextWave +1;}
   }

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;
    public float WaveCountDown
    {
        get {return waveCountDown +1;}
    }

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
	public SpawnState State
	{
		get { return state; }
	}

    void Start()
    {
        if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points referenced.");
		}

		waveCountDown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if (waveCountDown<=0)
        {
            if (state != SpawnState.SPAWNING)
            {
                
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
            
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }


    void WaveCompleted()
    {
        Debug.Log("DALGA TEMİZLENDİ");
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (nextWave+1>waves.Length-1)
        {
            nextWave = 0;
            Debug.Log("BÜTÜN DALGALAR TAMAMLANDI(CİNSEL OLMAYAN)");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown<=0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("DALGA GELİYOR : "+ _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            if (_wave.enemy.Length==1)
            {
                SpawnEnemy(_wave.enemy[0]);
            }
            if (_wave.enemy.Length==2)
            {
                SpawnEnemy(_wave.enemy[0]);
                SpawnEnemy(_wave.enemy[1]);
            }
            // SpawnEnemy(_wave.enemy[_wave.enemyType]);
            yield return new WaitForSeconds(1f/_wave.rate);
        }
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Düşman oluşturuluyor : "+_enemy);
        Transform _sp = spawnPoints[Random.Range(0,spawnPoints.Length)];
        Instantiate(_enemy,_sp.position,_sp.rotation);
    }
}
