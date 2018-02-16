using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MaterialManager : MonoBehaviour {

    public List<Item> collectMaterials(Activity activity, Location location, int maxNumberOfItems)
    {
        List<Item> collectedMaterials = new List<Item>();
        foreach (Item activityMaterial in activity.collectableMaterials)
        {
            if (location.collectableMaterials.Contains(activityMaterial))
            {
                collectedMaterials.Add(activityMaterial);
            }
            
        }

        return chooseCollectedMaterials(collectedMaterials, maxNumberOfItems);
    }

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
