using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonPressed : MonoBehaviour {

    //public Button yourButton;
    public GameObject activatedPanel;
    public GameObject deactivatedPanel1;
    public GameObject deactivatedPanel2;
    //public GameObject deactivatedPanel3;

   // public GameObject scrollbars;

    private void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        
        activatedPanel.SetActive(true);
        deactivatedPanel1.SetActive(false);
        deactivatedPanel2.SetActive(false);
        //deactivatedPanel3.SetActive(false);
        Debug.Log("Ausgelöst!");

        
    }
}
