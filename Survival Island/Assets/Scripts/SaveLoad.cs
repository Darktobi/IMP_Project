using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
class SaveLoad
{
    public float healthMAX;
    public float apMAX;
    public float foodMAX;

    public float health;
    public float ap;
    public float food;

    public string currentLocationName;
    public string currentActivityName;

    //public string[] eqSlots;
    //public Equipment[] currentEquipment;
    public Equipment currentHands;
    public Tool currentTool;

    public int str, con, agi, wis;
    public List<Item> items = new List<Item>();

    public Dictionary<string, int> currentInventory;
    public List<string> currentEquipment;

}
