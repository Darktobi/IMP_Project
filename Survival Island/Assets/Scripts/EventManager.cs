using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    private System.Random rnd = new System.Random();

    public bool checkForEvent()
    {
        int number = rnd.Next(1, 101);

        // 25% chance for event
        if(number > 74)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public GameEvent chooseEvent(Location location)
    {
        List<GameEvent> gameEvents = new List<GameEvent>();
        GameEvent.DangerLevel dangerLevel;

        //Number for danger-level
        int number = rnd.Next(1, 101);
       
        if (number < 56)
        {
            dangerLevel = GameEvent.DangerLevel.None;
        }
        else if(number < 86)
        {
            dangerLevel = GameEvent.DangerLevel.Medium;
        }
        else
        {
            dangerLevel = GameEvent.DangerLevel.High;
        }


        foreach (GameEvent gameEvent in location.getPossibleEvents())
        {
            if (gameEvent.dangerLevel == dangerLevel)
            {
                gameEvents.Add(gameEvent);
            }
        }

        if (gameEvents.Count <= 0)
        {
            return null;
        }

        //Random event of choosen danger-level
        number = rnd.Next(0, gameEvents.Count);
        return gameEvents[number];
    }

}
