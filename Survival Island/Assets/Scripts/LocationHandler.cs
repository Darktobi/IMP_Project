using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationHandler : MonoBehaviour {

    [SerializeField]
    private List<Location> locations;

    private Color32 white;
    private Color32 darkened;

    public Image btnLocation;
    public GameObject btnActions;
    public GameObject locationList;
    public GameObject actionList;

    private void Start()
    {
        white = new Color32(255, 255, 255, 255);
        darkened = new Color32(191, 191, 191, 255);
        btnLocation.color = white;
    }

    public void openLocation()
    {
        btnActions.SetActive(false);
        actionList.SetActive(false);
        btnLocation.color = white;

        foreach(Location location in locations)
        {
            if(location.checkActive())
            {
                location.gameObject.SetActive(true);
                locationList.SetActive(true);
            }
            else
            {
                location.gameObject.SetActive(false);
                locationList.SetActive(false);
            }
        }
    }

    public void openActions(Location location)
    {
        btnActions.SetActive(true);
        actionList.SetActive(true);
        btnLocation.color = darkened;
        location.getActivities();
    }
}
