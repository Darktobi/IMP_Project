using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class SaveLoad
{
    public float healthMAX;
    public float apMAX;

    public float health;
    public float ap;

    public int str, con, agi, wis;
    public List<Item> items = new List<Item>();
    //public Item item;

    //public MonoBehaviour itemMono;

    public Dictionary<string, int> addresses;

    //public Item item;
    //public List<string> mat;
    //public string[] matArray = new string[3];
    //public Mat[] matArray = new Mat[3];

    //public System.Type Mat, Tool, Food, Equipment;


}
