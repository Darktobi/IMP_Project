using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour {

    public int activityPoints;
    public float workingTime;
    public string activityName;
    public List<Item> collectableMaterials;

    public Tool neededTool;
    public Location currentLocation;

    public bool isAvaiable;
    
    public void setAvaiable()
    {
        isAvaiable = true;
    }

    public void setCurrentLocation(Location currentLocation)
    {
        this.currentLocation = currentLocation;
    }
	

}
