using UnityEngine;
using UnityEngine.SceneManagement; // E�er sahne de�i�tirme gerekiyorsa
using UnityEngine.UI;
using static UnityEngine.Application; // Uygulamay� kapatmak i�in

public class ExitButton : MonoBehaviour
{
    public Button buton;


    public void OnClickButton()
    {

        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
  
}
