using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWindowManager : MonoBehaviour {

    public Transform canvas;
    public Crafter crafter;
    public Player player;

    public GameObject notiWindow;

    public GameObject descrWindow;

    public void createNotificationWindow(string title, string text)
    {
        GameObject window = Instantiate(notiWindow, new Vector4(0, 0, 0), transform.rotation);
        window.transform.SetParent(canvas, false);

        window.transform.Find("Image/Title_Panel/Title_Text").GetComponent<Text>().text = title;
        window.transform.Find("Image/Upper_Panel/Event_Text").GetComponent<Text>().text = text; 

        Button okbutton = window.transform.Find("Image/Down_Panel/Button").GetComponent<Button>();
        okbutton.onClick.AddListener(() => Destroy(window));

    }

    public void createDescriptionWindow(Button button, Item item, string text, string caseType)
    {
        GameObject window = Instantiate(descrWindow, new Vector4(0,0,0), transform.rotation);
        window.transform.SetParent(canvas, false);

        window.transform.Find("Image/Title_Panel/Title_Text").GetComponent<Text>().text = item.getItenName();
        window.transform.Find("Image/Upper_Panel/Description_Text").GetComponent<Text>().text = text;

        Button acceptButton = window.transform.Find("Image/Down_Panel/Use_Button").GetComponent<Button>();

        if (caseType == "Crafting")
        {
            acceptButton.onClick.AddListener(() => crafter.craft(item));
        }else if (caseType == "EQ")
        {
            acceptButton.onClick.AddListener(() => player.equip(item as Equipment));
        }
        else if (caseType == "Food")
        {
            acceptButton.onClick.AddListener(() => player.eat(item as Food));
        }
        else if (caseType == "Tool")
        {
            acceptButton.onClick.AddListener(() => player.equip(item as Tool));
        }

        acceptButton.onClick.AddListener(() => Destroy(window));

        Button cnclbutton = window.transform.Find("Image/Down_Panel/Cancel_Button").GetComponent<Button>();
        cnclbutton.onClick.AddListener(() => Destroy(window));
    }

    internal void createDescriptionWindow(Button btnPanel, Item item, string text, object v)
    {
        throw new NotImplementedException();
    }
}
