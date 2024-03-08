using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject _menuActive;
    [SerializeField] private GameObject _menuPause;
    [SerializeField] private GameObject _menuShop;

    [Header("-----player------")] 

    [SerializeField] public GameObject player;
    [SerializeField] public PlayerController playerScript;

    [Header("-----AbilityUI------")]
    [SerializeField] public Image ability1Image;
    [SerializeField] public Image ability2Image;

    private bool _isPaused;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();


    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && _menuActive == null)
        {
            StatePaused();
            _menuActive = _menuPause;
            _menuActive.SetActive(_isPaused);
        }

        if (Input.GetKeyDown(KeyCode.Q) && _menuActive == null)
        {
            StatePaused();
            _menuActive = _menuShop;
            _menuShop.SetActive(true);
        }
    }

    public void StatePaused()
    {
        _isPaused = !_isPaused;
        Debug.Log("paused");
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void StateUnpaused()
    {
        _isPaused = !_isPaused;

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _menuActive.SetActive(false);
        _menuActive = null;
    }
}
