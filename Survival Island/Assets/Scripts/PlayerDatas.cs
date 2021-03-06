﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerDatas : MonoBehaviour {

    public int str, con, agi, wis;
    public float health, healthMAX, ap, apMAX, food, foodMAX;
    public Equipment weapon, head, chest, hands, legs, feet;
    public Tool tool;
    public Inventory inventory;
    public ActivityManager activityManager;
    public Player player;
    public string currentLocationName;
    public string currentActivityName;

    //EQ-Slots in UI in Character Display
    public Text[] eqSlots;

    private bool dontSave = false;
    private Activity tempActivity;


    private void Awake()
    {
        if (PlayerPrefs.GetInt("New Game") == 1)
        {
            str = 5;
            con = 5;
            agi = 5;
            wis = 5;

            foodMAX = 100;
            food = 100;
            apMAX = 8;
            ap = 8;
            healthMAX = con*10;
            health = healthMAX;

            currentLocationName = "Lager";
            currentActivityName = "Nichts";
        }
        else
        {
            load();
        }
    }

    //Which variables will be saved
    private SaveLoad createSaveGameObject()
    {
        SaveLoad data = new SaveLoad();

        //Ressources
        data.healthMAX = healthMAX;
        data.health = health;
        data.apMAX = apMAX;
        data.ap = ap;
        data.foodMAX = foodMAX;
        data.food = food;

        //Attributes
        data.str = str;
        data.con = con;
        data.agi = agi;
        data.wis = wis;

        //Equipment
        data.currentEquipment = new List<string>();
        if (weapon != null)
            data.currentEquipment.Add(weapon.name);
        if (head != null)
            data.currentEquipment.Add(head.name);
        if (chest != null)
            data.currentEquipment.Add(chest.name);
        if (hands != null)
            data.currentEquipment.Add(hands.name);
        if (legs != null)
            data.currentEquipment.Add(legs.name);
        if (feet != null)
            data.currentEquipment.Add(feet.name);
        if (tool != null)
            data.currentEquipment.Add(tool.name);

        //Actions
        data.currentLocationName = currentLocationName;
        data.currentActivityName = currentActivityName;

        //Inventory
        data.currentInventory = new Dictionary<string, int>();
  
        foreach (Item item in inventory.getItems())
        {
            data.currentInventory.Add(item.name, item.getCount());
        }

        return data;
    }

    public void save()
    {
        if(dontSave == false)
        {
            SaveLoad data = createSaveGameObject();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        
            bf.Serialize(file, data);
            file.Close();
        }
    }

    public void load()
    {
        if(File.Exists(Application.persistentDataPath+ "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            SaveLoad data = (SaveLoad)bf.Deserialize(file);
            file.Close();

            //Ressources
            healthMAX = data.healthMAX;
            health = data.health;

            apMAX = data.apMAX;
            ap =  data.ap;

            foodMAX = data.foodMAX;
            food = data.food;

            //Attributes
            str = data.str;
            con = data.con;
            agi = data.agi;
            wis = data.wis;

            Item[] savedItems = FindObjectsOfType<Item>();

            foreach (KeyValuePair<string, int> item in data.currentInventory)
            {
                foreach (Item mat in savedItems)
                {
                    if (mat.name == item.Key)
                    {
                        for(int i = 0; i < item.Value; i++)
                        {
                            inventory.addItem(mat);
                        } 
                    }
                }
            }

            //Equipment
            Equipment[] savedEquip = FindObjectsOfType<Equipment>();
            Tool[] savedTools = FindObjectsOfType<Tool>();

            foreach (string name in data.currentEquipment)
            {
                foreach (Equipment eq in savedEquip)
                {
                    if (eq.name == name)
                    {
                        str -= eq.getStr();
                        con -= eq.getCon();
                        agi -= eq.getAgi();
                        wis -= eq.getWis();
                        player.equip(eq);
                    }
                }
                foreach (Tool eq in savedTools)
                {
                    if (eq.name == name)
                    {
                        player.equip(eq);
                    }
                }
            }

            //Activity
            Activity[] allActivities = FindObjectsOfType<Activity>();
            foreach (Activity activityName in allActivities)
            {
                if (activityName.getActivityName() == data.currentActivityName)
                {
                    tempActivity = activityName;
                }
            }

            //If Player has an activity to do
            if(tempActivity != null)
            {
                Location[] allLocations = FindObjectsOfType<Location>();
                foreach (Location locationName in allLocations)
                {
                    if (locationName.getLocationName() == data.currentLocationName)
                    {
                        tempActivity.currentLocation = locationName;
                    }
                }

                ap += tempActivity.getNeededAP();
                activityManager.setActivity(tempActivity);
            }
        }
    }

    //Deleting the savegame file
    public void deleteFile()
    {
        dontSave = true;
        File.Delete(Application.persistentDataPath + "/playerInfo.dat");
    }
}

