using UnityEngine;

[System.Serializable]
public class Equipment : Item {

    [SerializeField]
    private int str, con, agi, wis;

    public enum Types{ Weapon, Head, Chest, Hands, Legs, Feet };
    public Types type;

	private void Start () {}
	
    public int getStr()
    {
        return str;
    }

    public int getCon()
    {
        return con;
    }

    public int getAgi()
    {
        return agi;
    }

    public int getWis()
    {
        return wis;
    }
}
