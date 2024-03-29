using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainVolume : MonoBehaviour
{
    private Slider slider;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ChangeMainVolume);
    }

    private void ChangeMainVolume(float value)
    {
       audioManager.SetMainVolume(value);
    }
}
