using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarHandler : MonoBehaviour {

    public PlayerDatas playerData;

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
        foodText.text = playerData.food + "/" + playerData.foodMAX + " FP";
        apText.text = playerData.ap + "/" + playerData.apMAX + " AP";
        healthText.text = playerData.health + "/" + playerData.healthMAX + " HP";
    }

    private void SetRightValues()
    {
        healthBar.fillAmount = playerData.health / playerData.healthMAX;
        apBar.fillAmount = playerData.ap / playerData.apMAX;
        foodBar.fillAmount = playerData.food / playerData.foodMAX;
    }
}
