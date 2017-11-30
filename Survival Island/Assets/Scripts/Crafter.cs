using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : MonoBehaviour {

    public List<Item> craftableItems;
    public Inventory inventory;

    // Use this for initialization
    void Start () {
		
	}

    public void showItems(System.Type type)
    {
        foreach (Item item in craftableItems)
        {
            if (item.GetType().Equals(type))
            {
                Debug.Log(item.name);
            }
        }
    }

    public void craft(Item item)
    {
        bool canCraft = true;

        List<Item> subtractedMaterials = new List<Item>();

        foreach(Item neededMaterial in item.neededMaterials)
        {
            if (inventory.subItem(neededMaterial))
            {
                subtractedMaterials.Add(neededMaterial);
            }

            else
            {
                Debug.Log("Leider nicht genug Materialien");
                canCraft = false;
                addSubtractedMaterials(subtractedMaterials);
                break;
            }
            
        }

        if (canCraft)
        {
            inventory.addItem(item);
        }
        
    }

    private void addSubtractedMaterials(List<Item> materials)
    {
        foreach(Item mat in materials)
        {
            inventory.addItem(mat);
        }
    }

}
