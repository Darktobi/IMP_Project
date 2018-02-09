using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

    private int counter;
    private MaterialManager materialManager;

    public Inventory inventory;
    public InventoryHandler inventoryHandler;
    public PlayerDatas playerData;
    public PopUpWindowManager popUpWindow;

    public int foodDownSpeed;

	void Start () {

        counter = 0;
        materialManager = new MaterialManager();
    }

    public float getHealth()
    {
        return playerData.health;
    }

    public float getActivityPoints()
    {
        return playerData.ap;
    }

    public int getStr()
    {
        return playerData.str;
    }

    public string getCurrentLocationName()
    {
        return playerData.currentLocationName;
    }

    public string getCurrentActivityName()
    {
        return playerData.currentActivityName;
    }

    public void setCurrentLocationName(string locationName)
    {
        playerData.currentLocationName = locationName;
    }

    public void setCurrentActivityName(string activityName)
    {
        playerData.currentActivityName = activityName;
    }

    public void setHealth(int health)
    {
        playerData.health += health;

        if (playerData.health <= 0)
        {
            // Vorrübergehend, bis bessere Lösung
            Debug.Log("Spieler ist gestorben! ");
            SceneManager.LoadScene("MainMenu");
        }

        if (playerData.health >= playerData.healthMAX)
        {
            playerData.health = playerData.healthMAX;
        }
    }

    public void setActivityPoints(int activityPoints)
    {
        playerData.ap += activityPoints;

        if (playerData.ap < 0)
        {
            playerData.ap = 0;
        }

        if (playerData.ap >= playerData.apMAX)
        {
            playerData.ap = playerData.apMAX;
        }
    }


    public void OnGUI()
    {
        counter++;
        if (counter == foodDownSpeed)
        {
            if (playerData.food != 0)
            {
                playerData.food--;
            } else
            {
                playerData.health--;                
            }
            counter = 0;
        }
    }

    public void save()
    {
        playerData.Save();
        Debug.Log("Saved!");
    }

    public bool checkEquipptedTool(Activity activity)
    {
        if (activity.neededTool == null)
        {
            return true;
        }

        if (playerData.tool == activity.neededTool)
        {
            playerData.tool.subStability();
            return true;
        }

        return false;
    }

    public void checkToolStability()
    {
        if (playerData.tool != null)
        {
            if (playerData.tool.getCurrentStability() <= 0)
            {
                playerData.tool.resetStability();
                inventory.subItem(playerData.tool);
                playerData.tool = null;
            }
        }
    }
   
    public void eat(Food food)
    {
        if (inventory.subItem(food))
        {
            setHealth(food.healthPoints);
            playerData.food = playerData.foodMAX;
            string title = "Heilung";
            string description = "Du wurdest um \n" + food.healthPoints + " \ngeheilt.\nDu hast jetzt \n" + playerData.health + " Gesundheit.";
            popUpWindow.createNotificationWindow(title, description);

            inventoryHandler.OpenFood();
        }
        else
        {
            string title = "Nicht möglich";
            string description = food.name + " nicht im Inventar vorhanden";
            popUpWindow.createNotificationWindow(title, description);
        }
    }

    public void collectMaterials(Activity currentActivity, Location currentLocation)
    {
        List<Item> collectedMaterials = materialManager.collectMaterials(currentActivity, currentLocation);
        foreach (Item collectedMaterial in collectedMaterials)
        {
            inventory.addItem(collectedMaterial);
            string title = "Du hast etwas gefunden!";
            string description = "Du hast eine/n \n" + collectedMaterial.name + "\ngefunden";

            popUpWindow.createNotificationWindow(title, description);
        }
    }

    public void equip(Tool tool)
    {
        playerData.tool = tool;
        playerData.eqSlots[6].text = "Werkz.: " + tool.name;
        playerData.Save();
    }

    public void equip(Equipment equipment)
    {
        if (equipment.type == Equipment.Types.Weapon)
        {
            unequip(playerData.weapon);
            playerData.weapon = equipment;
            playerData.eqSlots[0].text = "Waffe: " + playerData.weapon.name;
        }
        else if (equipment.type == Equipment.Types.Head)
        {
            unequip(playerData.head);
            playerData.head = equipment;
            playerData.eqSlots[1].text = "Kopf: " + equipment.name;
        }
        else if (equipment.type == Equipment.Types.Breast)
        {
            unequip(playerData.chest);
            playerData.chest = equipment;
            playerData.eqSlots[2].text = "Brust: " + equipment.name;
        }
        else if (equipment.type == Equipment.Types.Hands)
        {
            unequip(playerData.hands);
            playerData.hands = equipment;
            playerData.eqSlots[3].text = "Hände: " + equipment.name;
        }
        else if (equipment.type == Equipment.Types.Legs)
        {
            unequip(playerData.legs);
            playerData.legs = equipment;
            playerData.eqSlots[4].text = "Beine: " + equipment.name;
        }
        else if (equipment.type == Equipment.Types.Feet)
        {
            unequip(playerData.feet);
            playerData.feet = equipment;
            playerData.eqSlots[5].text = "Füße: " + equipment.name;
        }

        playerData.str += equipment.str;
        playerData.con += equipment.con;
        playerData.agi += equipment.agi;
        playerData.wis += equipment.wis;
        save();
    }

    private void unequip(Equipment equipment)
    {
        if(equipment != null)
        {
            playerData.str -= equipment.str;
            playerData.con -= equipment.con;
            playerData.agi -= equipment.agi;
            playerData.wis -= equipment.wis;
        }
    }
}


