using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarHandler : MonoBehaviour {

    public Player player;

    public Image apBar;
    public Image foodBar;
    public Image healthBar;

    public Text apText;
    public Text foodText;
    public Text healthText;

    public float apValue;
    public float foodValue;
    public float healthValue;


    private void Update ()
    {
        SetRightValues();
        foodText.text = player.getFood() + "/" + player.getFoodMax() + " FP";
        apText.text = player.getAp() + "/" + player.getApMax() + " AP";
        healthText.text = player.getHealth() + "/" + player.getHealthMax() + " HP";
    }

    private void SetRightValues()
    {
        healthBar.fillAmount = player.getHealth() / player.getHealthMax();
        apBar.fillAmount = player.getAp() / player.getApMax();
        foodBar.fillAmount = player.getFood() / player.getFoodMax();
    }
}
