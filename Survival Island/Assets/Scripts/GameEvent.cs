using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : MonoBehaviour {

    [SerializeField]
    private string title;
    [SerializeField]
    private string description;

    public enum DangerLevel { None, Medium, High };
    public DangerLevel dangerLevel;

    public abstract void run(Player player);

    public string getTitle()
    {
        return title;
    }
   
    public string getDescription()
    {
        return description;
    }

}
