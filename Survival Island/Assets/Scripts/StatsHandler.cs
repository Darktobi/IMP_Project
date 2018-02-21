using UnityEngine;
using UnityEngine.UI;

public class StatsHandler : MonoBehaviour {

    [SerializeField]
    private Player player;

    public Text strength;
    public Text constitution;
    public Text agility;
    public Text wisdom;

	private void Update () {
        strength.text = player.getStr().ToString();
        constitution.text = player.getCon().ToString();
        agility.text = player.getAgi().ToString();
        wisdom.text = player.getWis().ToString();
    }
}
