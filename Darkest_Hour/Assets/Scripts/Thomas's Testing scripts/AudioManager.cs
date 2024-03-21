using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip mainMenuMusicClip;
   

    private AudioSource mainMenuMusic;
    private AudioSource levelMusic;
    private AudioSource soundEffectSource;

    private float overallVolume = 1f;
    private float soundEffectsVolume = 1f;

    private Dictionary<string, AudioClip> levelMusicClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);


            mainMenuMusic = gameObject.AddComponent<AudioSource>();
            mainMenuMusic.clip = mainMenuMusicClip;
            
            levelMusic = gameObject.AddComponent<AudioSource>();

            soundEffectSource = gameObject.AddComponent<AudioSource>();


            
            //levelMusicClips.Add("TeamLogo", Resources.Load<AudioClip>("Fireball_Clip"));
            levelMusicClips.Add("Tutorial Level", Resources.Load<AudioClip>("Tutorial_Level_Clip"));
            //levelMusicClips.Add("Outside City (Lvl 1)", Resources.Load<AudioClip>("Level1_Clip"));
            //levelMusicClips.Add("Catacombs", Resources.Load<AudioClip>("Level1_Clip"));
            //levelMusicClips.Add("Throne Room", Resources.Load<AudioClip>("War_Drums"));
            //levelMusicClips.Add("Dragon Cave", Resources.Load<AudioClip>("BossFight"));
            levelMusicClips.Add("YouWin_Credits", Resources.Load<AudioClip>("YouWin!_Clip"));
            levelMusicClips.Add("GameOver!", Resources.Load<AudioClip>("GameOver!_Clip"));

            
            soundEffects.Add("ButtonClick", Resources.Load<AudioClip>("ButtonClick"));
            //soundEffects.Add("Hit", Resources.Load<AudioClip>("Hit_Clip"));
            //soundEffects.Add("Die", Resources.Load<AudioClip>("FemaleGrunt_Clip"));
            //soundEffects.Add("spawnPortal", Resources.Load<AudioClip>("Teleport_Clip"));
            //soundEffects.Add("Respawn", Resources.Load<AudioClip>("Respawn_Clip"));
            soundEffects.Add("ItemPickUp", Resources.Load<AudioClip>("ItemEquip"));

            if (SceneManager.GetActiveScene().name == "MainMenu")
            {

                mainMenuMusic.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

 
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        if (levelMusic.isPlaying)
        {
            levelMusic.Stop();
        }

       if (levelMusicClips.ContainsKey(sceneName))
       {
            mainMenuMusic.Stop();
            levelMusic.clip = levelMusicClips[sceneName];
            levelMusic.Play();
            levelMusic.loop = true;
       }

    }

    public void PlaySoundEffect(string soundEffectKey)
    {
        if (soundEffects.ContainsKey(soundEffectKey))
        {
            soundEffectSource.PlayOneShot(soundEffects[soundEffectKey]);
        }
        else
        {
            Debug.LogWarning("Sound effect with key " + soundEffectKey + " not found.");
        }
    }

    public void SetBackgroundVolume(float volume)
    {
        mainMenuMusic.volume = volume;
        levelMusic.volume = volume;
    }

    public void SetSoundEffectsVolume(float volume)
    {
        if (overallVolume <= 0f)
        {
            soundEffectSource.volume = volume;
        }
        else
        {
            soundEffectSource.volume = overallVolume * volume;
        }
    }

    public void SetOverallVolume(float volume)
    {
        mainMenuMusic.volume = volume;
        levelMusic.volume = volume;
        soundEffectSource.volume = volume;
    }


    public void RestartGameFromLevel1()
    {
        mainMenuMusic.Stop();

        // Play level 1 music
        if (levelMusicClips.ContainsKey("Level1"))
        {
            levelMusic.clip = levelMusicClips["Level1"];
            levelMusic.loop = true;
            levelMusic.Play();
        }
    }

}