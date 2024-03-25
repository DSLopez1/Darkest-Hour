using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] public GameObject spawnPortal;
    [SerializeField] public GameObject chest;
    [SerializeField] public GameObject playerSpawnPos;

    [SerializeField] public GameObject player;
    [SerializeField] public Player playerScript;

    void Awake()
    {
        instance = this;
        spawnPortal = GameObject.FindWithTag("Portal");
        if (spawnPortal != null )
        {
            spawnPortal.SetActive(false);
        }
        chest = GameObject.FindWithTag("Chest");
        if (chest != null )
        {
            chest.SetActive(false);
        }
        playerSpawnPos = GameObject.FindWithTag("playerSpawnPos");

        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        playerScript.PlayerSpawn();
    }

    public void EndOfLevel()
    {
        spawnPortal.SetActive(true);
        chest?.SetActive(true);
    }
}
