using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTeleportPortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            DontDestroyOnLoad(player);

            GameObject[] healthBars = GameObject.FindGameObjectsWithTag("HealthBar");
            foreach (GameObject healthBar in healthBars)
            {
                DontDestroyOnLoad(healthBar);
            }

            GameObject livesCounter = GameObject.FindGameObjectWithTag("LivesCounter");
            if (livesCounter != null)
            {
                DontDestroyOnLoad(livesCounter);
            }

            

            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
