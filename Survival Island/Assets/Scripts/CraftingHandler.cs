using UnityEngine;
using UnityEngine.UI;

public class CraftingHandler : MonoBehaviour {

    private Color32 white;
    private Color32 darkened;

    public Image btnEQ;
    public Image btnFood;
    public Image btnTools;
    public GameObject foodList;
    public GameObject eqList;
    public GameObject toolsList;
    public Button itemButton;
    public Transform eqPanel;
    public Transform foodPanel;
    public Transform toolsPanel;

    public Crafter crafter;

    private void Start()
    {
        white = new Color32(255, 255, 255, 255);
        darkened = new Color32(191, 191, 191, 255);

        btnEQ.color = white;
        btnFood.color = darkened;
        btnTools.color = darkened;

        openEQ();
        loadLists();
    }

    public void openEQ()
    {
        btnFood.color = darkened;
        btnTools.color = darkened;
        btnEQ.color = white;

        eqList.SetActive(true);
        toolsList.SetActive(false);
        foodList.SetActive(false);
    }

    public void openFood()
    {
        btnEQ.color = darkened;
        btnTools.color = darkened;
        btnFood.color = white;

        foodList.SetActive(true);
        eqList.SetActive(false);
        toolsList.SetActive(false);
    }

    public void openTools()
    {
        btnEQ.color = darkened;
        btnFood.color = darkened;
        btnTools.color = white;

        toolsList.SetActive(true);
        eqList.SetActive(false);
        foodList.SetActive(false);
    }

    private void loadLists()
    {
        crafter.showItems(typeof(Tool), itemButton, toolsPanel);
        crafter.showItems(typeof(Food), itemButton, foodPanel);
        crafter.showItems(typeof(Equipment), itemButton, eqPanel);
    }





}
