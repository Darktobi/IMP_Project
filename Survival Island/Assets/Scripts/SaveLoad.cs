using System.Collections.Generic;

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

    public int str, con, agi, wis;
    public List<Item> items = new List<Item>();

    public Dictionary<string, int> currentInventory;
    public List<string> currentEquipment;
}
