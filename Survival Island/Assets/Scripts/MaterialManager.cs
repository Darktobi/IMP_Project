using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MaterialManager : MonoBehaviour {

    //Materials you can get from a specific activity on a specific location
    public List<Item> collectMaterials(Activity activity, Location location, int maxNumberOfItems)
    {
        List<Item> collectedMaterials = new List<Item>();
        foreach (Item activityMaterial in activity.getCollectableMaterials())
        {
            if (location.getCollectableMaterials().Contains(activityMaterial))
            {
                collectedMaterials.Add(activityMaterial);
            }
        }

        return chooseCollectedMaterials(collectedMaterials, maxNumberOfItems);
    }

    //Random materials you get out of all possible materials
    private List<Item> chooseCollectedMaterials(List<Item> itemList, int maxNumberOfItems)
    {
        System.Random rnd = new System.Random();
        List<Item> choosenMaterials = new List<Item>();
        int numberOfItems = rnd.Next(1, maxNumberOfItems + 1);

        for (int i = 0; i < numberOfItems; i++)
        {
            int currentIndex = rnd.Next(0, itemList.Count);
            choosenMaterials.Add(itemList[currentIndex]);
        }

        //Return a sorted List for better work in Player class
        choosenMaterials = choosenMaterials.OrderBy(i => i.getItemName()).ToList();

        return choosenMaterials;
    }

}
