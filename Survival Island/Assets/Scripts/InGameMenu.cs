using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {

    public Player player;
    public GameObject gameMenu;
    public GameObject gameMenuMain;
    public GameObject soundMenu;
    public GameObject menuBtn;

	private void Start () {

        gameMenu.SetActive(false);
}
	
    public void OpenMenu()
    {
        gameMenu.SetActive(true);
        gameMenuMain.SetActive(true);
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
        player.save();
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenSettings()
    {
        soundMenu.SetActive(true);
        gameMenuMain.SetActive(false);

    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
