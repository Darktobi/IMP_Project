using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour {

    [SerializeField]
    private int ap;
    [SerializeField]
    private float workingTime;
    [SerializeField]
    private string activityName;
    [SerializeField]
    private Tool neededTool;

    public bool isAvaiable;
    public List<Item> collectableMaterials;
    public Location currentLocation { get; private set; }
    
    
    public int getAp()
    {
        return ap;
    }

    public float getWorkingTime()
    {
        return workingTime;
    }

    public string getActivityName()
    {
        return activityName;
    }

    public Tool getNeededTool()
    {
        return neededTool;
    }

    public void setCurrentLocation(Location currentLocation)
    {
        this.currentLocation = currentLocation;
    }
	

}
