using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour {

    public Image btnEQ;
    public Image btnFood;
    public Image btnTools;
    private Color32 white;
    private Color32 darkened;


    public void Start()
    {
        white = new Color32(255, 255, 255, 255);
        darkened = new Color32(191, 191, 191, 255);
    }


    public void OpenEQ()
    {
        btnFood.color = darkened;
        btnTools.color = darkened;
        btnEQ.color = white;
    }

    public void OpenFood()
    {
        btnEQ.color = darkened;
        btnTools.color = darkened;
        btnFood.color = white;
    }

    public void OpenTools()
    {
        btnEQ.color = darkened;
        btnFood.color = darkened;
        btnTools.color = white;
    }



}
