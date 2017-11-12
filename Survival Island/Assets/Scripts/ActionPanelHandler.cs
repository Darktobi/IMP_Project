using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPanelHandler : MonoBehaviour {

    public GameObject characterInfoPanel;
    public GameObject locationsPanel;
    public GameObject inventoryPanel;
    public GameObject craftingPanel;

    private Color32 white;
    private Color32 darkened;

    public Image locationsBtn;
    public Image inventoryBtn;
    public Image craftingBtn;

    // Use this for initialization
    private void Start () {
        DefaultUI();

        white = new Color32(255, 255, 255, 255);
        darkened = new Color32(191, 191, 191, 255);

        locationsBtn.color = white;
        inventoryBtn.color = white;
        craftingBtn.color = white;
    }

    public void DefaultUI()
    {
        characterInfoPanel.SetActive(true);
        locationsPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        craftingPanel.SetActive(false);

        locationsBtn.color = white;
        inventoryBtn.color = white;
        craftingBtn.color = white;
    }

    public void OpenInventory()
    {
        characterInfoPanel.SetActive(false);
        locationsPanel.SetActive(false);
        inventoryPanel.SetActive(true);
        craftingPanel.SetActive(false);

        craftingBtn.color = darkened;
        locationsBtn.color = darkened;
        inventoryBtn.color = white;
    }

    public void OpenLocations()
    {
        characterInfoPanel.SetActive(false);
        locationsPanel.SetActive(true);
        inventoryPanel.SetActive(false);
        craftingPanel.SetActive(false);

        craftingBtn.color = darkened;
        inventoryBtn.color = darkened;
        locationsBtn.color = white;
    }

    public void OpenCrafting()
    {
        characterInfoPanel.SetActive(false);
        locationsPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        craftingPanel.SetActive(true);

        inventoryBtn.color = darkened;
        locationsBtn.color = darkened;
        craftingBtn.color = white;
    }


}
