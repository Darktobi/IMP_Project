using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class PlayerDatas : MonoBehaviour {

    public int str, con, agi, wis;
    public float health, healthMAX, ap, apMAX, food, foodMAX;
    public Equipment weapon, head, chest, hands, legs, feet;
    public Tool tool;
    public Inventory inventory;
    public Player player;
    public string currentLocationName;
    public string currentActivityName;

    //EQ-Slots in UI in Character Display
    public Text[] eqSlots;


    private void Awake()
    {
        //data = new PlayerData();
        //new PlayerDatas();
        
        if (PlayerPrefs.GetInt("New Game") == 1)
        {
            Debug.Log("New Game");

            str = 5;
            con = 5;
            agi = 5;
            wis = 5;

            foodMAX = 100;
            food = 100;
            apMAX = 8;
            ap = 8;
            healthMAX = 50;
            health = 50;

        }
        else
        {
            Load();
            
        }
    }

    //Load and Save

    private SaveLoad CreateSaveGameObject()
    {
        SaveLoad data = new SaveLoad();
        Debug.Log("Saving...");

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

        foreach (string name in data.currentEquipment)
        {
             Debug.Log(name);
        }

        

        //Actions
        data.currentLocationName = currentLocationName;
        data.currentActivityName = currentActivityName;

        //Inventory
        data.currentInventory = new Dictionary<string, int>();

        foreach (Item item in inventory.items)
        {
            data.currentInventory.Add(item.name, item.getCount());
        }

        

        return data;
    }

    public void Save()
    {
        SaveLoad data = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath+ "/playerInfo.dat"))
        {
            //SaveLoad data = new SaveLoad();
            Debug.Log("Loading...");

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
            

            //Equipment
            //new Equipment();
           
            Equipment[] savedEquip = GameObject.FindObjectsOfType<Equipment>();
            Tool[] savedTools = GameObject.FindObjectsOfType<Tool>();
            new Player();

            foreach (string name in data.currentEquipment)
            {
                foreach (Equipment eq in savedEquip)
                {
                    //Debug.Log(eq);
                    if (eq.name == name)
                    {
                        
                        str -= eq.str;
                        con -= eq.con;
                        agi -= eq.agi;
                        wis -= eq.wis;
                        player.equip(eq);
                    }

                }
                foreach (Tool eq in savedTools)
                {
                    //Debug.Log(eq);
                    if (eq.name == name)
                    {

                        //str -= eq.str;
                        //con -= eq.con;
                        //agi -= eq.agi;
                        //wis -= eq.wis;
                        player.equip(eq);
                    }

                }

            }


            

            //Inventory
            new Inventory();

            Item[] savedItems = GameObject.FindObjectsOfType<Item>();

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

            //Actions
            currentLocationName = data.currentLocationName;
            currentActivityName = data.currentActivityName;

        }

    }

}

