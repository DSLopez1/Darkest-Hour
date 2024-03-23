using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    /* Audio Mamager - in the main menu scene create a (empty 3D object) and attach the audio manager script to the object, make sure your Audio is in a file called Resources Or 
     * if its in a file called Audio change from Resources.Load<AudioClip> to Audio.Load<AudioClip> 
     * just make sure the folder where all the audio is stored you use the EXACT name in the ____.Load<AudioClip>, also for example if my attack animations was called "Slash_Clip" 
     * when i add the sound to my library in the Audio Manager it would look like soundEffects.Add("Attack", Resources.Load<AudioClip>("Slash_Clip"));
     * make sure Exactly how you have the audio clip labeled its the same Exact way in here..including caps bc its case sensitive. here's a lil breakdown.
 soundEffects.Add("Attack", Resources.Load<AudioClip>("Attack_Clip"));
                   ^          ^                            ^
       trigger/function     Exact folder               Exact name that
      that triggers the     name audio is           the audio clip is named            > CASE SENSITIVE!
              audio           stored                  in the folder u keep the 
							  audio
    also if your working in a scene and it gives you a null refernce error or the portal doesnt work just make a empty game object and put the audio manager script on it and ur good to go, 
    just remember to delete it before playing through the game */


    public static AudioManager Instance;
    public AudioClip mainMenuMusicClip;

    [Header("----AudioSources----")]
    private AudioSource mainMenuMusic;
    private AudioSource levelMusic;
    private AudioSource soundEffectSource;

    private float overallVolume = 1f;
    private float backgroundVolume = 1f;
    private float soundEffectsVolume = 1f;

    private Dictionary<string, AudioClip> levelMusicClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance == null) //update awake method
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            mainMenuMusic = gameObject.AddComponent<AudioSource>();
            mainMenuMusic.clip = mainMenuMusicClip;

            levelMusic = gameObject.AddComponent<AudioSource>();
            soundEffectSource = gameObject.AddComponent<AudioSource>();


            //levelMusicClips.Add("TeamLogo", Resources.Load<AudioClip>("Fireball_Clip"));
            levelMusicClips.Add("Tutorial level", Resources.Load<AudioClip>("Tutorial_Level_Clip"));
            //levelMusicClips.Add("Outside City (Lvl 1)", Resources.Load<AudioClip>("Level1_Clip"));
            //levelMusicClips.Add("Catacombs", Resources.Load<AudioClip>("Level1_Clip"));
            //levelMusicClips.Add("Throne Room", Resources.Load<AudioClip>("War_Drums"));
            //levelMusicClips.Add("Dragon Cave", Resources.Load<AudioClip>("BossFight"));
            levelMusicClips.Add("YouWin_Credits", Resources.Load<AudioClip>("YouWin!_Clip"));
            levelMusicClips.Add("GameOver!", Resources.Load<AudioClip>("GameOver!_Clip"));


            soundEffects.Add("ButtonClick", Resources.Load<AudioClip>("ButtonClick"));
            soundEffects.Add("TeamLogo", Resources.Load<AudioClip>("Fireball_Clip"));
            //soundEffects.Add("Hit", Resources.Load<AudioClip>("Hit_Clip"));
            //soundEffects.Add("Die", Resources.Load<AudioClip>("FemaleGrunt_Clip"));
            //soundEffects.Add("spawnPortal", Resources.Load<AudioClip>("Teleport_Clip"));
            //soundEffects.Add("Respawn", Resources.Load<AudioClip>("Respawn_Clip"));
            soundEffects.Add("ItemPickUp", Resources.Load<AudioClip>("ItemEquip"));

            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
               
                mainMenuMusic.Play();
            }
            else
            {
                Destroy(gameObject);
            }

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
        backgroundVolume = volume;
        mainMenuMusic.volume = overallVolume * backgroundVolume;
        levelMusic.volume = overallVolume * backgroundVolume;
    }

    public void SetSoundEffectsVolume(float volume)
    {
        soundEffectsVolume = volume;
        soundEffectSource.volume = overallVolume * soundEffectsVolume;
    }

    public void SetOverallVolume(float volume)
    {
        overallVolume = volume;
        mainMenuMusic.volume = overallVolume * backgroundVolume;
        levelMusic.volume = overallVolume * backgroundVolume;
        soundEffectSource.volume = overallVolume * soundEffectsVolume;
    }


    public void RestartGameFromLevel1()
    {
        mainMenuMusic.Stop();

        // Play level 1 music
        if (levelMusicClips.ContainsKey("Level1_Clip"))
        {
            levelMusic.clip = levelMusicClips["Level1_Clip"];
            levelMusic.loop = true;
            levelMusic.Play();
        }
    }

}