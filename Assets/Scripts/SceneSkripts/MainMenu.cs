using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UIElements;
public class MainMenu : MonoBehaviour
{


    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); // Wczytanie sceny I
       

    }
    public void QuitGame()
    {       
        Application.Quit(); //Wyjście z aplikacji 
       
    }
    
}
