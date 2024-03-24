using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour, IDamage, IPhysics
{
    [Header("----- Components -----")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] public Transform shootPos;
    public TempCameraController playerCam;
    [SerializeField] public GameObject arm;
    [SerializeField] public ParticleSystem rainSystem;


    [Header("----- Player Stats -----")] 
    [SerializeField] public int HP;
    [SerializeField] public float playerSpeed;
    [SerializeField] private float _shootDistance;
    [SerializeField] public float gravity;
    [SerializeField] private float _pushBackResolution;
    public float damageMitigation;
    public float coolDownReduction;

    [Header("-----AbilityPos-----")] 
    [SerializeField] public Transform firePos;

    public float dashCooldown;
    public List<AbilityHolder> abilities = new List<AbilityHolder>();
    public Vector3 targetPos;

    [Header("Items")]
    public List<Item> items = new List<Item>();

    private Vector3 _move;
    private Vector3 _playerVelocity;
    private Vector3 _pushBack;
    public bool gravOn = true;
    public int _HPOrig;
    

    public Vector3 targetObjPosition;

    private void Start()
    {
        _HPOrig = HP;
        _controller = GetComponent<CharacterController>();
        respawn();
        for (int i = 0; i < 4; i++)
        {
            AbilityHolder abilityHolder = gameObject.AddComponent<AbilityHolder>();
            abilities.Add(abilityHolder);
        }
    }

    public void MakeAbilityHolder()
    {
        if (abilities.Count > 0)
        {
            abilities.Clear();
        }
        for (int i = 0; i < 4; i++)
        {
            //GameObject abilityHolderObject = new GameObject("AbilityHolderObject");
            AbilityHolder abilityHolder = gameObject.AddComponent<AbilityHolder>();
            abilities.Add(abilityHolder);
        }
    }

    private void Update()
    {
        
        Movement();
    }

    private void Movement()
    {

        _pushBack = Vector3.Lerp(_pushBack, Vector3.zero, _pushBackResolution * Time.deltaTime);

        if (_controller.isGrounded)
        {
            _playerVelocity = Vector3.zero;
        }
        _move = Input.GetAxis("Horizontal") * transform.right
             + Input.GetAxis("Vertical") * transform.forward;

        _controller.Move(_move * playerSpeed * Time.deltaTime);


        if (gravOn)
        {
            _playerVelocity.y -= gravity * Time.deltaTime;
        }
        _controller.Move((_playerVelocity + _pushBack) * Time.deltaTime);
    }

    public void updatePlayerUI()
    {
        GameManager.instance.playerHPBar.fillAmount = (float)HP / _HPOrig;
    }

    public void respawn()
    {
        _pushBack = Vector3.zero;
        HP = _HPOrig;
        updatePlayerUI();

        _controller.enabled = false;
        transform.position = LevelManager.instance.playerSpawnPos.transform.position;
        _controller.enabled = true;
    }

    public void TakeDamage(int amount)
    {
        float reduction = (float)amount * damageMitigation;
        HP -= amount - (int)reduction;
        updatePlayerUI();
        StartCoroutine(flashDamage());
        
        if (HP <= 0)
        {
            GameManager.instance.YouDied();
        }

    }

    public void PhysicsDir(Vector3 dir)
    {
        _pushBack += dir;
    }
    

    IEnumerator flashDamage()
    {
        GameManager.instance.playerDamageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.playerDamageFlash.SetActive(false);
    }
     public Vector3 getMoveVec() { return _move; }

     public void applyItemStats(int index)
     {
        items[index].Initialize();
        items[index].addStats();
     }

    public void RainOn()
    {
        rainSystem.Play();
    }

    public void RainOff()
    {
        rainSystem.Stop();
    }
}
