using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;

public class ChangeScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject car;
    int score = 10;
    int score2 = 0;
    Rigidbody carrb;
    bool kontrol = true;
    int sayac = 0;

    public Text textToFade;
    private float fadeDuration = 0.7f; 
    private bool shouldFade = false;

    private List<GameObject> activeObstacles = new List<GameObject>();
    private List<GameObject> activeObstacles2 = new List<GameObject>();

    void Start()
    {
        textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0);
        carrb = car.GetComponent<Rigidbody>();
        scoreText.text = "Score : " + score;
    }

    void Update()
    {
        foreach(var car in GameObject.FindGameObjectsWithTag("Cars"))
        {
            if (kontrol)
            {
                activeObstacles2.Add(car);
                kontrol = false;
            }
            activeObstacles.Add(car);
        }
        if(activeObstacles.Count>0 && activeObstacles2.Count > 0)
        {
            if(activeObstacles2[activeObstacles2.Count-1] != activeObstacles[activeObstacles.Count-1])
            {
                activeObstacles2.Add(activeObstacles[activeObstacles.Count-1]);
            }
        }
        
       
        
        Near();
        score = (int)(car.transform.position.z/2);
        
        scoreText.text = "Score : " + (score + score2);
        activeObstacles.Clear();
        if (shouldFade)
        {
            float newAlpha = Mathf.Lerp(textToFade.color.a, 0, Time.deltaTime / fadeDuration);
            textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, newAlpha);
            textToFade.text = "Near! x " + sayac;
            if (textToFade.color.a < 0.01f)
            {
                sayac = 0;
                shouldFade = false;
                textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0);
            }
        }


    }

    public void ShowText()
    {
        textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 1);

        shouldFade = true;
    }

    void Near()
    {
        for(int i = 0; i<activeObstacles.Count; i++)
        {
            if((car.transform.position.x - activeObstacles[i].transform.position.x)<3 && (car.transform.position.x - activeObstacles[i].transform.position.x) > -3)
            {
                if((car.transform.position.z - activeObstacles[i].transform.position.z) < 1f && (car.transform.position.z - activeObstacles[i].transform.position.z) > 0.8f)
                {
                    Debug.Log("KontRol" + activeObstacles2.Count);
                    int j = 0;
                    while(j<activeObstacles2.Count)
                    {
                        Debug.Log("KontRol22");
                        if (activeObstacles[i] == activeObstacles2[j])
                        {
                            
                            score2 += 50 + sayac*2;
                            activeObstacles2.RemoveAt(j);
                            sayac += 1;
                            ShowText();
                        }
                        else
                        {
                            j += 1;
                        }

                    }

                }
                  
            }
        }
    }
    
}
