using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PlayerEvent : GameEvent {

    [SerializeField]
    private int healthPoints;
    [SerializeField]
    private int activityPoints;

    public override void run(Player player)
    {
        player.setHealth(healthPoints);
        player.setAp(activityPoints);
    }
}
