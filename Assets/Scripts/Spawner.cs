using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    public float spawnRate = 1f;
    public int maxSpawned = 5;
    int spawned;

    void Start()
    {
        spawned = 0;
        StartCoroutine(SpawnEnemy());
    }


    void EnemySpawner()
    {
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-50, 50), transform.position.y, Random.Range(-50, 50)), this.transform.rotation);
        spawned++;
    }

    IEnumerator SpawnEnemy()
    {
        while (spawned < maxSpawned)
        {
            yield return new WaitForSeconds(spawnRate);
            EnemySpawner();
        }
    }
}
