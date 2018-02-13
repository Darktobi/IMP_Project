﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEvent : GameEvent {

    public int str;
    public int Max_health;
    private int totalDamage;

    public override void run(Player player)
    {
        int health = Max_health;
        //totalDamage = 0;
        
        int playerStr = player.getStr();
        bool battleIsRunning = true;

        while (battleIsRunning)
        {
            Debug.Log("Spieler greift an!");
            health -= playerStr;

            if(health <= 0)
            {
                battleIsRunning = false;
                Debug.Log("Spieler hat gewonnen ");
            }
            else
            {
                Debug.Log("Gegner greift an!");
                player.setHealth(-str);
                totalDamage += str;
            }
            if(player.getHealth() < 0)
            {
                battleIsRunning = false;
            }
        }

    }

    public int getTotalDamage()
    {
        return totalDamage;
    }
}
