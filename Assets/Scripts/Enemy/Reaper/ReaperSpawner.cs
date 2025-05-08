using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    private Vector2 currentPosition;
    private float timer;
    public float spawnInterval;


    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        timer += Time.deltaTime;

        currentPosition = transform.position;

        Vector2 randomOffset = new Vector2(Random.Range(-1, 1), .5f);

        if(timer > spawnInterval)
        {
            timer = 0;
            Instantiate(enemy, currentPosition + randomOffset, Quaternion.identity);
        }
    }
}
