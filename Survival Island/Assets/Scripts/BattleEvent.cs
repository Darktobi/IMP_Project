using UnityEngine;

public class BattleEvent : GameEvent {

    [SerializeField]
    private int str;
    [SerializeField]
    private int maxHealth;

    private int currentHealth; 
    private int totalDamage;

    public override void run(Player player)
    {
        //Enemyhealth
        currentHealth = maxHealth;

        totalDamage = 0;
        
        int playerStr = player.getStr();
        bool battleIsRunning = true;

        while (battleIsRunning)
        {
            //Player attacking enemy
            currentHealth -= playerStr;

            if(currentHealth <= 0)
            {
                battleIsRunning = false;
            }
            //Player receiving damage
            else
            {
                player.setHealth(-str);
                totalDamage += str;
            }
            //Player has no HP left and lost battle/game
            if(player.getHealth() < 0)
            {
                battleIsRunning = false;
            }
        }

    }

    //The damage the player received in the battle
    public int getTotalDamage()
    {
        return totalDamage;
    }
}
