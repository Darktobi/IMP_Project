using UnityEngine;
using UnityEngine.UI;

public class BarHandler : MonoBehaviour {

    [SerializeField]
    private Player player;

    public Image apBar;
    public Image foodBar;
    public Image healthBar;

    public Text apText;
    public Text foodText;
    public Text healthText;

    private void Update ()
    {
        healthBar.fillAmount = player.getHealth() / player.getHealthMax();
        apBar.fillAmount = player.getAp() / player.getApMax();
        foodBar.fillAmount = player.getFood() / player.getFoodMax();

        foodText.text = player.getFood() + "/" + player.getFoodMax() + " FP";
        apText.text = player.getAp() + "/" + player.getApMax() + " AP";
        healthText.text = player.getHealth() + "/" + player.getHealthMax() + " HP";
    }

}
