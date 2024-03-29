using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectsVolumeController : MonoBehaviour
{
    private Slider slider;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateSoundEffectsVolume);
    }

    private void UpdateSoundEffectsVolume(float value)
    {
        audioManager.SetSoundFXvolume(value);
        audioManager.PlaySoundEffect(0); // number of array on clip
    }
}