using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public bool isPlay = false;
    public GameObject tapToStartText;  
    public GameObject gameMechanics;
    public GameObject Car;
    public TextMeshProUGUI textToFade;
    public GameObject restartGame;
    Rigidbody rb;

    void Start()
    {
        isPlay = false;
        rb = Car.GetComponent<Rigidbody>();
        textToFade.color = new Color(textToFade.color.r, textToFade.color.g, textToFade.color.b, 0);
        tapToStartText.SetActive(true);
        gameMechanics.SetActive(false);
        Car.SetActive(true);
        restartGame.SetActive(false);
        Car.transform.position = new Vector3(0, 0, 0);
        rb.MovePosition(Car.transform.position);
        
    }

    void Update()
    {
        Car.transform.position = new Vector3(0, 0, 0);
        rb.MovePosition(Car.transform.position);
        if (Input.GetMouseButtonDown(0))  
        {
            StartGameAction();  
        }
    }

    void StartGameAction()
    {
        
        tapToStartText.SetActive(false);  
        gameMechanics.SetActive(true);
        Car.SetActive(true);
        isPlay = true;
    }
}
