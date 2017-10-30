using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanels : MonoBehaviour {

    public int numberPanels;
    public GameObject button;

    // Use this for initialization
    void Start () {
		for (int i = 0; i<numberPanels; i++)
        {
            GameObject newButton = Instantiate(button) as GameObject;
            newButton.transform.SetParent(gameObject.transform, false);
        }
	}

    


}
