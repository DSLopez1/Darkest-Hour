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
        SceneManager.LoadScene("Outside City (Lvl 1)");
    }

    public void OnTutorialButtonClicked()
    {
        PlayButtonClickSound();
        Tutorial();
        
    }

    public void OnOptionsButtonClicked()
    {
        PlayButtonClickSound();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
            buttonClickSound.Play();
    }

    public void Tutorial()
    {
        StartCoroutine(LoadTutorialWithDelay(2f));
    }

    private IEnumerator LoadTutorialWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Tutorial level");
    }
}
