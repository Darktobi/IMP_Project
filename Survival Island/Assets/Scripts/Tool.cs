using UnityEngine;

[System.Serializable]
public class Tool : Item {

    [SerializeField]
    private int MaxStability;

    private int currentStability;

	private void Start () {
        currentStability = MaxStability;
	}
	
    public void subStability()
    {
        currentStability--;
    }

    public int getCurrentStability()
    {
        return currentStability;
    }

    public void resetStability()
    {
        currentStability = MaxStability;
    } 
}
