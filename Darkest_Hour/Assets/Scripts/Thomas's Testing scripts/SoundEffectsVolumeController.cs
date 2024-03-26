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
        audioManager = AudioManager.Instance;
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateSoundEffectsVolume);
    }

    private void UpdateSoundEffectsVolume(float value)
    {
        audioManager.SetSoundEffectsVolume(value);
        audioManager.PlaySoundEffect("Hit");
        audioManager.PlaySoundEffect("Fireball_Clip");
    }
}