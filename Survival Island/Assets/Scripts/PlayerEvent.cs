using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerEvent : GameEvent {

    public int healthPoints;
    public int activityPoints;

    public override void run(Player player)
    {
        player.setHealth(healthPoints);
        player.setActivityPoints(activityPoints);
    }
}
