using UnityEngine;

public class BlockDestroyer : MonoBehaviour
{
    public Transform player;       
    float destroyDistance = 100f;  

    void Update()
    {
        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
        {
            if (block.transform.position.z < player.position.z - destroyDistance)
            {
                Destroy(block);
            }
        }
    }
}
