using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour {

    public Slider soundSlider;
    public Slider musicSlider;
    public Text soundValue;
    public Text musicValue;

    private void Start ()
    {
        if (PlayerPrefs.HasKey("music Volume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("music Volume");
        }
        if (PlayerPrefs.HasKey("sound Volume"))
        {
            soundSlider.value = PlayerPrefs.GetFloat("sound Volume");
        }

        soundValue.text = soundSlider.value.ToString();
        musicValue.text = musicSlider.value.ToString();

        soundSlider.onValueChanged.AddListener(delegate {
            valueChangeCheck();
        });

        musicSlider.onValueChanged.AddListener(delegate {
            valueChangeCheck();
        });
    }

    private void valueChangeCheck()
    {
        soundValue.text = soundSlider.value.ToString();
        musicValue.text = musicSlider.value.ToString();

        AudioListener.volume = soundSlider.value / 100;

        PlayerPrefs.SetFloat("music Volume", musicSlider.value);
        PlayerPrefs.SetFloat("sound Volume", soundSlider.value);
    }
}
