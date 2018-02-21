using System.Collections.Generic;
using UnityEngine;

public class ItemEvent : GameEvent {

    [SerializeField]
    private List<Item> items;
    private Item givenItem;

    public override void run(Player player)
    {
        System.Random rnd = new System.Random();

        int number = rnd.Next(0, items.Count);

        givenItem = items[number];
        player.addItem(items[number]);
    }

    public Item getGivenItem()
    {
        return givenItem;
    }

}
