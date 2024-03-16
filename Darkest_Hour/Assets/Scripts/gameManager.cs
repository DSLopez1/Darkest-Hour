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
    [SerializeField] private GameObject _menuDied;
    [SerializeField] TMP_Text enemyCountText;
    [SerializeField] TMP_Text _livesCountText;


    [Header("-----player------")]

    [SerializeField] public GameObject player;
    [SerializeField] public Player playerScript;
    [SerializeField] private int _lives;
    public TempCameraController PlayerCam;
    public Image playerHPBar;
    public GameObject playerSpawnPos;
    public GameObject spawnPortal;
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
        playerScript = player.GetComponent<Player>();
        PlayerCam = Camera.main.GetComponent<TempCameraController>();
        playerSpawnPos = GameObject.FindWithTag("playerSpawnPos");
        _livesCountText.text = _lives.ToString("F0");
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

    public void YouDied()
    {
        _lives -= 1;
        _livesCountText.text = _lives.ToString("F0");
        if (_lives > 0)
        {
            StatePaused();
            _menuActive = _menuDied;
            _menuActive.SetActive(true);  
        }
        else
        {
            youLose();
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
