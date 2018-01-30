﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    //[SerializeField]
    public List<Item> items;
    private int num;
    //public PlayerDatas playerDatas;

    // Use this for initialization
    void Start()
    {
        num = 0;
    }


    public void showItems(System.Type type, Transform rowPanel, Button button, Transform parentPanel)
    {
        foreach(Item item in items)
        {
            if (item.GetType().Equals(type))
            {
                addSlot(item.name, button, rowPanel, parentPanel, item);
                
            }
        }
    }

    public void addItem(Item item)
    {
        if (items.Contains(item))
        {
            
            item.addCount();
            Debug.Log(item.name + " +1");
        }

        else
        {
            items.Add(item);
            item.addCount();
            Debug.Log(item.name + " added");
        }

        

    }

    public bool subItem(Item item)
    {
        Debug.Log(item.name);
        if (items.Contains(item))
        {
            item.subCount();

            if (item.getCount() == 0)
            {
                items.Remove(item);
            }
            return true;
        }

        return false;
        
        
    }

    private void addSlot(string name, Button button2, Transform rowPanel, Transform parentPanel, Item item)
    {

        var btnPanel = Instantiate(rowPanel);
        btnPanel.transform.SetParent(parentPanel);


        var btnItem = Instantiate(button2);
        btnItem.transform.SetParent(btnPanel);
        btnItem.GetComponentInChildren<Text>().text = name +" x"+ item.getCount();

        num++;
        

    }

    public void clearPage(Transform parentPanel)
    {

        //Debug.Log("Cleared!");
        foreach (Transform child in parentPanel) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
