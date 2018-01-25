using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour {


    public string title;
    public string description;

    public bool changeItems;
    public bool hasDescision;

    public enum DangerLevel { None, Medium, High };

    public DangerLevel dangerLevel;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
