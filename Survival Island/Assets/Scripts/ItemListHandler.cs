using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListHandler : MonoBehaviour {

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
            
            if (itemCount > j)
            {
                var btnPanel = Instantiate(rowPanel);
                btnPanel.transform.SetParent(parent);

                

                
                for (int i = 0; i < 6; i++)
                {
               
                    var btnItem = Instantiate(itemSlot);

                    btnItem.transform.SetParent(btnPanel);
                    //Instantiate(items[i]);

                    
                }
                
            }

            j += 5;
            
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
        /*for (int i = 0; i < 5; i++)
        {

        }*/
        /*
        for (int i = 0; i < items.Length; i++)
        {
            Instantiate(items[i]);
        }*/
    }
}
