using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject settingsMenu;
    public Transform canvas;
    public GameObject popUpWindow;
    
    private void Start()
    {
        mainMenu.SetActive(true);
    }

    public void openSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void openMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void initiateNewGame()
    {
        GameObject window = Instantiate(popUpWindow);
        window.transform.SetParent(canvas, false);

        window.transform.Find("Image/Title_Panel/Title_Text").GetComponent<Text>().text = "Island Survivor";
        window.transform.Find("Image/Upper_Panel/Event_Text").GetComponent<Text>().text = "Du warst mit Freunden auf einem Kreuzfahrtschiff unterwegs, als ein schwerer Sturm aufgezogen ist.\nNachdem du vom Deck gespühlt wurdest, bist du am Strand einer Insel aufgewacht,\n\nDeine aufgabe ist es zu überleben, bis vielleicht hilfe an kommt, oder du von der Insel flüchten kannst!";

        Button okbutton = window.transform.Find("Image/Down_Panel/Button").GetComponent<Button>();
        okbutton.onClick.AddListener(() => startNewGame());

    }

    private void startNewGame()
    {
        PlayerPrefs.SetInt("New Game", 1);
        File.Delete(Application.persistentDataPath + "/playerInfo.dat");
        SceneManager.LoadScene("SurvivalIsland");
    }

    public void continueGame()
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

    public void quitGame()
    {
        Application.Quit();

    }


    


}
