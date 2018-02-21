using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour {

    [SerializeField]
    private Inventory inventory;

    public Image btnEQ;
    public Image btnFood;
    public Image btnTools;
    public Image btnMats;
    private Color32 white;
    private Color32 darkened;

    public GameObject foodList;
    public GameObject eqList;
    public GameObject toolsList;
    public GameObject matsList;

    public Button itemButton;

    public Transform eqPanel;
    public Transform foodPanel;
    public Transform toolsPanel;
    public Transform matsPanel;

    private void Start()
    {
        white = new Color32(255, 255, 255, 255);
        darkened = new Color32(191, 191, 191, 255);

        openEQ();
    }

    private void OnEnable()
    {
        openEQ();
    }

    public void openEQ()
    {
        darkenAll();
        btnEQ.color = white;

        closeAll();
        eqList.SetActive(true);
        inventory.clearPage(eqPanel);
        inventory.showItems(typeof(Equipment), itemButton, eqPanel);
    }

    public void openFood()
    {
        darkenAll();
        btnFood.color = white;

        closeAll();
        foodList.SetActive(true);
        inventory.clearPage(foodPanel);
        inventory.showItems(typeof(Food), itemButton, foodPanel);
    }

    public void openTools()
    {
        darkenAll();
        btnTools.color = white;
        closeAll();
        toolsList.SetActive(true);
        inventory.clearPage(toolsPanel);
        inventory.showItems(typeof(Tool), itemButton, toolsPanel);
    }

    public void openMaterials()
    {
        darkenAll();
        btnMats.color = white;
        closeAll();
        matsList.SetActive(true);
        inventory.clearPage(matsPanel);
        inventory.showItems(typeof(Mat), itemButton, matsPanel);
    }

    private void darkenAll()
    {
        btnEQ.color = darkened;
        btnFood.color = darkened;
        btnTools.color = darkened;
        btnMats.color = darkened;
    }

    private void closeAll()
    {
        eqList.SetActive(false);
        toolsList.SetActive(false);
        foodList.SetActive(false);
        matsList.SetActive(false);
    }

    private void loadLists()
    {
        inventory.showItems(typeof(Tool),  itemButton, toolsPanel);
        inventory.showItems(typeof(Food),  itemButton, foodPanel);
        inventory.showItems(typeof(Equipment),  itemButton, eqPanel);
        inventory.showItems(typeof(Mat),  itemButton, matsPanel);
    }





}
