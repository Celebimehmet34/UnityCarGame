using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CarMovement : MonoBehaviour
{

    public AudioSource engineAudio;



    public UnityEngine.UI.Slider speedBar;
    public Gradient colorGradient;
    public UnityEngine.UI.Image fillImage;

    float moveSpeed = 20f;
    public float forwardSpeed = 13f;
    private Rigidbody rb;
    public RawImage resim;

    public GameObject restartText;


    

    /// <summary>
    /// 
    /// </summary>
    public TextMeshProUGUI scoreText;
    public GameObject car;
    int score = 10;
    int score2 = 0;
    Rigidbody carrb;
    bool kontrol = true;
    int sayac = 0;

    public TextMeshProUGUI speedText;



    private List<GameObject> activeObstacles = new List<GameObject>();
    private List<GameObject> activeObstacles2 = new List<GameObject>();
    /// <summary>
    /// 
    /// </summary>
    /// 
    RectTransform scorePosition;


    public TextMeshProUGUI textToFade;

    private float fadeDuration = 0.5f; 
    private bool shouldFade = false;


    public GameObject nesne;


    int timer = 0;
    void Start()
    {


        forwardSpeed = 13f;
        StartGame script1 = nesne.GetComponent<StartGame>();

        textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0);

        carrb = car.GetComponent<Rigidbody>();
        scoreText.text = "Score : " + score;
        scorePosition = scoreText.GetComponent<RectTransform>();
        ///
        resim.enabled = false;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    if (touch.position.x > Screen.width / 2)
                    {
                        float maxSpeed = 28f;
                        float hizArtisOrani = 0.05f;
                        float kalanHiz = maxSpeed - forwardSpeed;
                        kalanHiz = (3f + kalanHiz) / 6.4f;

                        if (kalanHiz > 0)
                        {
                            forwardSpeed += kalanHiz * hizArtisOrani * Time.deltaTime * 12f;
                        }
                    }
                    else
                    {
                        if (forwardSpeed > 8f)
                        {
                            forwardSpeed -= 0.3f * Time.deltaTime * 45;
                        }
                    }
                }
            }
            else
            {
                if ((forwardSpeed - 13f) < 0.03f && (forwardSpeed - 13f) > -0.03f)
                {
                    forwardSpeed = 13f;
                }
                if (forwardSpeed > 13)
                {
                    float maxSpeed = 13f;
                    float hizArtisOrani = 0.05f;
                    float kalanHiz = maxSpeed - forwardSpeed;

                    if (kalanHiz < 0)
                    {
                        kalanHiz = -(6f - kalanHiz) / 12.4f;
                        forwardSpeed += kalanHiz * hizArtisOrani * Time.deltaTime * 13;
                    }
                }
                else if (forwardSpeed < 13)
                {
                    float maxSpeed = 13f;
                    float hizArtisOrani = 0.05f;
                    float kalanHiz = maxSpeed - forwardSpeed;
                    kalanHiz = (0.8f + kalanHiz) / 6.4f;

                    if (kalanHiz > 0)
                    {
                        forwardSpeed += kalanHiz * hizArtisOrani * Time.deltaTime * 35f;
                    }
                }
            }
        }

        ///MOUSE

        if (Input.GetMouseButton(0))  // Sol týk basýlý tutulduðunda
        {
            Vector3 mousePos = Input.mousePosition;  // Fare pozisyonu ekranda

            if (mousePos.x > Screen.width / 2)
            {
                float maxSpeed = 28f;
                float hizArtisOrani = 0.05f;
                float kalanHiz = maxSpeed - forwardSpeed;
                kalanHiz = (3f + kalanHiz) / 6.4f;

                if (kalanHiz > 0)
                {
                    forwardSpeed += kalanHiz * hizArtisOrani * Time.deltaTime * 12f;
                }
            }
            // Sol tarafa týklandýðýnda hýz düþecek
            else
            {
                
                if (forwardSpeed > 8f)
                {
                    forwardSpeed -= 0.5f * Time.deltaTime * 45;  // Zamanla kademeli azalýþ

                }
            }
        }
        else
        {
            if((forwardSpeed-13f)<0.03f && (forwardSpeed - 13f) > -0.03f)
            {
                forwardSpeed = 13f;
            }
            if (forwardSpeed > 13)
            {
                float maxSpeed = 13f;
                float hizArtisOrani = 0.05f;
                float kalanHiz = maxSpeed - forwardSpeed;


                if (kalanHiz < 0)
                {
                    kalanHiz = -(6f - kalanHiz) / 12.4f;
                    forwardSpeed += kalanHiz * hizArtisOrani * Time.deltaTime * 13;
                }
            }
            else if (forwardSpeed< 13)
            {
                float maxSpeed = 13f;
                float hizArtisOrani = 0.05f;
                float kalanHiz = maxSpeed - forwardSpeed;
                kalanHiz = (0.8f + kalanHiz) / 6.4f;

                if (kalanHiz > 0)
                {
                    forwardSpeed += kalanHiz * hizArtisOrani * Time.deltaTime * 35f;
                }
            }
        }

       
        StartGame script1 = nesne.GetComponent<StartGame>();
        if (script1.isPlay)
        {
            MoveForward();
            MoveSideways();
        }

        timer++;

        if (timer > 50)
        {
            timer = 0;
        }
    }

    private void Update()
    {


        speedBar.minValue = 0f;
        speedBar.maxValue = 1f;
        speedBar.value = Mathf.Clamp01(forwardSpeed / 28f);
        fillImage.color = colorGradient.Evaluate(speedBar.value);

        float normalizedSpeed = forwardSpeed / 24f - 0.1f;

        engineAudio.volume = Mathf.Lerp(0, 1, normalizedSpeed);

        engineAudio.pitch = Mathf.Lerp(1, 2, normalizedSpeed);


        speedText.color = fillImage.color;
        if (Mathf.Round(forwardSpeed * 10) <= 280 && Mathf.Round(forwardSpeed * 10) >=80)
        {
            speedText.text = "Speed: " + (Mathf.Round(forwardSpeed*10)) + " km/h";
        }
        

        
        foreach (var car in GameObject.FindGameObjectsWithTag("Cars"))
        {
            if (kontrol)
            {
                activeObstacles2.Add(car);
                kontrol = false;
            }
            activeObstacles.Add(car);
        }
        if (activeObstacles.Count > 0 && activeObstacles2.Count > 0)
        {
            if (activeObstacles2[activeObstacles2.Count - 1] != activeObstacles[activeObstacles.Count - 1])
            {
                activeObstacles2.Add(activeObstacles[activeObstacles.Count - 1]);
            }
        }


        if (shouldFade)
        {
            float newAlpha = Mathf.Lerp(textToFade.color.a, 0, Time.deltaTime / fadeDuration);
            textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, newAlpha);

            if (textToFade.color.a < 0.01f)
            {
                shouldFade = false;
                textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0);
                sayac = 0;
            }
        }
        

        Near();
        score = (int)(car.transform.position.z / 2);

        scoreText.text = "Score : " + (score + score2);
        activeObstacles.Clear();
        
    }
    public void ShowText()
    {
        textToFade.text = "Near x " + sayac;
        textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 1);
        
        shouldFade = true;
    }
    void MoveForward()
    {
           
        Vector3 forwardMovement = transform.forward * forwardSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);
    }

    void MoveSideways()
    {
        if (Application.isMobilePlatform)
        {
            
            Vector3 tilt = Input.acceleration;
            float moveDirection = tilt.x;
            Vector3 newPosition = rb.position + new Vector3(1.5f*moveDirection * moveSpeed * Time.deltaTime, 0, 0);
            rb.MovePosition(newPosition);
        }
        else
        {   Debug.Log(moveSpeed);
            float moveHorizontal = Input.GetAxis("Horizontal");

            Vector3 sidewaysMovement = transform.right * moveHorizontal * moveSpeed * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + sidewaysMovement);

        }

        
        
    }

     


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bariyer" || collision.gameObject.tag == "Cars")
        {
            engineAudio.Stop();
            restartText.SetActive(true);
            
            scorePosition.anchoredPosition = new Vector2(0, -450);
            scoreText.fontSize = 68;
            resim.enabled = true;
            Time.timeScale = 0f;

            
        }
    }


    

    
    




    void Near()
    {
        for (int i = 0; i < activeObstacles.Count; i++)
        {
            if ((car.transform.position.x - activeObstacles[i].transform.position.x) < 3.2f && (car.transform.position.x - activeObstacles[i].transform.position.x) > -3.2f)
            {
                if ((car.transform.position.z - activeObstacles[i].transform.position.z) < 1f && (car.transform.position.z - activeObstacles[i].transform.position.z) > 0.8f)
                {
                    
                    int j = 0;
                    while (j < activeObstacles2.Count)
                    {
                        
                        if (activeObstacles[i] == activeObstacles2[j])
                        {
                            sayac += 1;
                            ShowText();
                            score2 += 50 + sayac * 2;
                            activeObstacles2.RemoveAt(j);
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
