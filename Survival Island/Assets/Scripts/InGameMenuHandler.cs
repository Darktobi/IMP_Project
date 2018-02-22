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
        gameMenu.SetActive(true);
        gameMenuMain.SetActive(true);
        menuBtn.SetActive(false);
        soundMenu.SetActive(false);
    }

    public void closeMenu()
    {
        gameMenu.SetActive(false);
        menuBtn.SetActive(true);
    }

    public void loadMainMenu()
    {
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
