using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioLibrary audioLibrary; // referencing audiolibrary

    private float MainVolume = 1f;
    private float backgroundVolume = 1f;
    private float SFXvolume = 1f;

    private AudioSource bgMusicSource; // referencing bglvl music
    private Dictionary<string, AudioClip> lvlMusicMap;

    private AudioSource soundFXsource; // referencing sfx

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);

            return;
        }
         

        instance = this;
        DontDestroyOnLoad(gameObject);

        Initialize();
    }

    private void Initialize()
    {
        bgMusicSource = gameObject.AddComponent<AudioSource>(); // add clips for background music
        bgMusicSource.loop = true;
        soundFXsource = gameObject.AddComponent<AudioSource>(); // add clips for sfx
        InitializelvlMusicMap();
        PlayBGmusicInLvl(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //setting clip to scenename to play
    private void InitializelvlMusicMap()
    {
        lvlMusicMap = new Dictionary<string, AudioClip>();
        foreach (var levelMusic in audioLibrary.backgroundMusic)
        {
            string sceneName = levelMusic.name;
            lvlMusicMap[sceneName] = levelMusic;
        }
    }
    // play lvl music make sure music clip is same name as scene name
    private void PlayBGmusicInLvl(string sceneName)
    {
        if(lvlMusicMap.ContainsKey(sceneName))
        {
            AudioClip clip = lvlMusicMap[sceneName];
            bgMusicSource.clip = clip;
            bgMusicSource.Play();
        }
    }
    // play audio clips when scene loads
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGmusicInLvl(scene.name);
    }
    // play sfx audio from array in audio library
    public void PlaySoundEffect(int index)
    {
        if(index < 0 || index >= audioLibrary.soundEffects.Length)
            return;

        AudioClip clip = audioLibrary.soundEffects[index];
        soundFXsource.PlayOneShot(clip, SFXvolume);
    }
    // setting lvl music volume
    public void SetLvlMusicVolume(float volume)
    {
        backgroundVolume = volume;
        bgMusicSource.volume = MainVolume * backgroundVolume;
    }
    // setting sfx volume
    public void SetSoundFXvolume(float volume)
    {
        SFXvolume = volume;
    }
    // setting main volume
    public void SetMainVolume(float volume)
    {
        MainVolume = volume;
        bgMusicSource.volume = MainVolume * backgroundVolume;
        soundFXsource.volume = MainVolume * SFXvolume;
    }
}
