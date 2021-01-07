using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] 
    GameObject pauseMenuUI;
    [SerializeField] 
    GameObject settingsMenuUI;
    bool CzyJestZatrzymana;


    private void Update()
    {
         
        if (Input.GetKeyDown(KeyCode.Escape)) // Sprawdzenie czy gra została zatrzymana
        {
            CzyJestZatrzymana = !CzyJestZatrzymana;
        }

        if (CzyJestZatrzymana) // Uruchomienie / wyłączenie pause menu 
        {
            ActivateMenu(); 
        }

        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu() 
    {
        pauseMenuUI.SetActive(true); //Pause menu zostaje pokazane na pierwszym planie
    }

    public void DeactivateMenu() //Pause menu zostaje schowane
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        CzyJestZatrzymana = false;
    }

    public void OpenSettings() // Otwieranie ustawień
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(true);

    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu"); // Ladowanie sceny Menu
    }

    public void Back() // powrót 
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }
}
