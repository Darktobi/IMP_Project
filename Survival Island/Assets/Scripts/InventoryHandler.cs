using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour {

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

    public Inventory inventory;

    public Button itemButton;
    public Transform eqPanel;
    public Transform foodPanel;
    public Transform toolsPanel;
    public Transform matsPanel;
    //public Transform rowPanel;

    public void Start()
    {
        white = new Color32(255, 255, 255, 255);
        darkened = new Color32(191, 191, 191, 255);

        OpenEQ();
    }

    private void OnEnable()
    {
        OpenEQ();
    }

    public void OpenEQ()
    {
        DarkenAll();
        btnEQ.color = white;

        CloseAll();
        eqList.SetActive(true);
        inventory.clearPage(eqPanel);
        inventory.showItems(typeof(Equipment), /*rowPanel,*/ itemButton, eqPanel);
    }

    public void OpenFood()
    {
        DarkenAll();
        btnFood.color = white;

        CloseAll();
        foodList.SetActive(true);
        inventory.clearPage(foodPanel);
        inventory.showItems(typeof(Food), /*rowPanel,*/ itemButton, foodPanel);
    }

    public void OpenTools()
    {
        DarkenAll();
        btnTools.color = white;
        CloseAll();
        toolsList.SetActive(true);
        inventory.clearPage(toolsPanel);
        inventory.showItems(typeof(Tool), /*rowPanel,*/ itemButton, toolsPanel);
    }

    public void OpenMaterials()
    {
        DarkenAll();
        btnMats.color = white;
        CloseAll();
        matsList.SetActive(true);
        inventory.clearPage(matsPanel);
        inventory.showItems(typeof(Mat), /*rowPanel,*/ itemButton, matsPanel);
    }

    private void DarkenAll()
    {
        btnEQ.color = darkened;
        btnFood.color = darkened;
        btnTools.color = darkened;
        btnMats.color = darkened;
    }

    private void CloseAll()
    {
        eqList.SetActive(false);
        toolsList.SetActive(false);
        foodList.SetActive(false);
        matsList.SetActive(false);
    }

    private void loadLists()
    {
        inventory.showItems(typeof(Tool), /*rowPanel,*/ itemButton, toolsPanel);
        inventory.showItems(typeof(Food), /*rowPanel,*/ itemButton, foodPanel);
        inventory.showItems(typeof(Equipment), /*rowPanel,*/ itemButton, eqPanel);
        inventory.showItems(typeof(Mat), /*rowPanel,*/ itemButton, matsPanel);
    }





}
