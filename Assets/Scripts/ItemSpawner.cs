using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponPrefabs;
    [SerializeField] private GameObject[] healthPotionPrefabs;
    [SerializeField] private int spawnCount = 10;
    [SerializeField] private float itemLifetime = 15f;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private GameObject spawnArea;

    private List<Vector3> spawnPositions = new List<Vector3>();

    void Start()
    {
        StartCoroutine(SpawnItemsContinuously());
    }

    IEnumerator SpawnItemsContinuously()
    {
        while (true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject itemToSpawn = GetRandomItemPrefab();
                Vector3 spawnPosition = GetRandomPositionOnCollider();

                spawnPositions.Add(spawnPosition);
                GameObject spawnedItem = Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);
                StartCoroutine(DestroyItemAfterTime(spawnedItem));
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    GameObject GetRandomItemPrefab()
    {
        int randomIndex = Random.Range(0, 2);

        if (randomIndex == 0)
        {
            int weaponIndex = Random.Range(0, weaponPrefabs.Length);
            return weaponPrefabs[weaponIndex];
        }
        else
        {
            int potionIndex = Random.Range(0, healthPotionPrefabs.Length);
            return healthPotionPrefabs[potionIndex];
        }
    }

    Vector3 GetRandomPositionOnCollider()
    {
        Vector3 randomPosition;

        Vector3 center = spawnArea.GetComponent<BoxCollider>().center;
        Vector3 size = spawnArea.GetComponent<BoxCollider>().size;

        do
        {
            float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
            float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

            randomPosition = new Vector3(randomX, spawnArea.transform.position.y, randomZ);
        }
        while (IsPositionOccupied(randomPosition));

        return randomPosition;
    }

    bool IsPositionOccupied(Vector3 position)
    {
        foreach (Vector3 occupiedPosition in spawnPositions)
        {
            if (Vector3.Distance(position, occupiedPosition) < 1f)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator DestroyItemAfterTime(GameObject item)
    {
        yield return new WaitForSeconds(itemLifetime);
        spawnPositions.Remove(item.transform.position);
        Destroy(item);
    }
}
