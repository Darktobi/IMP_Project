using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject settingsMenu;
    public Slider soundSlider;
    public Slider musicSlider;
    public Text soundValue;
    public Text musicValue;

    public void Start()
    {
        //Menu


        //Sound
        soundValue.text = soundSlider.value.ToString();
        musicValue.text = musicSlider.value.ToString();

        soundSlider.onValueChanged.AddListener(delegate {

            ValueChangeCheck();

        });

        musicSlider.onValueChanged.AddListener(delegate {

            ValueChangeCheck();

        });
    }

    private void ValueChangeCheck()
    {
        soundValue.text = soundSlider.value.ToString();
        musicValue.text = musicSlider.value.ToString();
        AudioListener.volume = soundSlider.value / 100;
        //Debug.Log(AudioListener.volume);
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
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs deleted!");
        SceneManager.LoadScene("SurvivalIsland");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("SurvivalIsland");
    }

    public void QuitGame()
    {
        Application.Quit();

    }


}
