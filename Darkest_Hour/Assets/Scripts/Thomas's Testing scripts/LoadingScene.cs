using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public static LoadingScene instance;

    [SerializeField]
    private GameObject loading_Bar_Holder;

    [SerializeField]
    private Image loading_Bar_Progress;

    private float progress_Value = 1.1f;
    public float progress_Multiplier_1 = 0.5f;
    public float progress_Multiplier_2 = 0.07f;

    public float load_Level_Time = 2f;
    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        StartCoroutine(LoadingSomeLevel());
    }

    void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadLevel(string levelName)
    {
        loading_Bar_Holder.SetActive(true);

        progress_Value = 0f;

        //Time.timescale = 0f;

        SceneManager.LoadScene(levelName);
    }

    void ShowLoadingScene()
    {
        if(progress_Value > 1f)
        {
            progress_Value += progress_Multiplier_1 * progress_Multiplier_2;
            loading_Bar_Progress.fillAmount = progress_Value;

            if(progress_Value >= 1f)
            {
                progress_Value = 1.1f;

                loading_Bar_Progress.fillAmount = 0f;

                loading_Bar_Holder.SetActive(false);

                //Time.timeScale = 1f;
            }
        } // if progress value <1

    }

    IEnumerator LoadingSomeLevel()
    {
        yield return new WaitForSeconds(load_Level_Time);

        //LoadLevel("Tutorial level"); 
        LoadLevelAsync("Tutorial level");
    }

    public void LoadLevelAsync(string levelName)
    {
        StartCoroutine(LoadAsynchronously(levelName));
    }

    IEnumerator LoadAsynchronously(string levelName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        loading_Bar_Holder.SetActive(true);

        //while the operation is Not Done
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;

            loading_Bar_Progress.fillAmount = progress;

            if(progress >= 1f)
            {
                loading_Bar_Holder.SetActive(false);
            }
        }

        yield return null;
    }

}
