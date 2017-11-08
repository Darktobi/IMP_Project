using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {

    public GameObject gameMenu;
    public GameObject soundMenu;
    public GameObject menuBtn;

	// Use this for initialization
	void Start () {

        gameMenu.SetActive(false);
}
	
	// Update is called once per frame
	

    public void OpenMenu()
    {
        gameMenu.SetActive(true);
        menuBtn.SetActive(false);
        soundMenu.SetActive(false);
    }

    public void CloseMenu()
    {
        gameMenu.SetActive(false);
        menuBtn.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenSettings()
    {
        soundMenu.SetActive(true);
        gameMenu.SetActive(false);

    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
