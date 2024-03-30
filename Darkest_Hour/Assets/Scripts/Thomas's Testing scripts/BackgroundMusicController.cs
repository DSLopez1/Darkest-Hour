using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicController : MonoBehaviour
{
    private Slider slider;
    private AudioManager audioManager;
    private string bgMusicVolumeKey = "BackgroundMusicVolume";


    void Start()
    {
        audioManager = AudioManager.instance;
        slider = GetComponent<Slider>();

        // get saved backgroundmusic volume 
        slider.onValueChanged.AddListener(UpdateBackgroundVolume);
        float savedVolume = PlayerPrefs.GetFloat(bgMusicVolumeKey, 1f);
        slider.value = savedVolume;
        UpdateBackgroundVolume(savedVolume); // used saved volume
    }
    // set backgroundmusic 
    private void UpdateBackgroundVolume(float value)
    {
        audioManager.SetLvlMusicVolume(value);

        // saved volume as player prefs
        PlayerPrefs.SetFloat(bgMusicVolumeKey, value);
        PlayerPrefs.Save();
    }
}
