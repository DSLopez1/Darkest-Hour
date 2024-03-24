using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] public GameObject spawnPortal;
    [SerializeField] public GameObject chest;
    [SerializeField] public GameObject playerSpawnPos;

    void Awake()
    {
        instance = this;
        spawnPortal = GameObject.FindWithTag("Portal");
        chest = GameObject.FindWithTag("Chest");
        playerSpawnPos = GameObject.FindWithTag("playerSpawnPos");
    }

    public void EndOfLevel()
    {
        spawnPortal.SetActive(true);
        chest.SetActive(true);
    }
}
