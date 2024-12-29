using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public GameObject nesne;
 

    
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            StartGame script1 = nesne.GetComponent<StartGame>();
            script1.isPlay = false;
            string currentSceneName = SceneManager.GetActiveScene().name;
            Time.timeScale = 1f;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
