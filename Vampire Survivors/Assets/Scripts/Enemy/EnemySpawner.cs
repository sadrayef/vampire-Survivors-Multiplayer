using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

//--------------------------------------------------------------------- The most important script for an enemy

public class EnemySpawner : NetworkBehaviour
{

    //---------------------------------------------------------------------

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;
        public int waveQuota; // total number of enemies that are gonna spawn in this wave
        public float spawnInterval; // total time to spawn
        public int spawnCount; // number of the enemy that already spawned

    }

    //---------------------------------------------------------------------

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; // number of enemies that are gonna spawn in this wave
        public int spawnCount; // number of the enemy that already spawned
        public GameObject enemyPrefab;
    }

    //---------------------------------------------------------------------

    public List<Wave> waves;
    public int currentWaveCount; // starts with zero ---> index of the current wave cause an array elements starts with zero

    Transform hero;

    [Header("Spawner Attributes")]
    float spawnTimer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public float waveInterval;
    bool isWaveActive = false;

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints;

    //---------------------------------------------------------------------

    void Start()
    {
        hero = FindObjectOfType<HeroStats>().transform; // to track the hero
        CalculateWaveQuota();
    }

    //---------------------------------------------------------------------

    void Update()
    {
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !isWaveActive) // if there is an inactive enemy wave
        {
            isWaveActive = true;
            StartCoroutine(BeginNextWave()); //moving on to the next wave

            // coroutine = a method that can pause execution and return control to Unity but then continue where it left off on the following frame.
        }


        spawnTimer += Time.deltaTime;

        if(spawnTimer > waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0;
            SpawnEnemies();
        }
    }

    //---------------------------------------------------------------------

    IEnumerator BeginNextWave() // IEnumerator = a function that goes through each yield and returns whatever it yields as a function.
    {
        isWaveActive = true;
        // we used thread
        yield return new WaitForSeconds(waveInterval);  // yeild = You can think of yield as a thread operation. In each frame the Unity engine waits for all of its 'threads' to finish before advancing to the next frame
        if (currentWaveCount < waves.Count -1 )
        {
            isWaveActive= false;
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    //---------------------------------------------------------------------

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    //---------------------------------------------------------------------

    void SpawnEnemies()
    {
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    
                    Instantiate(enemyGroup.enemyPrefab, hero.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                    //Limit the number of the enemies that can spawn in each waves
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }
                }
            }
        }
        if(enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    //---------------------------------------------------------------------

    public void OnEnemyKilled()
    {
        enemiesAlive--;
    }

    //---------------------------------------------------------------------

}
