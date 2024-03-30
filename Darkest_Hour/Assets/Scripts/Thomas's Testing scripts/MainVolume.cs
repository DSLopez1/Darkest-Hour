using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainVolume : MonoBehaviour
{
    private Slider slider;
    private AudioManager audioManager;
    private string volumeKey = "MainVolume";

    private void Start()
    {
        audioManager = AudioManager.instance;
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ChangeMainVolume);

        // get saved main volume
        float savedVolume = PlayerPrefs.GetFloat(volumeKey, 1f);
        slider.value = savedVolume;
        ChangeMainVolume(savedVolume); // use saved volume
    }
    // set volume for sfx and lvl 
    private void ChangeMainVolume(float value)
    {
       audioManager.SetMainVolume(value);

       // save mainvolume as player prefs
       PlayerPrefs.SetFloat(volumeKey, value);
       PlayerPrefs.Save();
    }
}
