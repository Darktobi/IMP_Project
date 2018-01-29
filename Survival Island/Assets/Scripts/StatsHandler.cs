using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsHandler : MonoBehaviour {

    public Text strength;
    public Text constitution;
    public Text agility;
    public Text wisdom;

    //PlayerData playerData;
    public PlayerDatas playerData;

	
	// Update is called once per frame
	void Update () {
        strength.text = playerData.str.ToString();
        constitution.text = playerData.con.ToString();
        agility.text = playerData.agi.ToString();
        wisdom.text = playerData.wis.ToString();
    }
}
