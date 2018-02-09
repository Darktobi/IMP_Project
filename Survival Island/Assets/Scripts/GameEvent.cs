using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : MonoBehaviour {

    public string title;
    public string description;

    public enum DangerLevel { None, Medium, High };
    public DangerLevel dangerLevel;

    public abstract void run(Player player);

}
