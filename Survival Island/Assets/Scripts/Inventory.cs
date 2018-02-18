using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    [SerializeField]
    private Player player;

    public List<Item> items;
    public PopUpWindowManager popUpWindow;

    public void showItems(System.Type type, Button button, Transform parentPanel)
    {
        foreach(Item item in items)
        {
            if (item.GetType().Equals(type))
            {
                addSlot(button,  parentPanel, item);
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
            sortList();
        }
    }

    public bool subItem(Item item)
    {
        if (items.Contains(item))
        {
            item.subCount();

            if (item.getCount() == 0)
            {
                items.Remove(item);
                sortList();
            }
            return true;
        }
        return false;  
    }

    private void sortList()
    {
        items = items.OrderBy(i => i.getItemName()).ToList();
    }

    private void addSlot(Button button2,  Transform parentPanel, Item item)
    {

        var btnItem = Instantiate(button2);
        btnItem.transform.SetParent(parentPanel);

        btnItem.name = item.name;
        if (item.GetComponent<Image>().sprite != null)
        {
            btnItem.transform.GetChild(1).GetComponent<Image>().sprite = item.GetComponent<Image>().sprite;
            btnItem.transform.GetChild(1).GetComponent<Image>().preserveAspect = true;

        }

        else
            btnItem.transform.GetChild(1).GetComponent<Image>().enabled = false;


        if (item.GetType() == typeof(Equipment))
        {
            string text = "Möchtest du\n\n" + item.getItemName() ;
            if(item.GetComponent<Equipment>().getStr() != 0)
            {
                text += "\nSTR+"+item.GetComponent<Equipment>().getStr();
            }
            if (item.GetComponent<Equipment>().getCon() != 0)
            {
                text += "\nKON+"+item.GetComponent<Equipment>().getCon();
            }
            if (item.GetComponent<Equipment>().getAgi() != 0)
            {
                text += "\nGES+"+item.GetComponent<Equipment>().getAgi();
            }
            if (item.GetComponent<Equipment>().getWis() != 0)
            {
                text += "\nWEI+"+item.GetComponent<Equipment>().getWis();
            }
            text += "\n\nAusrüsten?";
            string type = "EQ";
            btnItem.onClick.AddListener(() => popUpWindow.createDescriptionWindow(btnItem, item, text, type));
        }

        else if(item.GetType() == typeof(Food))
        {
            string text = "Möchtest du \n\n" + item.getItemName() + "\nHP+" + item.GetComponent<Food>().getHealthPoints() + "\nFP+" + item.GetComponent<Food>().getFoodPoints() +  "\n\nessen?";
            string type = "Food";
            btnItem.onClick.AddListener(() => popUpWindow.createDescriptionWindow(btnItem, item, text, type));
        }

        else if(item.GetType() == typeof(Tool))
        {
            string text = "Möchtest du \n\n" + item.getItemName() + "\nHaltbarkeit: " + item.GetComponent<Tool>().getCurrentStability() + "\n\nAusrüsten?";
            string type = "Tool";
            btnItem.onClick.AddListener(() => popUpWindow.createDescriptionWindow(btnItem, item, text, type));
        }

        btnItem.GetComponentInChildren<Text>().text = item.getItemName() +" x"+ item.getCount();
        btnItem.transform.localScale = new Vector3(1, 1, 1);
    }

    public void clearPage(Transform parentPanel)
    {
        foreach (Transform child in parentPanel) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
