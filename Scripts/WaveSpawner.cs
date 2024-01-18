using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public static int BossAlive = 0;
    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 5f;
    public Text waveCountdownText;
    public static float enemyHP;

    public static int waveIndex = 0;

    public GameManager gameManager;


    void Start()
    {
        EnemiesAlive = 0;
        waveIndex = 0;
    }

    void Update()
    {
        if (EnemiesAlive > 0 || (BossAlive > 0 && waveIndex == 6))
        {
            return;
        }

        

        if (waveIndex == waves.Length)
        {
            Debug.Log("E:" + EnemiesAlive + "B" + BossAlive);
            if (EnemiesAlive <= 0 && BossAlive <= 0)
            {
                gameManager.WinGame();
                this.enabled = false;
            }
            return;
        }

        //if (waveIndex != 7)
        //{
            if (countdown <= 0f)
            {

                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
                return;
            }
        //}
        //else
        //{
        //    StartCoroutine(SpawnWave());
        //    countdown = 0f;
        //    return;
        //}

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        //waveCountdownText.text = Mathf.Round(countdown).ToString();
        ///waveCountdownText.text = "Next Wave in: " + string.Format("{0:00}", countdown);
        waveCountdownText.text = "Next Wave in: " + Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveIndex];

        enemyHP = wave.healthpoints;
        EnemiesAlive = wave.count;

        if(waveIndex == 6)
        {
            EnemiesAlive = 0;
            BossAlive = 1;
        }
        
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            //yield return new WaitForSeconds(1f / wave.rate);
            yield return new WaitForSeconds(0.3f);
        }
        
        
        waveIndex++;
        PlayerStats.Waves++;

       
    }

    void SpawnEnemy(GameObject enemy)
    {

       

        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);



    }
}
