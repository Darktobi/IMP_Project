using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

    [SerializeField]
    private PlayerDatas playerData;
    [SerializeField]
    private int foodDownSpeed;

    private int counter;
    private MaterialManager materialManager;

    public InventoryHandler inventoryHandler;
    public PopUpWindowManager popUpWindow;

    

	void Start () {

        counter = 0;
        materialManager = new MaterialManager();
    }

    public float getHealth()
    {
        return playerData.health;
    }

    public float getHealthMax()
    {
        return playerData.healthMAX;
    }

    public float getAp()
    {
        return playerData.ap;
    }

    public float getApMax()
    {
        return playerData.apMAX;
    }

    public float getFood()
    {
        return playerData.food;
    }

    public float getFoodMax()
    {
        return playerData.foodMAX;
    }

    public int getStr()
    {
        return playerData.str;
    }

    public int getCon()
    {
        return playerData.con;
    }

    public int getAgi()
    {
        return playerData.agi;
    }

    public int getWis()
    {
        return playerData.wis;
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
            playerData.deleteFile();

            popUpWindow.createNotificationWindow("Game Over", "Dein Charakter hat es leider nicht geschafft zu überleben,\nsondern ist gestorben!\n\nVersuche es doch noch einmal!");
        }

        if (playerData.health >= playerData.healthMAX)
        {
            playerData.health = playerData.healthMAX;
        }
    }

    public void setAp(int activityPoints)
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
        //Debug.Log("Saved!");
    }

    public bool checkEquipptedTool(Activity activity)
    {
        if (activity.getNeededTool() == null)
        {
            return true;
        }

        if (playerData.tool == activity.getNeededTool())
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
                playerData.inventory.subItem(playerData.tool);
                playerData.tool = null;
                playerData.eqSlots[6].text = "Werkz.: ";
            }
        }
    }
   
    public void eat(Food food)
    {
        if (playerData.inventory.subItem(food))
        {
            setHealth(food.getHealthPoints());
            playerData.food = playerData.foodMAX;
            string title = "Heilung";
            string description = "Du wurdest um \n" + food.getHealthPoints() + " \ngeheilt.\nDu hast jetzt \n" + playerData.health + " Gesundheit.";
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
            playerData.inventory.addItem(collectedMaterial);
            string title = "Du hast etwas gefunden!";
            string description = "Du hast eine/n \n\n" + collectedMaterial.getItemName() + "\n\ngefunden!";

            popUpWindow.createNotificationWindow(title, description);
        }
    }

    public void addItem(Item item)
    {
        playerData.inventory.addItem(item);
    }

    public bool subItem(Item item)
    {
        return playerData.inventory.subItem(item);
    }

    public void equip(Tool tool)
    {
        playerData.tool = tool;
        playerData.eqSlots[6].text = "Werkz.: " + tool.getItemName();
        playerData.Save();
    }

    public void equip(Equipment equipment)
    {
        if (equipment.type == Equipment.Types.Weapon)
        {
            unequip(playerData.weapon);
            playerData.weapon = equipment;
            playerData.eqSlots[0].text = "Waffe: " + playerData.weapon.getItemName();
        }
        else if (equipment.type == Equipment.Types.Head)
        {
            unequip(playerData.head);
            playerData.head = equipment;
            playerData.eqSlots[1].text = "Kopf: " + equipment.getItemName();
        }
        else if (equipment.type == Equipment.Types.Breast)
        {
            unequip(playerData.chest);
            playerData.chest = equipment;
            playerData.eqSlots[2].text = "Brust: " + equipment.getItemName();
        }
        else if (equipment.type == Equipment.Types.Hands)
        {
            unequip(playerData.hands);
            playerData.hands = equipment;
            playerData.eqSlots[3].text = "Hände: " + equipment.getItemName();
        }
        else if (equipment.type == Equipment.Types.Legs)
        {
            unequip(playerData.legs);
            playerData.legs = equipment;
            playerData.eqSlots[4].text = "Beine: " + equipment.getItemName();
        }
        else if (equipment.type == Equipment.Types.Feet)
        {
            unequip(playerData.feet);
            playerData.feet = equipment;
            playerData.eqSlots[5].text = "Füße: " + equipment.getItemName();
        }

        playerData.str += equipment.getStr();
        playerData.con += equipment.getCon();
        playerData.agi += equipment.getAgi();
        playerData.wis += equipment.getWis();
        save();
    }

    private void unequip(Equipment equipment)
    {
        if(equipment != null)
        {
            playerData.str -= equipment.getStr();
            playerData.con -= equipment.getCon();
            playerData.agi -= equipment.getAgi();
            playerData.wis -= equipment.getWis();
        }
    }
}


