using UnityEngine;
using UnityEngine.UI;

public class SettingsPage : MonoBehaviour
{
    [SerializeField] private Text volumeTextVal = null;
    [SerializeField] private Slider volumeSlider = null;
    
    PlayerPrefs playerPrefs;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            float volume = PlayerPrefs.GetFloat("Volume", 1);
            
            volumeTextVal.text = (volume * 100f).ToString("0") + "%";
            volumeSlider.value = volume;
        }
    }
    
    public void SetVolume()
    {
        float volume = volumeSlider.value;
        AudioListener.volume = volume;
        volumeTextVal.text = (volume * 100f).ToString("0") + "%";
    }
    
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", AudioListener.volume);
    }
}
