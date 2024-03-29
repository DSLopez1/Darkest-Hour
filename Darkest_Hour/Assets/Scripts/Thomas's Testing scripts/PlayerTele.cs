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
            AudioManager.instance.PlaySoundEffect(2);
            GameObject player = other.gameObject;
            DontDestroyOnLoad(player);

            GameObject UI = GameObject.FindGameObjectWithTag("UI");
            if (UI != null)
            {
                DontDestroyOnLoad (UI);
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
