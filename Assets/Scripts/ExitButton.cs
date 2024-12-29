using UnityEngine;
using UnityEngine.SceneManagement; // Eðer sahne deðiþtirme gerekiyorsa
using UnityEngine.UI;
using static UnityEngine.Application; // Uygulamayý kapatmak için

public class ExitButton : MonoBehaviour
{
    public Button buton;


    public void OnClickButton()
    {

        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
  
}
