using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public class PlayerDatas : MonoBehaviour {

    public int str, con, agi, wis;
    public float health, healthMAX, ap, apMAX;
    public Inventory inventory;

    //public SaveLoad data;

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

            healthMAX = 500;
            apMAX = 8;
            health = 500;
            ap = 8;
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

        //Stats
        data.healthMAX = healthMAX;
        data.health = health;
        data.apMAX = apMAX;
        data.ap = ap;

        //Attributes
        data.str = str;
        data.con = con;
        data.agi = agi;
        data.wis = wis; 

        data.addresses = new Dictionary<string, int>();


        foreach (Item item in inventory.items)
        {

          
            data.addresses.Add(item.name, item.getCount());
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

            

            //Stats
            healthMAX = data.healthMAX;
            health = data.health;
            apMAX = data.apMAX;
            ap =  data.ap;

            //Attributes
            str = data.str;
            con = data.con;
            agi = data.agi;
            wis = data.wis;

            new Inventory();

            Item[] test2 = GameObject.FindObjectsOfType<Item>();

            foreach (KeyValuePair<string, int> item in data.addresses)
            {
                foreach (Item mat in test2)
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

        }
        
    }

}

