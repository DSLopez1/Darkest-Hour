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
    private float progress_Multiplier_1 = 0.5f;
    private float progress_Multiplier_2 = 0.07f;

    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        
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
    }

}
