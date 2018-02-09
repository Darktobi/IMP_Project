using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    public string name;
    public List<Item> neededMaterials;

    private int count;

	void Start () {
        count = 0;
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
