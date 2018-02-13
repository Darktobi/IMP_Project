using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    [SerializeField]
    private string itemName;

    public List<Item> neededMaterials;

    private int count;

	void Start () {
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
