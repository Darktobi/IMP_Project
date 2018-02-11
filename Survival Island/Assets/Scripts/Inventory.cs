using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    [SerializeField]
    private Player player;

    public List<Item> items;
    public PopUpWindowManager popUpWindow;

    public void showItems(System.Type type, /*Transform rowPanel,*/ Button button, Transform parentPanel)
    {
        foreach(Item item in items)
        {
            if (item.GetType().Equals(type))
            {
                addSlot(button, /*rowPanel,*/ parentPanel, item);
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
        player.save();
    }

    public bool subItem(Item item)
    {
        if (items.Contains(item))
        {
            item.subCount();

            if (item.getCount() == 0)
            {
                items.Remove(item);
            }
            player.save();
            return true;
        }
        return false;  
    }

    private void addSlot(Button button2, /*Transform rowPanel,*/ Transform parentPanel, Item item)
    {
        //var btnPanel = Instantiate(rowPanel);
        //btnPanel.transform.SetParent(parentPanel);

        var btnItem = Instantiate(button2);
        btnItem.transform.SetParent(parentPanel);

        if(item.GetType() == typeof(Equipment))
        {
            string text = "Möchtest du \n\n" + item.getItenName() + "\n\nAusrüsten?";
            string type = "EQ";
            btnItem.onClick.AddListener(() => popUpWindow.createDescriptionWindow(btnItem, item, text, type));
        }

        else if(item.GetType() == typeof(Food))
        {
            string text = "Möchtest du \n\n" + item.getItenName() + "\n\nessen?";
            string type = "Food";
            btnItem.onClick.AddListener(() => popUpWindow.createDescriptionWindow(btnItem, item, text, type));
        }

        else if(item.GetType() == typeof(Tool))
        {
            string text = "Möchtest du \n\n" + item.getItenName() + "\n\nAusrüsten?";
            string type = "Tool";
            btnItem.onClick.AddListener(() => popUpWindow.createDescriptionWindow(btnItem, item, text, type));
        }

        btnItem.GetComponentInChildren<Text>().text = item.getItenName() +" x"+ item.getCount();
        btnItem.transform.localScale = new Vector3(1, 1, 1);
    }

    public void clearPage(Transform parentPanel)
    {
        foreach (Transform child in parentPanel) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
