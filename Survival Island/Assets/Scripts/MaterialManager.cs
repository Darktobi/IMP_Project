using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour {


    public List<Item> collectMaterials(Activity activity, Location location)
    {
        List<Item> collectedMaterials = new List<Item>();
        foreach (Item activityMaterial in activity.collectableMaterials)
        {
            if (location.collectableMaterials.Contains(activityMaterial))
            {
                collectedMaterials.Add(activityMaterial);
            }
            
        }
        return collectedMaterials;
    }
}
