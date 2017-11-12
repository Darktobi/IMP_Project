﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingHandler : MonoBehaviour {

    public Image btnEQ;
    public Image btnFood;
    public Image btnTools;
    private Color32 white;
    private Color32 darkened;

    public GameObject foodList;
    public GameObject eqList;
    public GameObject toolsList;


    public void Start()
    {
        white = new Color32(255, 255, 255, 255);
        darkened = new Color32(191, 191, 191, 255);

        btnEQ.color = white;
        btnFood.color = darkened;
        btnTools.color = darkened;

    }

    


    public void OpenEQ()
    {
        btnFood.color = darkened;
        btnTools.color = darkened;
        btnEQ.color = white;

        eqList.SetActive(true);
        toolsList.SetActive(false);
        foodList.SetActive(false);
    }

    public void OpenFood()
    {
        btnEQ.color = darkened;
        btnTools.color = darkened;
        btnFood.color = white;

        foodList.SetActive(true);
        eqList.SetActive(false);
        toolsList.SetActive(false);
    }

    public void OpenTools()
    {
        btnEQ.color = darkened;
        btnFood.color = darkened;
        btnTools.color = white;

        toolsList.SetActive(true);
        eqList.SetActive(false);
        foodList.SetActive(false);
    }





}