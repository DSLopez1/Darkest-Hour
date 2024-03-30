using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicController : MonoBehaviour
{
    private Slider slider;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateBackgroundVolume);
    }

    private void UpdateBackgroundVolume(float value)
    {
        audioManager.SetLvlMusicVolume(value);
    }
}
