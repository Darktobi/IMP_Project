using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Food : Item {

    [SerializeField]
    private int healthPoints;

    private void Start() { }

    public int getHealthPoints()
    {
        return healthPoints;
    }
}
