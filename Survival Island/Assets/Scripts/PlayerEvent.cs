using UnityEngine;

public class PlayerEvent : GameEvent {

    [SerializeField]
    private int healthPoints;
    [SerializeField]
    private int activityPoints;
    [SerializeField]
    private int foodPoints;

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

    public int getActivityPoints()
    {
        return activityPoints;
    }

    public int getFoodPoints()
    {
        return foodPoints;
    }
}
