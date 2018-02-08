﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

    //private const int MAX_HEALTH = 100;
    private float health;
    private float activityPoints;
    private bool isWorking;
    private int str, con, agi, wis, counter;
    private Location currentLocation;
    private Activity currentWork;
    private MaterialManager materialManager;
    private EventManager eventManager;
    private GameEvent currentEvent;

    //public Equipment weapon, head, breast, hands, legs, feet;
    //public Tool tool;

    public Inventory inventory;
    public InventoryHandler inventoryHandler;
    public PlayerDatas playerData;
    public Text currentLocationText;
    public Image durationBar;
    public PopUpWindowManager popUpWindow;

    private float workingTime;
    private float totalTime;

    public int foodDownSpeed;

	// Use this for initialization
	void Start () {
        health = playerData.health;
        activityPoints = playerData.ap;
        PlayerPrefs.SetFloat("foodValue", health);

        //eqSlots = new Text[5];

        workingTime = 0;
        counter = 0;
        isWorking = false;
        materialManager = new MaterialManager();
        eventManager = new EventManager();

        playerData.currentLocationName = "Lager";
        playerData.currentActivityName = "Nichts";


        durationBar.fillAmount = 1;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.S))
        {
            save();
        }


        if (isWorking)
        {
            workingTime -= Time.deltaTime;

            //in der Leiste darstellen
            durationBar.fillAmount = workingTime/totalTime;

            if (workingTime <= 0)
            {
                isWorking = false;
                workingTime = 0;

                collectMaterials();

                if(currentLocation.locationName != "Lager")
                {
                    //Check if event occured
                    if (eventManager.checkForEvent())
                    {
                        Debug.Log("Event ist eingetreten");
                        currentEvent = eventManager.chooseEvent(currentLocation);

                        if(currentEvent != null)
                        {
                            Debug.Log(currentEvent.title);
                            Debug.Log(currentEvent.description);

                            //Eventwindow
                            popUpWindow.createNotificationWindow(currentEvent.title, currentEvent.description);

                            currentEvent.run(this);
                        } 
                    }
                }

                //Check for tool stability if a tool is equiped
                if(playerData.tool != null)
                {
                    if (playerData.tool.getCurrentStability() <= 0)
                    {
                        playerData.tool.resetStability();
                        inventory.subItem(playerData.tool);
                        playerData.tool = null;
                    }
                }
               
                
                //ToDo: Set a basic location and basic work
                currentLocation = null;
                currentWork = null;

                //Am lager chillen
                playerData.currentLocationName = "Lager";
                playerData.currentActivityName = "Nichts";

                durationBar.fillAmount = 1;

                save();
                
            }

        }

        //Zuwissung der Aktuellen Tätigkeit und Ort zum Text in der UI
        currentLocationText.text = playerData.currentLocationName  + ": " + playerData.currentActivityName;
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

    public void changeStatus(int health, int activityPoints)
    {
        setHealth(health);
        setActivityPoints(activityPoints);
    }

    public void save()
    {
        playerData.Save();
        Debug.Log("Saved!");
    }

    public void setActivity(Activity activity)
    {
        if (!isWorking)
        {
                if (playerData.ap >= activity.activityPoints)
                {
                    if (checkEquipptedTool(activity))
                    {
                        currentWork = activity;
                        currentLocation = currentWork.currentLocation;
                        activityPoints -= currentWork.activityPoints;
                        workingTime = currentWork.workingTime;
                        isWorking = true;

                        //Zuwissung der aktuellen Location
                        playerData.currentLocationName = currentLocation.locationName;
                        playerData.currentActivityName = currentWork.activityName;

                        totalTime = workingTime;

                        Debug.Log("AP: " + activityPoints);
                        if (activityPoints > playerData.apMAX)

                        {
                            playerData.ap = playerData.apMAX;
                            Debug.Log("Max AP erreicht.");
                            activityPoints = playerData.apMAX;
                        }
                        else
                        {
                            playerData.ap = activityPoints;
                        }


                }

                else
                {
                    Debug.Log("Nicht das benötigte Werkzeug ausgerüstet. Du brauchst " + activity.neededTool.name);

                    string title = "Achtung!";
                    string description = "Nicht das benötigte Werkzeug ausgerüstet. Du brauchst ein/e \n\n" + activity.neededTool.name;

                    popUpWindow.createNotificationWindow(title, description);
                }
            }
            else
            {
                Debug.Log("Leider nicht genug Aktivitätspunkte zur Verfügung");
                string title = "Achtung";
                string description = "Leider nicht genug Aktivitätspunkte zur Verfügung";
                popUpWindow.createNotificationWindow(title, description);

            }

        }
        else
        {
            Debug.Log("Es wird noch eine andere Tätigkeit ausgeführt!");
            string title = "Achtung";
            string description = "Es wird noch eine andere Tätigkeit ausgeführt!";
            popUpWindow.createNotificationWindow(title, description);

        }

    }

    public void eat(Food food)
    {
        Debug.Log("Aktuelle Gesundheit: " + health);
        if (inventory.subItem(food))
        {
            Debug.Log("Spieler geheilt um " + food.healthPoints);
            setHealth(food.healthPoints);
            playerData.food = playerData.foodMAX;
            string title = "Heilung";
            string description = "Du wurdest um \n" + food.healthPoints + " \ngeheilt.\nDu hast jetzt \n" + playerData.health + " Gesundheit.";
            popUpWindow.createNotificationWindow(title, description);

            Debug.Log("Gesundheit nach dem Essen: " + health);
        }

        else
        {
            Debug.Log(food.name + " nicht im Inventar vorhanden");
            string title = "Nicht möglich";
            string description = food.name + " nicht im Inventar vorhanden";

            popUpWindow.createNotificationWindow(title, description);

        }

        inventoryHandler.OpenFood();
    }

    public void equip(Tool tool)
    {
        this.playerData.tool = tool;
        playerData.eqSlots[6].text = "Werkz.: "+tool.name;
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
            playerData.eqSlots[2].text = "Brust: "+ equipment.name;
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
            playerData.eqSlots[5].text = "Füße: "+ equipment.name;
        }

        playerData.str += equipment.str;
        playerData.con += equipment.con;
        playerData.agi += equipment.agi;
        playerData.wis += equipment.wis;
        Debug.Log("Stärke: " + str + ", Konstitution: " + con + ", Geschicklichkeit: " + agi + ", Wissen: " + wis);
        playerData.Save();
    }

    private void collectMaterials()
    {
        List<Item> collectedMaterials = materialManager.collectMaterials(currentWork, currentLocation);
        foreach (Item collectedMaterial in collectedMaterials)
        {
            inventory.addItem(collectedMaterial);
            string title = "Du hast etwas gefunden!";
            string description = "Du hast eine/n \n" + collectedMaterial.name + "\ngefunden";

            popUpWindow.createNotificationWindow(title, description);

        }
    }

    private void setHealth(int health)
    {
        this.health += health;

        if (this.health <= 0)
        {
            // Vorrübergehend, bis bessere Lösung
            Debug.Log("Spieler ist gestorben! ");
            SceneManager.LoadScene("MainMenu");
        }

        if (this.health >= playerData.healthMAX)
        {
            this.health = playerData.healthMAX;
        }

        playerData.health = this.health;
    }

    private void setActivityPoints(int activityPoints)
    {
        this.activityPoints += activityPoints;

        if (this.activityPoints < 0)
        {
            this.activityPoints = 0;
        }

        if (this.activityPoints >= playerData.apMAX)
        {
            this.activityPoints = playerData.apMAX;
        }

        playerData.ap = this.activityPoints;
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

    private bool checkEquipptedTool(Activity activity)
    {
        if(activity.neededTool == null)
        {
            return true;
        }

        //Da Daten jetzt in PlayerDatas sind, musste wurde tools gg playerData.tool geändert
        //this.playerData.tool = playerData.tool;
        if(playerData.tool == activity.neededTool)
        {
            playerData.tool.subStability();
            return true;
        }

        return false;
    }

    public void Test()
    {
        Debug.Log("Test");
    }
 
}


