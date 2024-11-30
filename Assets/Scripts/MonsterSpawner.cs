using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    [SerializeField] private int monstersCount = 3;
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
        for (int i = 0; i < monstersCount; i++)
        {
            Instantiate(monsterPrefab, GetRandomPoint(), transform.rotation);
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
