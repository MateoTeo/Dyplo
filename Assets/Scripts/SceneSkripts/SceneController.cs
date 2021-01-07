using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;        
    }
    public void Ponow()
    {
        SceneManager.LoadScene("SampleScene"); // Wczytanie sceny I
    }
    public void Exit()
    {
        Application.Quit(); //Wyjście z aplikacji 
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
