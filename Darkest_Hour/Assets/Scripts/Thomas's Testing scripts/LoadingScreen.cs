using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar;
    public float updateDelay = 0.1f;

    void Start()
    {
        StartCoroutine(UpdateProgressBar());
    }

    IEnumerator UpdateProgressBar()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Outside City (Lvl 1)");
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressBar.value = progress;

            if (asyncLoad.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f);
                asyncLoad.allowSceneActivation = true;
            }

            yield return new WaitForSeconds(updateDelay);
        }
    }
}

