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

	private void Start () {

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

    public void setHealthMax()
    {
        playerData.healthMAX = playerData.con * 10;
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

        if (playerData.health > playerData.healthMAX)
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

        if (playerData.ap > playerData.apMAX)
        {
            playerData.ap = playerData.apMAX;
        }
    }

    public void setFood(int foodPoints)
    {
        playerData.food += foodPoints;

        if(playerData.food < 0)
        {
            playerData.food = 0;
        }

        if(playerData.food > playerData.foodMAX)
        {
            playerData.food = playerData.foodMAX;
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
                string title = "Werkzeug zerbrochen";
                string description = playerData.tool.getItemName() + " hat keine Haltbarkeit mehr und kann nicht weiter genutzt werden!";
                popUpWindow.createNotificationWindow(title, description);

                playerData.tool.resetStability();
                playerData.eqSlots[6].text = "Werkz.: ";
                playerData.tool = null;
            }
        }
    }
   
    public void eat(Food food)
    {
        if (playerData.inventory.subItem(food))
        {
            setHealth(food.getHealthPoints());
            setFood(food.getFoodPoints());
            string title = "Heilung";
            string description = "Du wurdest um \n" + food.getHealthPoints() + "HP \ngeheilt.\nDein Hunger wurde um \n" + food.getFoodPoints() + "FP \ngestillt.";
            popUpWindow.createNotificationWindow(title, description);
            inventoryHandler.OpenFood();
            save();
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
        List<Item> collectedMaterials = materialManager.collectMaterials(currentActivity, currentLocation, playerData.wis / 2);
        string title;
        string description;

        if(collectedMaterials.Count >= 1)
        {
            title = "Du hast etwas gefunden!";
            description = "Du hast folgende Items gefunden:\n\n";

            //Add all items and sum them for notification window
            int specificItemCount = 1;
            Item currentItem = null;

            foreach (Item collectedMaterial in collectedMaterials)
            {
                playerData.inventory.addItem(collectedMaterial);

                // First Item in List
                if(currentItem == null)
                {
                    currentItem = collectedMaterial;
                }

                else if (currentItem == collectedMaterial)
                {
                    specificItemCount++;;
                }
                else
                {
                    description += currentItem.getItemName() + " x" + specificItemCount + "\n";
                    currentItem = collectedMaterial;
                    specificItemCount = 1;
                }
                
            }
            // After ending Loop add last Item with number to notification window
            description += currentItem.getItemName() + " x" + specificItemCount + "\n";
        }
        else
        {
            title = "Du hast leider nix gefunden";
            description = "";
        }

        popUpWindow.createNotificationWindow(title, description);
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
        if (playerData.inventory.subItem(tool))
        {
            unequip(playerData.tool);
            playerData.tool = tool;
            playerData.eqSlots[6].text = "Werkz.: " + tool.getItemName();
            playerData.Save();

            playerData.inventory.subItem(tool);
            inventoryHandler.OpenTools();
        }

    }

    public void equip(Equipment equipment)
    {

        //Muss noch gelöst werden!

        //if (playerData.inventory.subItem(equipment))
        //{
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
            else if (equipment.type == Equipment.Types.Chest)
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

        playerData.inventory.subItem(equipment);
            inventoryHandler.OpenEQ();

            playerData.str += equipment.getStr();
            playerData.con += equipment.getCon();
            playerData.agi += equipment.getAgi();
            playerData.wis += equipment.getWis();
            setHealthMax();
            save();

        //}

    }

    private void unequip(Equipment equipment)
    {
        if(equipment != null)
        {
            playerData.inventory.addItem(equipment);
            playerData.str -= equipment.getStr();
            playerData.con -= equipment.getCon();
            playerData.agi -= equipment.getAgi();
            playerData.wis -= equipment.getWis();

            setHealthMax();

        }
    }

    private void unequip(Tool tool)
    {
        if(tool != null)
        {
            playerData.inventory.addItem(tool);
        }
    }

    public void WonGame()
    {
        string title = "Spiel Gewonnen!";
        string description = "Du hast ein Boot hergestellt. Damit ist dein Spieler in der Lage, die Insel zu verlassen!\n\nHerzlichen Glückwunsch, \ndu hast das Spiel erfolgreich durchgespielt!";
        popUpWindow.createNotificationWindow(title, description);

    }
}


