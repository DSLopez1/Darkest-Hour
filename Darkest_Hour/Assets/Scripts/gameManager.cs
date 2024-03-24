using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem.Composites;
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
    [SerializeField] private GameObject _menuDied;
    [SerializeField] private GameObject _menuAbility;
    [SerializeField] private GameObject _itemMenu;
    [SerializeField] TMP_Text enemyCountText;
    [SerializeField] TMP_Text _livesCountText;
    public ButtonFunctions buttons;

    [Header("-----Boss------")]
    [SerializeField] public GameObject bossUI;
    [SerializeField] public TMP_Text bossText;
    [SerializeField] public Image bossHP;

    [Header("-----player------")]

    [SerializeField] public GameObject player;
    [SerializeField] public Player playerScript;
    [SerializeField] private int _lives;

    public TempCameraController PlayerCam;
    public Image playerHPBar;


    public GameObject playerDamageFlash;
    public Animator armAnim;

    [Header("-----AbilityInterface------")]

    public List<Ability> abilities = new List<Ability>();
    public List<GameObject> abilityImages = new List<GameObject>();
    public List<Image> coolDownImages = new List<Image>();

    [Header("ItemFrame")]
    public List<GameObject> itemsUI = new List<GameObject>();
    public List<Item> allItems = new List<Item>();
    public List<Item> itemCopy = new List<Item>();

    [Header("-----Inputs------")]
    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameObject _pauseMenuFirst;
    [SerializeField] GameObject _deathMenuFirst;
    private bool MenuOpenCloseInput;
    private InputAction _menuOpenCloseAction;

    private bool _isPaused;
    int enemyCount;

    // Start is called before the first frame update
    private void Awake()
    {
        buttons = GetComponent<ButtonFunctions>();
        if (instance == null)
        {
            instance = this;
        }
        

        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        playerScript.updatePlayerUI();

        PlayerCam = Camera.main.GetComponent<TempCameraController>();
        armAnim = GameManager.instance.playerScript.arm.GetComponent<Animator>();
        _livesCountText.text = _lives.ToString("F0");
        itemCopy = allItems.ToList();

        // Input Initialized
        _menuOpenCloseAction = playerInput.actions["MenuOpenClose"];

    }

    // Update is called once per frame
    private void Update()
    {
        MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
        if (MenuOpenCloseInput && _menuActive == null)  //(Input.GetButtonDown("Cancel") && _menuActive == null)
        {

            StatePaused();
            _menuActive = _menuPause;
            _menuActive.SetActive(_isPaused);
            EventSystem.current.SetSelectedGameObject(_pauseMenuFirst);
            //EventSystem.current.firstSelectedGameObject = _pauseMenuFirst;
        }
        else if (MenuOpenCloseInput && _menuActive != null)
        {
            StateUnpaused();
        }

        if (Input.GetKeyDown(KeyCode.Q) && _menuActive == null)
        {
            StatePaused();
            _menuActive = _menuAbility;
            _menuAbility.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.I) && _menuActive == null)
        {
            StatePaused();
            _menuActive = _itemMenu;
            _itemMenu.SetActive(true);
        }
    }

    public void StatePaused()
    {
        _isPaused = !_isPaused;
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
        EventSystem.current.SetSelectedGameObject(null);
        _menuActive = null;
    }

    public void CompleteLevel(int amount)
    {
        enemyCount += amount;
        enemyCountText.text = enemyCount.ToString("F0");

        if (enemyCount <= 0)
        {
            LevelManager.instance.EndOfLevel();
        }
    }

    public void YouWin()
    {
        SceneManager.LoadScene("YouWin_Credits");
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
            EventSystem.current.SetSelectedGameObject(_deathMenuFirst);
        }
        else
        {
            youLose();
        }

    }

    public void youLose()
    {
        SceneManager.LoadScene("GameOver!");
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
            }
        }
    }

    public void UpdateItemUI()
    {
        for (int i = 0; i < 5; i++)
        {
            Sprite sprite = itemsUI[i].GetComponent<Image>().sprite;

            if (sprite != null)
            {
                itemsUI[i].SetActive(true);
            }
            else
            {
            }
        }
    }
}
