using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("-----Menus-----")]
    [SerializeField] private GameObject _menuActive;
    [SerializeField] private GameObject _menuPause;
    [SerializeField] private GameObject _menuLose;
    [SerializeField] private GameObject _menuWin;
    [SerializeField] private GameObject _menuShop;
    [SerializeField] private GameObject _menuAbility;
    [SerializeField] TMP_Text enemyCountText;


    [Header("-----player------")]

    [SerializeField] public GameObject player;
    [SerializeField] public PlayerController playerScript;
    [SerializeField] public CameraController PlayerCam;
    [SerializeField] private GameObject spawnPortal;
    public Image playerHPBar;
    public GameObject playerSpawnPos;
    public GameObject playerDamageFlash;
    


    [Header("-----AbilityInterface------")]

    public List<Ability> abilities = new List<Ability>();
    public List<GameObject> abilityImages = new List<GameObject>();
    public List<Image> coolDownImages = new List<Image>();

    private bool _isPaused;
    int enemyCount;
    int bossCount;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
        PlayerCam = Camera.main.GetComponent<CameraController>();
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
            _menuActive = _menuAbility;
            _menuAbility.SetActive(true);
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

    public void CompleteLevel(int amount)
    {
        enemyCount += amount;
        enemyCountText.text = enemyCount.ToString("F0");

        if (enemyCount <= 0)
        {
            Instantiate(spawnPortal, Vector3.zero, Quaternion.identity);
        }
    }
    
    public void updateGameGoal(int amount)
    {
        bossCount += amount;
        
        if(bossCount <= 0)
        {
            //you win
            _menuActive = _menuWin;
            _menuActive.SetActive(true);
            StatePaused();
        }
    }

    public void youLose()
    {
        StatePaused();

        _menuActive = _menuLose;
        _menuActive.SetActive(true);
    }

    public void UpdateAbilityUI()
    {
        for (int i = 0; i < 4; i++)
        {
            Sprite sprite = abilityImages[i].GetComponent<Image>().sprite;

            if (sprite != null)
            {
                abilityImages[i].SetActive(true);
            }
            else
            {
                Debug.Log("sprite is null");
            }
        }
    }
}
