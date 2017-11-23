using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour {


    public List<string> collectMaterials(Activity activity, Location location)
    {
        List<string> collectedMaterials = new List<string>();
        foreach (string activityMaterial in activity.collectableMaterials)
        {
            foreach (string locationMaterial in location.collectableMaterials)
            {
                Debug.Log(locationMaterial);
                if (activityMaterial == locationMaterial)
                {
                    collectedMaterials.Add(activityMaterial);
                    break;
                }
            }
        }
        return collectedMaterials;
    }
}
