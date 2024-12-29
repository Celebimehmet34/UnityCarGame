using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BariyerSag : MonoBehaviour
{

    public GameObject bariyerPrefab; 
    public Transform player;
    public float spawnDistance = 90f;
    private float nextSpawnPosition = 0f;
    /*void Start()
    {
        while (player.position.z > nextSpawnPosition - spawnDistance)
        {
            SpawnBariyer();
            nextSpawnPosition += bariyerPrefab.transform.localScale.z + 9.7f;
        }
    }*/

    void Update()
    {
        if (player.position.z > nextSpawnPosition - spawnDistance)
        {
            Vector3 spawnPosition = new Vector3(0, 0, nextSpawnPosition);
            SpawnBariyer();
            nextSpawnPosition += bariyerPrefab.transform.localScale.z+9.7f;
        }
        
    }

    void SpawnBariyer()
    {
        Vector3 spawnPosition = new Vector3(6.5f, 0, nextSpawnPosition);
        Quaternion rotation = Quaternion.Euler(0, 180, 0);
        Instantiate(bariyerPrefab, spawnPosition, rotation);

    }
}
