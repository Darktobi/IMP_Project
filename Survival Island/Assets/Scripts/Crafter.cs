using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Crafter : MonoBehaviour {

    [SerializeField]
    private Player player;

    private int neededAp = 1;
    [SerializeField]
    private List<Item> craftableItems;    
    public PopUpWindowManager popUpWindow;

    private void Awake()
    {
        craftableItems = FindObjectsOfType<Item>().ToList();
        craftableItems = craftableItems.OrderBy(i => i.getItemName()).ToList();
    }

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
        if (player.getAp() >= neededAp)
        {
            bool canCraft = true;

            List<Item> subtractedMaterials = new List<Item>();

            foreach (Item neededMaterial in item.neededMaterials)
            {
                if (player.subItem(neededMaterial))
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
                player.setAp(-neededAp);
                player.addItem(item);
                if(item.name == "Boat")
                {
                    player.WonGame();

                }
                else
                {
                    player.save();
                }
            }
        }

        else
        {
            string title = "Nicht möglich!";
            string description = "Leider nicht genug Aktionspunkte zur Verfügung!";
            popUpWindow.createNotificationWindow(title, description);
        }
        
        
    }

    private void addSubtractedMaterials(List<Item> materials)
    {
        foreach(Item mat in materials)
        {
            player.addItem(mat);
        }
    }

    private void addRow(Button button2, Transform parentPanel, Item item)
    {
        Button btnPanel = Instantiate(button2);
        btnPanel.name = item.name;
        if (item.GetComponent<Image>().sprite != null)
        {
            btnPanel.transform.GetChild(1).GetComponent<Image>().sprite = item.GetComponent<Image>().sprite;
            btnPanel.transform.GetChild(1).GetComponent<Image>().preserveAspect = true;
        }
        else
            btnPanel.transform.GetChild(1).GetComponent<Image>().enabled = false;


        //Description Text
        string text = "Willst du Das Item wirklich Herstellen?\nEs werden";
        Dictionary<Item, int> neededMats = new Dictionary<Item, int>();
        foreach (Item neededMaterial in item.getNeededMaterials())
        {
            if (neededMats.ContainsKey(neededMaterial))
            {
                neededMats[neededMaterial] += 1;
            }
            else
            {
                neededMats.Add(neededMaterial, 1);
            }           

        }
        foreach (KeyValuePair<Item, int> mat in neededMats)
        {
            text += "\n" + mat.Key.getItemName() + " x" + mat.Value;
        }

        text += "\nbenötigt.";

        string type = "Crafting";
         
        btnPanel.onClick.AddListener(() => popUpWindow.createDescriptionWindow(btnPanel, item, text, type));
        btnPanel.GetComponentInChildren<Text>().text = item.getItemName() + " ( " + neededAp + "AP)";
        btnPanel.transform.SetParent(parentPanel.GetComponent<Transform>());
        btnPanel.transform.localScale = new Vector3(1, 1, 1);

       
    }
    

}
