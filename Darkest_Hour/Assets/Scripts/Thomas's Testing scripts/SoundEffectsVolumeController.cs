using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectsVolumeController : MonoBehaviour
{
    private Slider slider;
    private AudioManager audioManager;
    private string sfxVolumeKey = "SFXVolume";

    void Start()
    {
        audioManager = AudioManager.instance;
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateSoundEffectsVolume);

        // get saved sfx volume 
        float savedVolume = PlayerPrefs.GetFloat(sfxVolumeKey, 1f);
        slider.value = savedVolume;
        UpdateSoundEffectsVolume(savedVolume);

    }

    private void UpdateSoundEffectsVolume(float value)
    {
        audioManager.SetSoundFXvolume(value);
        audioManager.PlaySoundEffect(0); // number of array on clip

        //set sfx to player prefs and save
        PlayerPrefs.SetFloat(sfxVolumeKey, value);
        PlayerPrefs.Save();
    }
}