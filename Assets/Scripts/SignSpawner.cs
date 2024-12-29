using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignSpawner : MonoBehaviour
{
    public Transform player;
    public List<GameObject> signPrefabs;

    float spawnDistance = 20f;
    float oldPosition = 0f;

    void Update()
    {
        if(player.transform.position.z - oldPosition > spawnDistance)
        {
            SignSpawn();
        }
        foreach (GameObject sign in GameObject.FindGameObjectsWithTag("Sign"))
        {
            if (sign.transform.position.z < player.position.z - 30f)
            {
                Destroy(sign);
            }
        }
    }

    void SignSpawn()
    {

        float zIndex = Random.Range(40f, 45f);

        Vector3[] positions = new Vector3[]
        {
            new Vector3(2.5f, 0.5f, zIndex+player.transform.position.z),
            new Vector3(-10f, 0.5f, zIndex+player.transform.position.z),

        };
        int positionIndex = Random.Range(0, 2);


        

        int signIndex = Random.Range(0, signPrefabs.Count);
        GameObject newSign = Instantiate(signPrefabs[signIndex], positions[positionIndex], Quaternion.identity);

        oldPosition += newSign.transform.position.z;
    }


    
}
