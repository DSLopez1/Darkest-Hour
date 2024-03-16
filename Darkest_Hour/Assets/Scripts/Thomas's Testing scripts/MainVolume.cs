using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainVolume : MonoBehaviour
{
    private Slider slider;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.Instance;
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateOverallVolume);
    }

    private void UpdateOverallVolume(float value)
    {
        audioManager.SetOverallVolume(value);
    }
}
