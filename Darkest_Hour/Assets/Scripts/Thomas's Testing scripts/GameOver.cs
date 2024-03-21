using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string level1SceneName = "Outside City (Lvl 1)";


    public void RestartGameFromLevel1ButtonClicked()
    {
        // Call the RestartGameFromLevel1 method of the AudioManager to reset the audio
        AudioManager.Instance.RestartGameFromLevel1();

        
    }
    // restart the game from level1
    public void RestartGame()
    {
        SceneManager.LoadScene("Outside City (Lvl 1)");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
   
}

