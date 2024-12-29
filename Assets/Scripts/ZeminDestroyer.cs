using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeminDestroyer : MonoBehaviour
{

    public Transform player;


    void Update()
    {
        foreach (GameObject zemin in GameObject.FindGameObjectsWithTag("Zemin"))
        {


            if (zemin.transform.position.z < player.position.z - 60f)
            {
                Destroy(zemin);
            }
        }
    }
}
