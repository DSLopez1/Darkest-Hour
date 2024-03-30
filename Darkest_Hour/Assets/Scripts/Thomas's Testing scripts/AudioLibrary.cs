using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Custom/Audio Library")] 
public class AudioLibrary : ScriptableObject
{
    public AudioClip[] backgroundMusic;
    public AudioClip[] soundEffects;
}
