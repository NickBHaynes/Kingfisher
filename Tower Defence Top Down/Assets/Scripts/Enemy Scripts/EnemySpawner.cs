using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawners;
    public GameObject spawnVFX;
    public GameObject enemyGroup;


    public float timeBetweenSpawns = 1f;
    public float timeUntilNextSpawn = 0.5f;
    public float amountSpawned;
    private int spawnerNum;
    private int enemyToSpawn;

    // new wave mode
    [Header("New Wave Spawning System")]
    public Wave[] waves;
    private int currentWave;
    private int totalSpawnedinCurrentWave;
    public TMP_Text waveCountLabel;
    public GameObject nextWavePanel;

    // Start is called before the first frame update
    void Start()
    {
        amountSpawned = 0;
        totalSpawnedinCurrentWave = 0;
        waveCountLabel.text = "Wave " + waves[currentWave].waveNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        waveSpawn();
        

        if (totalSpawnedinCurrentWave >= waves[currentWave].totalWaveSpawnCount && amountSpawned <= 0 && currentWave < waves.Length)
        {
            Time.timeScale = 0;
            nextWavePanel.SetActive(true);
            currentWave++;
            amountSpawned = 0;
            totalSpawnedinCurrentWave = 0;

            waveCountLabel.text = "Wave " + waves[currentWave].waveNumber.ToString();
        }
    }

    public void ResumeNextWave()
    {
        Time.timeScale = 1;
        nextWavePanel.SetActive(false);
    }

    public void EnemyKilled()
    {
        amountSpawned--;
    }

    public void waveSpawn()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            if (i == currentWave)
            {
                waveArrayToSpawn(i);
            }
        }

    }

    public void waveArrayToSpawn(int currentWave)
    {
        if (timeUntilNextSpawn <= 0 && amountSpawned < waves[currentWave].amountSpawnable && totalSpawnedinCurrentWave <= waves[currentWave].totalWaveSpawnCount)
        {
            // this section will spawn a random enemy from the current wave
            enemyToSpawn = Random.Range(0, waves[currentWave].enemies.Length);
            spawnerNum = Random.Range(0, spawners.Length);

            var newEnemy = Instantiate(waves[currentWave].enemies[enemyToSpawn], spawners[spawnerNum].position, Quaternion.identity);
            newEnemy.transform.parent = enemyGroup.transform;

            if (spawnVFX != null)
            {
                var newVFX = Instantiate(spawnVFX, spawners[spawnerNum].position, Quaternion.identity);
                newVFX.transform.parent = spawners[spawnerNum].transform;
            }

            amountSpawned++;
            totalSpawnedinCurrentWave++;
            timeUntilNextSpawn = timeBetweenSpawns;

        }

        timeUntilNextSpawn -= Time.deltaTime;

    }
}
