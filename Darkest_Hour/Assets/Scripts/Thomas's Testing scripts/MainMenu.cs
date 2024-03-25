using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource buttonClickSound;
    
    public void OnPlayButtonClicked()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene("Main Story");
    }


    public void OnOptionsButtonClicked()
    {
        PlayButtonClickSound();
    }

    public void QuitGame()
    {
        PlayButtonClickSound();
        Application.Quit();
    }

    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
            buttonClickSound.Play();
    }
}
