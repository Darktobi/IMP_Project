using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarHandler : MonoBehaviour {

    public Image apBar;
    public Image foodBar;

    public Text apText;
    public Text foodText;

    public float apValue;
    public float foodValue;

    private void Awake()
    {
        PlayerPrefs.SetFloat("foodMAX", 500);
        PlayerPrefs.SetFloat("apMAX", 8);

        PlayerPrefs.SetFloat("apValue", apValue);
        PlayerPrefs.SetFloat("foodValue", foodValue);
    }

   

    // Update is called once per frame
    private void Update ()
    {
        //Just placeholder for input

        SetRightValues();

        foodText.text= Mathf.Round(PlayerPrefs.GetFloat("foodValue")) + "/"+ PlayerPrefs.GetFloat("foodMAX");
        apText.text = Mathf.Round(PlayerPrefs.GetFloat("apValue")) + "/" + PlayerPrefs.GetFloat("apMAX");

    }

    private void SetRightValues()
    {
        foodBar.fillAmount = PlayerPrefs.GetFloat("foodValue") / (PlayerPrefs.GetFloat("foodMAX"));
        apBar.fillAmount = PlayerPrefs.GetFloat("apValue") / (PlayerPrefs.GetFloat("apMAX"));
        //apBar.fillAmount = PlayerPrefs.GetFloat("apMAX");



    }
}
