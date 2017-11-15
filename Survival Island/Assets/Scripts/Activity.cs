using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour {

    public int activityPoints;
    public int duration;

    public bool isAvaiable;
    

	// Use this for initialization
	void Start () {
	}

    public void setAvaiable()
    {

        isAvaiable = true;
    }
	

}
