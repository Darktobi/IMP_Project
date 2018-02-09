using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour {

    [SerializeField]
    private List<Activity> activities;
    [SerializeField]
    private string locationName;

    private bool isActive;

    public List<Item> collectableMaterials;
    public List<GameEvent> possibleEvents;

    public string getLocationName()
    {
        return locationName;
    }

    public bool checkActive()
    {
        // Location is only active if there is at least one avaiable activity
        isActive = checkForActivities();

        //Disable all activities
        foreach(Activity activity in activities)
        {
            activity.gameObject.SetActive(false);
        }

        return isActive;
    }

    public void getActivities()
    {
        //activate only activites avaiable for specific location
        foreach(Activity activity in activities)
        {
            if (activity.isAvaiable)
            {
                activity.setCurrentLocation(this);
                activity.gameObject.SetActive(true);
            } 
        }
    }

    private bool checkForActivities()
    {
        bool hasActivities = false;

        foreach(Activity activitiy in activities)
        {
            if (activitiy.isAvaiable)
            {
                hasActivities = true;
                break;
            }
        }

        return hasActivities;
    }
}
