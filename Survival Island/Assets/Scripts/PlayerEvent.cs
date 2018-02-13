using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerEvent : GameEvent {

    [SerializeField]
    public int healthPoints;
    [SerializeField]
    public int activityPoints;

    public override void run(Player player)
    {
        player.setHealth(healthPoints);
        player.setAp(activityPoints);
    }
}
