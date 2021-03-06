﻿using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    [SerializeField]
    private string itemName;
    [SerializeField]
    private List<Item> neededMaterials;

    private int count;

    public bool isNotCraftable;

	private void Start ()
    {
        count = 0;
	}

    public string getItemName()
    {
        return itemName;
    }

    public List<Item> getNeededMaterials()
    {
        return neededMaterials;
    }

    public void addCount()
    {
        count++;
    }

    public void subCount()
    {
        count--;
    }

    public int getCount()
    {
        return count;
    }
}
