using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeminSpawn : MonoBehaviour
{

    public GameObject zeminPrefab;
    public Transform player;
    float spawnDistance = 100f;
    private float nextSpawnPosition = 0f;

    void Start()
    {
        while (player.position.z > nextSpawnPosition - spawnDistance)
        {
            SpawnZemin();
            nextSpawnPosition += 30f;
        }
    }

    void Update()
    {
        if (player.position.z > nextSpawnPosition - spawnDistance)
        {
            Vector3 spawnPosition = new Vector3(0, 0, nextSpawnPosition);
            SpawnZemin();
            nextSpawnPosition += 30f;

        }
    }

    void SpawnZemin()
    {

        Vector3 spawnPosition = new Vector3(0, -0.1f, nextSpawnPosition);
        Quaternion rotation = Quaternion.Euler(-90, 90, 0);
        GameObject newZemin = Instantiate(zeminPrefab, spawnPosition, rotation);

        newZemin.transform.localScale = new Vector3(4000f, 3000f, 100f);

    }
}
