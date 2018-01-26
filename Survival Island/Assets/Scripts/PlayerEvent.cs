using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent : GameEvent {

    public int healthPoints;
    public int activityPoints;

    public override void run(Player player)
    {
        player.changeStatus(healthPoints, activityPoints);
    }
}
