using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuHandler : MonoBehaviour {

    [SerializeField]
    private Player player;

    public GameObject gameMenu;
    public GameObject gameMenuMain;
    public GameObject soundMenu;
    public GameObject menuBtn;

	private void Start ()
    {
        gameMenu.SetActive(false);
    }
	
    public void openMenu()
    {
        Time.timeScale = 0;

        gameMenu.SetActive(true);
        gameMenuMain.SetActive(true);
        menuBtn.SetActive(false);
        soundMenu.SetActive(false);
    }

    public void closeMenu()
    {
        Time.timeScale = 1;

        gameMenu.SetActive(false);
        menuBtn.SetActive(true);
    }

    public void loadMainMenu()
    {
        Time.timeScale = 1;

        player.save();
        SceneManager.LoadScene("MainMenu");
    }

    public void openSettings()
    {
        soundMenu.SetActive(true);
        gameMenuMain.SetActive(false);
    }

    public void quitGame()
    {
        player.save();
        Application.Quit();
    }


}
