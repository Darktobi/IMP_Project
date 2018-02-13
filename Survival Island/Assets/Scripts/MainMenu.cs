using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject settingsMenu;
    public Transform canvas;
    public GameObject popUpWindow;
    
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
        PlayerPrefs.SetInt("New Game", 1);
        SceneManager.LoadScene("SurvivalIsland");
    }

    public void ContinueGame()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {

            SceneManager.LoadScene("SurvivalIsland");
            PlayerPrefs.SetInt("New Game", 0);

        } else
        {
            GameObject window = Instantiate(popUpWindow);
            window.transform.SetParent(canvas, false);

            window.transform.Find("Image/Title_Panel/Title_Text").GetComponent<Text>().text = "Nicht möglich!";
            window.transform.Find("Image/Upper_Panel/Event_Text").GetComponent<Text>().text = "Es wurde kein Spielstand gefunden.\nBitte beginne ein neues Spiel!";

            Button okbutton = window.transform.Find("Image/Down_Panel/Button").GetComponent<Button>();
            okbutton.onClick.AddListener(() => Destroy(window));

        }
    }

    public void QuitGame()
    {
        Application.Quit();

    }


}
