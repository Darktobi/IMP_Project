using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject settingsMenu;
    

    public void Start()
    {
        mainMenu.SetActive(true);
    }

    

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void StartNewGame()
    {
        

        //Sorgt dafür, dass das Spiel als ein neues geladen wird
        PlayerPrefs.SetInt("New Game", 1);

        //Debug.Log("PlayerPrefs deleted!");
        SceneManager.LoadScene("SurvivalIsland");
    }

    public void ContinueGame()
    {
        //Sorgt dafür, dass das Spiel fortgeführt wird
        PlayerPrefs.SetInt("New Game", 0);
        SceneManager.LoadScene("SurvivalIsland");
    }

    public void QuitGame()
    {
        Application.Quit();

    }


}
