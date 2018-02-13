using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    System.Random rnd = new System.Random();

    public bool checkForEvent()
    {
        int number = rnd.Next(1, 101);

        // 25% Chance, dass Event eintritt 
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

        // Bestimmen welches Gefahrenlevel gewählt wird
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


        foreach (GameEvent gameEvent in location.possibleEvents)
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
        //Zufälliges Event der gewählten Gefahrenstufe auswählen
        number = rnd.Next(0, gameEvents.Count);
        return gameEvents[number];
    }

}
