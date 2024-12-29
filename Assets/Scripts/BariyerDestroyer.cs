using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BariyerDestroyer : MonoBehaviour
{

    public Transform player;       



    void Update()
    {
        foreach (GameObject bariyer in GameObject.FindGameObjectsWithTag("Bariyer"))
        {
            if (bariyer.transform.position.z < player.position.z - 30f) 
            { 
                Destroy(bariyer);
            }
        }
    }
}
