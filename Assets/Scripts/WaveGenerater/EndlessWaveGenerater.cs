using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EndlessWaveGenerater : MonoBehaviour
{
    // Script Reference
    GameManager gameManager;
    SaveDataManager saveDataManager;
    UIManager ui;
    [SerializeField] GlobalSoundManager soundManager;

    // Spawners
    public Transform defaultSpawnLocation;
    public GameObject[] spawners;

    // Enemy List
    public GameObject[] tier1;
    public GameObject[] tier2;
    public GameObject[] tier3;
    public GameObject[] tier4;
    public GameObject[] tier5;

    // Enemy Information
    public int enemyHpModifier = 0;
    public int enemyPointRewardModifier = 0;

    // Wave Information
    public float countdownUntilNextWave = 30;
    public int currentWave = 0;
    int enemiesSpawned = 0;
    int enemiesToSpawn = 2;
    float spawnInterval = 2;
    public int enemyTier = 1;
    public int unlockTier = 0;
    public bool readyToSpawn = true;
    public bool spawningHasCompleted = false;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        saveDataManager = gameManager.GetComponent<SaveDataManager>();
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();

        ProceedToNextWave();

    }

    public void Update()
    {
        CheckIfAllEnemiesHaveBeenSpawned();

        if(isEnemyInScene())
        {
            ui.HideCountdown();
            countdownUntilNextWave = 30;
        }
        else if(!isEnemyInScene() && spawningHasCompleted)
        {
            ui.ShowCountdown();
            countdownUntilNextWave -= Time.deltaTime;
        }

        if (countdownUntilNextWave <= 0 && readyToSpawn && !isEnemyInScene())
        {
            ProceedToNextWave();
        }
    }

    private void UpdateEnemyHP(GameObject Enemy)
    {
        Enemy.GetComponent<Enemy>().hp += enemyHpModifier;
    }

    private void CheckIfAllEnemiesHaveBeenSpawned()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemiesSpawned >= enemiesToSpawn)
        {
            spawningHasCompleted = true;
        }
        else
        {
            spawningHasCompleted = false;
        }
    }

    public void EndCountdown()
    {
        countdownUntilNextWave = 0;
    }

    private void ProceedToNextWave()
    {
        // Update Current Wave
        currentWave++;
        enemiesSpawned = 0;

        gameManager.wavesSurvived = currentWave;

        if(saveDataManager.highestWaveOnAnyMap < currentWave)
        {
            saveDataManager.highestWaveOnAnyMap++;
        }
        
        if(gameManager.difficulty == GameManager.currentDifficulty.easy)
        {
            // Update Modifiers
            enemyHpModifier += 3;
            //enemyPointRewardModifier += 25;
            enemiesToSpawn += 3;
            unlockTier++;
        }
        else if(gameManager.difficulty == GameManager.currentDifficulty.normal)
        {
            enemyHpModifier += 5;
            //enemyPointRewardModifier += 10;
            enemiesToSpawn += 5;
            unlockTier++;
        }
        else
        {
            enemyHpModifier += 10;
            //enemyPointRewardModifier += 150;
            enemiesToSpawn += 10;
            unlockTier++;
        }

        if(unlockTier == 5)
        {
            unlockTier = 0;
            enemyTier++;
        }

        // Spawn Enemies
        StartCoroutine(SpawnInterval());

        soundManager.PlayStartingWaveSound();
    }

    private Vector2 GiveRandomPosition()
    {
        //Vector2 randomSpawnPosition = spawners[Random.Range(0, spawners.Length)].transform.position;
        //return randomSpawnPosition;

        GameObject randomSpawner = spawners[Random.Range(0, spawners.Length)];
        if(randomSpawner.activeSelf)
        {
            return randomSpawner.transform.position;
        }
        else
        {
            return defaultSpawnLocation.position;
        }

    }

    private void SpawnEnemies(Vector2 position)
    {
        if(enemyTier == 1)
        {
            GameObject randomEnemy = tier1[Random.Range(0, tier1.Length)];
            Instantiate(randomEnemy, position, Quaternion.identity);
        }
        else if (enemyTier == 2)
        {
            GameObject randomEnemy = tier2[Random.Range(0, tier2.Length)];
            Instantiate(randomEnemy, position, Quaternion.identity);
        }
        else if (enemyTier == 3)
        {
            GameObject randomEnemy = tier3[Random.Range(0, tier3.Length)];
            Instantiate(randomEnemy, position, Quaternion.identity);
        }
        else if (enemyTier == 4)
        {
            GameObject randomEnemy = tier4[Random.Range(0, tier4.Length)];
            Instantiate(randomEnemy, position, Quaternion.identity);
        }
        else
        {
            GameObject randomEnemy = tier5[Random.Range(0, tier5.Length)];
            Instantiate(randomEnemy, position, Quaternion.identity);
        }
    }

    private bool isEnemyInScene()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
            return true;
        else return false;
    }

    private IEnumerator SpawnInterval()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemies(GiveRandomPosition());
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }

        readyToSpawn = true;
    }
}
