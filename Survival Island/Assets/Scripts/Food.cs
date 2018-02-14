using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Food : Item {

    [SerializeField]
    private int healthPoints;
    [SerializeField]
    private int foodPoints;

    private void Start() { }

    public int getHealthPoints()
    {
        return healthPoints;
    }

    public int getFoodPoints()
    {
        return foodPoints;
    }
}
