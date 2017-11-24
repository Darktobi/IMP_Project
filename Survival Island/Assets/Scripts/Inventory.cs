using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {


    public List<Item> items;

    // Use this for initialization
    void Start()
    {
        
    }

    public void showItems(System.Type type)
    {
        foreach(Item item in items)
        {
            if (item.GetType().Equals(type))
            {
                Debug.Log(item.name + " x" + item.getCount());
            }
        }
    }

    public void addItem(Item item)
    {
        if (items.Contains(item))
        {
            
            item.addCount();
        }

        else
        {
            items.Add(item);
            item.addCount();
        }
    }

    public void subItem(Item item)
    {
        if (items.Contains(item))
        {
            item.subCount();

            if(item.getCount() == 0)
            {
                items.Remove(item);
            }
        }
    }
}
