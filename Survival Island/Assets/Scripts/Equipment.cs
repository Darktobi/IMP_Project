using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Equipment : Item {


    public int str, con, agi, wis;
    public enum Types{ Weapon, Head, Breast, Hands, Legs, Feet };

    public Types type;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
