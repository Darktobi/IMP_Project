using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafter : MonoBehaviour {

    public List<Item> craftableItems;
    public Inventory inventory;

    //Darstellung
    
    public int itemCount;
    


    // Use this for initialization
    private void Start () {
        Debug.Log(gameObject.name + ", child of " + transform.parent.name);
    }

    public void showItems(System.Type type, Button button, Transform parentPanel)
    {
        foreach (Item item in craftableItems)
        {

            if (item.GetType().Equals(type))
            {
                AddRow(item.name, button, parentPanel, item);
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

    private void AddRow(string name, Button button2, Transform parentPanel, Item item)
    {

        Debug.Log("Row Added: "+ name + " in " + item);
        Button btnPanel = Instantiate(button2);
        btnPanel.onClick.AddListener(()=> craft(item));
        btnPanel.GetComponentInChildren<Text>().text = name;
        btnPanel.transform.SetParent(parentPanel.GetComponent<Transform>());

       
    }
    

}
