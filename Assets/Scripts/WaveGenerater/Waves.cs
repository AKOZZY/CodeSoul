using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class WaveInfo
{
    public string waveName;
    public int spawnInterval;

    public int enemiesNeededToSpawn;
    public int enemiesInScene;

    public bool allEnemiesSpawned = false;

    public GameObject[] enemies;

    private void Update()
    {
        AllEnemiesSpawned();
    }

    private void AllEnemiesSpawned()
    {
        enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemiesNeededToSpawn = enemies.Length;

        if(enemiesInScene >= enemies.Length)
        {
            allEnemiesSpawned = true;
        }
        else
        {
            allEnemiesSpawned = false;
        }
    }
}


public class Waves : MonoBehaviour
{
    //[SerializeField] UIManager ui;
    [SerializeField] GlobalSoundManager gsm;

    public int waveNumber = 0;
    public int waveIndex = 0;

    public bool canSpawn = false;

    private WaveInfo currentWave;

    [SerializeField] WaveInfo[] waves;
    [SerializeField] GameObject[] spawnLocations;

    public float countdown = 30;

    int enemyCap;

    [SerializeField] int maximumEnemyCount;


    private void Update()
    {
        WaveInformation();
        EndGame();
        //Debug.Log(currentWave.waveName);
    }

    private void EndGame()
    {
        if(waveIndex == 20)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void SpawnEnemy(GameObject enemy, GameObject spawnLocation)
    {
        //GameObject randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
        Instantiate(enemy, spawnLocation.transform.position, Quaternion.identity);
    }

    private GameObject GenerateRandomPosition()
    {
        GameObject randomLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];
        return randomLocation;
    }

    private void WaveInformation()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        currentWave = waves[waveIndex];
        
        countdown -= Time.deltaTime;
        

        if(waveIndex != 20)
        {
            if (countdown <= 0 && !canSpawn && !enemy)
            {
                canSpawn = true;
                SpawnWave();
                gsm.PlayStartingWaveSound();
            }
        }

        if(enemy)
        {
            countdown = 30;
        }
        else
        {
            canSpawn = false;
        }

    }

    private void SpawnWave()
    {
        if(waveIndex != 20)
        {
            if (waveIndex == 0 && waveNumber == 0)
            {
                waveNumber++;
                StartCoroutine(SpawnInterval());
            }
            else
            {
                waveIndex++;
                waveNumber++;
                StartCoroutine(SpawnInterval());
            }
        }
    }

    public void EndCountdown()
    {
        countdown = 0;
    }

    private IEnumerator SpawnInterval()
    {
        for(int i = 0; i < waves[waveIndex].enemies.Length; i++)
        {
            SpawnEnemy(waves[waveIndex].enemies[i],GenerateRandomPosition());
            yield return new WaitForSeconds(currentWave.spawnInterval);
        }
    }
}
