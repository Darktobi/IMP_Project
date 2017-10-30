using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToMap : MonoBehaviour {

    public GameObject deactivatedPanel;
    public GameObject deactivatedPanel1;
    public GameObject deactivatedPanel2;

    private void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {

        deactivatedPanel.SetActive(false);
        deactivatedPanel1.SetActive(false);
        deactivatedPanel2.SetActive(false);
        //deactivatedPanel3.SetActive(false);
        Debug.Log("Ausgelöst!");


    }
}
