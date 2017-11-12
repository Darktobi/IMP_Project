using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationHandler : MonoBehaviour {

    public Image btnLocation;
    public GameObject btnActions;

    private Color32 white;
    private Color32 darkened;

    private void Start()
    {
        white = new Color32(255, 255, 255, 255);
        darkened = new Color32(191, 191, 191, 255);
        btnLocation.color = white;
    }

    public void OpenLocation()
    {
        btnActions.SetActive(false);
        btnLocation.color = white;
    }

    public void OpenActions()
    {
        btnActions.SetActive(true);
        btnLocation.color = darkened;
    }
}
