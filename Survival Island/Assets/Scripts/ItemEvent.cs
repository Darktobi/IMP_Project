using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvent : GameEvent {

    [SerializeField]
    private List<Item> items;

    public override void run(Player player)
    {
        System.Random rnd = new System.Random();
        int number = rnd.Next(0, items.Count);
        player.addItem(items[number]);
    }

}
