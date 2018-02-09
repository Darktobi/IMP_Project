using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tool : Item {

    public int MaxStability;
    private int currentStability;

	void Start () {
        currentStability = MaxStability;
	}
	
    public void subStability()
    {
        currentStability--;
    }

    public int getCurrentStability()
    {
        return currentStability;
    }

    public void resetStability()
    {
        currentStability = MaxStability;
    } 
}
