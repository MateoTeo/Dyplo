using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    GameObject pauseMenuUI;
    bool CzyJestZatrzymana;
    GameObject canvas;

    private void Start()
    {
        canvas = GameObject.Find("GUI");
        pauseMenuUI = GameObject.Find("PauseMenuMain");
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) // Sprawdzenie czy gra została zatrzymana
        {
            CzyJestZatrzymana = !CzyJestZatrzymana;      
        }     
    }

    private void OnGUI()
    {
        if (true)
        {
            if (CzyJestZatrzymana) // Uruchomienie / wyłączenie pause menu 
            {

                ActivateMenu();
                canvas.SetActive(false);
            }

            else
            {
                DeactivateMenu();
                canvas.SetActive(true);
            }
        }
        
    }
    void ActivateMenu()
    {
        pauseMenuUI.SetActive(true); //Pause menu zostaje pokazane na pierwszym planie
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
    }

    public void DeactivateMenu() //Pause menu zostaje schowane
    {
        pauseMenuUI.SetActive(false);
        CzyJestZatrzymana = false;
        Time.timeScale = 1f;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu"); // Ladowanie sceny Menu
    }

    public void Back() // powrót 
    {
        pauseMenuUI.SetActive(false);
    }  
}
   