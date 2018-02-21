using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour {

    [SerializeField]
    private int neededAP;
    [SerializeField]
    private float workingTime;
    [SerializeField]
    private string activityName;
    [SerializeField]
    private Tool neededTool;
    [SerializeField]
    private List<Item> collectableMaterials;

    public bool isAvaiable;
    public Location currentLocation;
    
    public int getNeededAP()
    {
        return neededAP;
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

    public List<Item> getCollectableMaterials()
    {
        return collectableMaterials;
    }
	

}
