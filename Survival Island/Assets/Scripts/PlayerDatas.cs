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

            Debug.Log("Stärke: " + str + ", Konstitution: " + con + ", Geschicklichkeit: " + agi + ", Wissen: " + wis);



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

       

        //Debug.Log(inventory.items);
        //data.items = inventory.items;
        //Debug.Log(inventory.items.ToString());

        //int i = 0;
        //foreach (Item item in inventory.items)
        //{
        //    if (item.GetType().Equals(typeof(Mat)))
        //    {

        //        Debug.Log("Containing" + item.name + " x" + item.getCount());
        //        //data.matArray[i] = item.name;
        //        //data.matArray[i] = (Mat)item;

        //        data.items.Add(item);
        //        i++;
        //    }
        //}

        //Debug.Log(data.matArray);

        //data.itemMono = inventory.items;

        data.addresses = new Dictionary<string, int>();

        //int i = 0;
        //foreach (Item item in inventory.items)
        //{
        //    if (item.GetType().Equals(typeof(Mat)))
        //    {

        //        //Debug.Log("Containing" + item.name + " x" + item.getCount());
        //        data.addresses.Add(item.name, item.getCount());
        //        i++;
        //    }
        //}

        foreach (Item item in inventory.items)
        {

            //if (item.GetType().Equals(typeof(Mat)))
            //{

            //    //Debug.Log("Containing" + item.name + " x" + item.getCount());
            //    data.addresses.Add(item.name, item.getCount());
            //    Debug.Log("Item: " + item);
            //    Debug.Log("Name: " + item.name);

            //}

            Debug.Log("Item: " + item + "\nName: " +  item.name + "\nCount: " + item.getCount());
            data.addresses.Add(item.name, item.getCount());
        }


        Debug.Log("Elements: "+data.addresses.Count());

        //Dictionary<string, int>.ValueCollection values = data.addresses.Values;
        //foreach (int mat in values)
        //{
        //    Debug.Log("Values: "+mat);
        //}

        foreach(KeyValuePair<string, int> item in data.addresses)
        {
            Debug.Log("Name: " + item.Key + " | Wert: " +  item.Value);
            //Debug.Log("Search for Object: " + GameObject.Find(item.Key));

        }


        //Geht alle von typ Mat durch
        //Mat[] test = GameObject.FindObjectsOfType<Mat>();
        //foreach (Mat mat in test)
        //{

        //    if(mat.name == "Holz")
        //    Debug.Log("includes: "+ mat);

        //    if (mat.name == "Muschel")
        //    Debug.Log("includes: " + mat);
        //}

        return data;
    }

    public void Save()
    {
        SaveLoad data = CreateSaveGameObject();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        

        bf.Serialize(file, data);
        file.Close();

        //Debug.Log("File saved to:" + Application.persistentDataPath);
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

            //data.addresses.Clear();

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

            //inventory.items = data.items;
            //Debug.Log(data.addresses);
            Item[] test2 = GameObject.FindObjectsOfType<Item>();
            //Debug.Log(test2);
            //int j = 0;
            foreach (KeyValuePair<string, int> item in data.addresses)
            {
                Debug.Log("Name: " + item.Key + " | Wert: " + item.Value);
                foreach (Item mat in test2)
                {

                    if (mat.name == item.Key)
                    {
                        Debug.Log("To Inv: " + mat + " " +item.Value + " times");
                        for(int i = 0; i < item.Value; i++)
                        {
                            Debug.Log("i = " + i);
                            inventory.addItem(mat);

                            //Testint to get cuttent content of the Inventory
                            //foreach (Item item2 in inventory.items)
                            //{
                            //    if (item.GetType().Equals(typeof(Food)))
                            //    {
                            //        Debug.Log("Containing" + item2.name + " x" + item2.getCount());
                            //    }
                            //    if (item.GetType().Equals(typeof(Mat)))
                            //    {
                            //        Debug.Log("Containing" + item2.name + " x" + item2.getCount());
                            //    }
                            //}
                        }
                        
                    }

                }
            }

            
            

            //foreach (Item item in data.addresses)
            //{
            //    Debug.Log("Test");
            //}

            Debug.Log("File loaded from:" + Application.persistentDataPath);
        }
        
    }

}

