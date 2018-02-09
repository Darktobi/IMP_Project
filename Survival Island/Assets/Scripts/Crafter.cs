using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafter : MonoBehaviour {

    public List<Item> craftableItems;
    public Inventory inventory;
    public Player player;
    public PopUpWindowManager popUpWindow;
    
    public void showItems(System.Type type, Button button, Transform parentPanel)
    {
        foreach (Item item in craftableItems)
        {
            if (item.GetType().Equals(type))
            {
                addRow(button, parentPanel, item);
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
                string title = "Nicht möglich!";
                string description = "Leider nicht genügend Materialien vorhanden!";
                popUpWindow.createNotificationWindow(title, description);

                canCraft = false;
                addSubtractedMaterials(subtractedMaterials);
                break;
            }
            
        }

        if (canCraft)
        {
            inventory.addItem(item);
            player.save();
        }
        
    }

    private void addSubtractedMaterials(List<Item> materials)
    {
        foreach(Item mat in materials)
        {
            inventory.addItem(mat);
        }
    }

    private void addRow(Button button2, Transform parentPanel, Item item)
    {
        Button btnPanel = Instantiate(button2);

        //Description Text
        string text = "Willst du \n\n"+item.name+" \n\nHerstellen?\nEs werden\n"+ item.neededMaterials.ToString() + "\nbenötigt.";
        string type = "Crafting";
         
        btnPanel.onClick.AddListener(() => popUpWindow.createDescriptionWindow(btnPanel, item, text, type));
        btnPanel.GetComponentInChildren<Text>().text = item.name;
        btnPanel.transform.SetParent(parentPanel.GetComponent<Transform>());

       
    }
    

}
