using UnityEngine;

public class PlayerEvent : GameEvent {

    [SerializeField]
    private int healthPoints;
    [SerializeField]
    private int foodPoints;
    [SerializeField]
    private int activityPoints;


    public override void run(Player player)
    {
        player.setHealth(healthPoints);
        player.setAp(activityPoints);
        player.setFood(foodPoints);
    }

    public int getHealtPoints()
    {
        return healthPoints;
    }

    public int getFoodPoints()
    {
        return foodPoints;
    }

    public int getActivityPoints()
    {
        return activityPoints;
    }

}
