using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private int minMonstersCount = 1;
    [SerializeField] private int maxMonstersCount = 5;
    [SerializeField] private float spawnTimeDifference = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SpawnMonsters());
        }
    }

    IEnumerator SpawnMonsters()
    {
        int randomMonsterCount = Random.Range(minMonstersCount, maxMonstersCount + 1);

        for (int i = 0; i < randomMonsterCount; i++)
        {
            // Choose random monster
            GameObject randomMonsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];

            // Spawn monster
            Instantiate(randomMonsterPrefab, GetRandomPoint(), transform.rotation);
            yield return new WaitForSeconds(spawnTimeDifference);
        }
    }

    Vector3 GetRandomPoint()
    {
        Collider spawnerCollider = GetComponent<Collider>();
        Bounds bounds = spawnerCollider.bounds;

        // Generate a random point within the bounds
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
