using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsHandler : MonoBehaviour {

    public Text strength;
    public Text constitution;
    public Text agility;
    public Text wisdom;
    public Player player;

	void Update () {
        strength.text = player.getStr().ToString();
        constitution.text = player.getCon().ToString();
        agility.text = player.getAgi().ToString();
        wisdom.text = player.getWis().ToString();
    }
}
