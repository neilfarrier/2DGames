using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Enemy enemyToSpawn;

    [SerializeField]
    private float spawnRegionWidth;

    [SerializeField]
    private Bases bases;

    public IEnumerator StartSpawning()
    {
        SpawnEnemy();
        yield return new WaitForSeconds(Random.Range(1f,3f));
        StartCoroutine(StartSpawning());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private Vector3 GetRandomPosition()
    {
        float randomXPosition = Random.Range(-spawnRegionWidth, spawnRegionWidth);
        return new Vector3(randomXPosition, transform.position.y, transform.position.z);
    }

    void SpawnEnemy()
    {
        Enemy enemyInstance = Instantiate(enemyToSpawn, GetRandomPosition(), Quaternion.identity);
        enemyInstance.AssignTarget(bases.GetRandomBase(), Random.Range(1f,3f));
    }
}
