using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsHandler : MonoBehaviour {

    public Text strength;
    public Text constitution;
    public Text finesse;
    public Text wisdom;

    // Use this for initialization
    void Start () {
		if (PlayerPrefs.GetInt("New Game")==1)
        {
            PlayerPrefs.SetInt("STR", 5);
            PlayerPrefs.SetInt("CON", 5);
            PlayerPrefs.SetInt("FIN", 5);
            PlayerPrefs.SetInt("WIS", 5);
        }
	}
	
	// Update is called once per frame
	void Update () {
        strength.text = PlayerPrefs.GetInt("STR").ToString();
        constitution.text = PlayerPrefs.GetInt("CON").ToString();
        finesse.text = PlayerPrefs.GetInt("FIN").ToString();
        wisdom.text = PlayerPrefs.GetInt("WIS").ToString();
    }
}
