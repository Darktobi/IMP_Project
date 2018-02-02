using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tool : Item {

    public int MaxStability;
    private int currentStability;

	// Use this for initialization
	void Start () {
        currentStability = MaxStability;
	}
	
	// Update is called once per frame
	void Update () {
		
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
