using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WaveSpawner : MonoBehaviour
{

    public static int EnemiesAlive = 0;

    public Wave[] waves;

    [SerializeField]
    private Transform spawnpoint;

    [SerializeField]
    private float timeBetwenWaves = 5f;

    private float countdown = 5f;

    [SerializeField]
    private Text WaveCountdownTimer;

    private int waveIndex = 0;

    bool waveRunning = false;

    // Update is called once per frame
    void Update()
    {


        if(EnemiesAlive > 0)
        {
            return;
        }

        if (!waveRunning && countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetwenWaves;
            return;

        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        WaveCountdownTimer.text = string.Format("{0:00.0}", countdown);
    }

    IEnumerator SpawnWave()
    {
        waveRunning = true;
        PlayerStats.rounds++;

        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate);
        }

        waveRunning = false;

        waveIndex++;

        if(waveIndex == waves.Length)
        {
            Debug.Log("Bravo !, c'est gagné ekip");
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnpoint.position, Quaternion.identity);
        EnemiesAlive++;
    }

}
