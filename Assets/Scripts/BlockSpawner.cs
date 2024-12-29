using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;  // Blok prefabý
    public Transform player;        // Karakter referansý
    float spawnDistance = 300f;  // Karakterin önünde kaç birim uzaklýkta blok üretilecek
    private float nextSpawnPosition = 0f;
    public List<GameObject> obstaclePrefabs;
    float oldspeed = 0;
    float newspeed;
    float oldblockZ = 0;
    
    private List<GameObject> activeObstacles = new List<GameObject>();



    private void Start()
    {
        while(player.position.z > nextSpawnPosition - spawnDistance)
        {
            SpawnBlock();
            nextSpawnPosition += 30f;
        }
    }

    void Update()
    {
        Vector3 spawnPosition2 = new Vector3(0, 0, 0);
        if (player.position.z > nextSpawnPosition - spawnDistance)
        {
            Vector3 spawnPosition = new Vector3(0, 0, nextSpawnPosition);
            spawnPosition2 = new Vector3(0, 0, nextSpawnPosition);
            SpawnBlock();
            nextSpawnPosition += 30f;  
            
        }
        if(spawnPosition2.z != 0)
        {
            
            SpawnObstacle((spawnPosition2.z-200f));
        }
        
        DestroyFarObstacles();
        CarSlowDown();
        CarAway();

        
    }

    void SpawnBlock()
    {
        Vector3 spawnPosition = new Vector3(0, 0, nextSpawnPosition);
        Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
        

    }
    void SpawnObstacle(float blockZ)
    {
        
        Vector3[] positions = new Vector3[]
        {
            new Vector3(-3.9f, 0f, blockZ),
            new Vector3(-3.75f, 0f, blockZ),
            new Vector3(-3.75f, 0f, blockZ),
            new Vector3(-3.6f, 0f, blockZ),
            new Vector3(3.9f, 0f, blockZ),
            new Vector3(3.75f, 0f, blockZ),
            new Vector3(3.75f, 0f, blockZ),
            new Vector3(3.6f, 0f, blockZ),
            new Vector3(0f, 0f, blockZ),
            new Vector3(0.2f, 0f, blockZ),
            new Vector3(-0.2f, 0f, blockZ),

        };

        int sayi = Random.Range(0, 2);

        if((blockZ - oldblockZ) > 10 )
        {
            bool flag = true;
            for(int i = 0; i < activeObstacles.Count ; i++)
            {
                if ((activeObstacles[i].transform.position.z - nextSpawnPosition)<-38)
                {

                    flag = false;
                }
            }

            if(activeObstacles.Count == 0)
            {
                flag = false;
            }

            if (!flag)
            {

                int randomIndex = Random.Range(0, positions.Length);
                int obstacleIndex = Random.Range(0, obstaclePrefabs.Count);
                GameObject newObstacle = Instantiate(obstaclePrefabs[obstacleIndex], positions[randomIndex], Quaternion.identity);

                activeObstacles.Add(newObstacle);

                int hizSayi = Random.Range(0, 3);
                newspeed = (hizSayi * 2) + 8;

                while (newspeed == oldspeed)
                {
                    hizSayi = Random.Range(0, 3);
                    newspeed = (hizSayi * 2) + 8;
                }

                Rigidbody rb;
                rb = newObstacle.GetComponent<Rigidbody>();
                rb.velocity = new Vector3(rb.velocity.x, 0, newspeed);
                oldblockZ = blockZ;
            }

        }
        
       
        
    }

    void DestroyFarObstacles()
    {
        float distanceThreshold = player.position.z - spawnDistance;

        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            if (activeObstacles[i].transform.position.z < distanceThreshold)
            {
                Destroy(activeObstacles[i]); 
                activeObstacles.RemoveAt(i); 
            }
        }
    }

    /*void CarSlowDown()
    {
        for(int i = 0; i < activeObstacles.Count; i++)
        {
            for(int j = i; j < activeObstacles.Count; j++)
            {
                if (activeObstacles[i].transform.position.x == activeObstacles[j].transform.position.x)
                {
                    if(activeObstacles[i].transform.position.z - activeObstacles[j].transform.position.z < -10)
                    {


                        
                       // Rigidbody rb;
                        rb = activeObstacles[i].GetComponent<Rigidbody>();
                        rb.velocity = new Vector3(rb.velocity.x, 0, 8f);
                        rb = activeObstacles[j].GetComponent<Rigidbody>();
                        rb.velocity = new Vector3(rb.velocity.x, 0, 8f);
                    }
                }
            }
        }
    }*/
    void CarSlowDown()
    {
        for (int i = 0; i < activeObstacles.Count; i++)
        {
            for (int j = i + 1; j < activeObstacles.Count; j++)
            {
                if ((activeObstacles[i].transform.position.x - activeObstacles[j].transform.position.x) < 3.6f && (activeObstacles[i].transform.position.x - activeObstacles[j].transform.position.x) > -3.6f)
                {
                    float distanceZ = activeObstacles[j].transform.position.z - activeObstacles[i].transform.position.z;

                    


                    if (distanceZ < 12 && distanceZ > 0)
                    {
                        
                        Rigidbody rb1 = activeObstacles[i].GetComponent<Rigidbody>();
                        Rigidbody rb2 = activeObstacles[j].GetComponent<Rigidbody>();

                        if(rb1.velocity.z < rb2.velocity.z && (rb1.velocity.z>3 && rb2.velocity.z >3) && rb2.position.z < rb1.position.z)
                        {
                            while(rb1.velocity.z < rb2.velocity.z)
                            {
                                rb2.velocity = new Vector3(rb2.velocity.x, 0, rb2.velocity.z - 1);
                            }
                            rb1.velocity = new Vector3(rb1.velocity.x, 0, rb1.velocity.z + 1);
                        }
                        else if(rb1.velocity.z > rb2.velocity.z && (rb1.velocity.z > 3 && rb2.velocity.z > 3) && rb2.position.z > rb1.position.z)
                        {
                            while (rb1.velocity.z > rb2.velocity.z)
                            {
                                rb1.velocity = new Vector3(rb1.velocity.x, 0, rb1.velocity.z - 1);
                            }
                            rb2.velocity = new Vector3(rb2.velocity.x, 0, rb2.velocity.z + 1);
                        }


                    }/*else if(distanceZ < 22 && distanceZ > 0)
                    {
                        Rigidbody rb1 = activeObstacles[i].GetComponent<Rigidbody>();
                        Rigidbody rb2 = activeObstacles[j].GetComponent<Rigidbody>();
                        if (rb1.velocity.z < rb2.velocity.z && (rb1.velocity.z > 3 && rb2.velocity.z > 3) && rb2.position.z < rb1.position.z)
                        {
                            
                                rb2.velocity = new Vector3(rb2.velocity.x, 0, rb2.velocity.z - 1.5f);
                          
                        }
                        else if (rb1.velocity.z > rb2.velocity.z && (rb1.velocity.z > 3 && rb2.velocity.z > 3) && rb2.position.z > rb1.position.z)
                        {
                            
                                rb1.velocity = new Vector3(rb1.velocity.x, 0, rb1.velocity.z - 1.5f);
                            
                        }
                    }*/
                }
            }
        }
    }


    void CarAway()
    {
        if(activeObstacles.Count > 2)
        {

        
        for( int i = 0; i<activeObstacles.Count; i++)
        {
            for(int j = i+1; j<activeObstacles.Count; j++)
            {
                if (activeObstacles[i].transform.position.z - activeObstacles[j].transform.position.z<5f && activeObstacles[i].transform.position.z - activeObstacles[j].transform.position.z > -5f)
                {
                    if (activeObstacles[i].transform.position.x - activeObstacles[j].transform.position.x < 4.1f && activeObstacles[i].transform.position.x - activeObstacles[j].transform.position.x > -4.1f)
                    {
                        Rigidbody rb1 = activeObstacles[i].GetComponent<Rigidbody>();
                        Rigidbody rb2 = activeObstacles[j].GetComponent<Rigidbody>();
                        if (activeObstacles[i].transform.position.x < activeObstacles[j].transform.position.x)  
                       {
                            rb1.velocity = new Vector3(rb1.velocity.x - 0.1f, 0.5f, rb1.velocity.z);
                             rb2.velocity = new Vector3(rb2.velocity.x + 0.1f, 0.5f, rb2.velocity.z);
                        }
                        else
                        {
                            rb2.velocity = new Vector3(rb2.velocity.x - 0.1f, 0.5f, rb2.velocity.z);
                            rb1.velocity = new Vector3(rb1.velocity.x + 0.1f, 0.5f, rb1.velocity.z);
                        }
                    }
                    
                }
            }
        }
    }
    }
}
