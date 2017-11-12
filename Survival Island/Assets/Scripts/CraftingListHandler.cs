using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingListHandler : MonoBehaviour {

    //public GameObject itemRow;
    //private Button[] items;
    public Button itemSlot;
    public Transform parent;
    public Transform rowPanel;
    public int itemCount;
    

    // Use this for initialization
    void Start () {

        
        for (int j = 0; j<itemCount; j++)
        {

            AddRow();
            
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		/*for(int i = 0; i<items.Length; i++)
        {

        }*/
	}

    private void AddRow()
    {
        var btnPanel = Instantiate(rowPanel);
        btnPanel.transform.SetParent(parent);
    }
}
