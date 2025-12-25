using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    Transform spawner;
    List<Transform> spawnSpots;

    public float spawnRate = 1f;

    public int maxSpawned = 5;
    int spawned;

    void Awake()
    {
        spawner = GameObject.Find("Spawner").transform;
        spawnSpots = new List<Transform>();
    }

    void Start()
    {
        spawned = 0;
        InitializeSpawnSpot();
        StartCoroutine(SpawnEnemy());
    }

    void InitializeSpawnSpot()
    {
        foreach (Transform child in spawner)
        {
            spawnSpots.Add(child);
        }
    }

    void EnemySpawner()
    {
        GameObject newEnemy = Instantiate(enemy, spawnSpots[Random.Range(0, spawnSpots.Count)].position, this.transform.rotation);
        spawned++;
    }

    IEnumerator SpawnEnemy()
    {
        while (maxSpawned - spawned >= 0)
        {
            yield return new WaitForSeconds(spawnRate);
            EnemySpawner();
        }
    }
}
