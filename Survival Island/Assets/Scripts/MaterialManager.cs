﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour {


    public List<Mat> collectMaterials(Activity activity, Location location)
    {
        List<Mat> collectedMaterials = new List<Mat>();
        foreach (Mat activityMaterial in activity.collectableMaterials)
        {
            if (location.collectableMaterials.Contains(activityMaterial))
            {
                collectedMaterials.Add(activityMaterial);
            }
            
        }
        return collectedMaterials;
    }
}
