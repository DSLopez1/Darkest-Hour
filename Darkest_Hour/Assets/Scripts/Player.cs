using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour, IDamage, IPhysics
{
    [Header("----- Componenets -----")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private GameObject _targeterObject;
    [SerializeField] public Transform shootPos;

    [Header("----- Player Stats -----")] 
    [SerializeField] public int _HP;
    [SerializeField] public float playerSpeed;
    [SerializeField] public int jumpMax;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _shootDistance;
    [SerializeField] public float gravity;
    [SerializeField] private float _sprintMod;
    [SerializeField] private float _pushBackResolution;

    [Header("-----AbilityPos-----")] 
    [SerializeField] public Transform firePos;
    public List<AbilityHolder> abilities = new List<AbilityHolder>();

    private Vector3 _move;
    private Vector3 _playerVelocity;
    private Vector3 _pushBack;
    private int _jumpCount;
    private bool _isShooting;
    public bool gravOn = true;
    int _HPOrig;
    int _Lives = 3;

    public Vector3 targetObjPosition;

    private void Start()
    {
        _HPOrig = _HP;
        _Lives = 3;
        _controller = GetComponent<CharacterController>();
        respawn();

        for (int i = 0; i < 4; i++)
        {
            GameObject abilityHolderObject = new GameObject("AbilityHolderObject");
            AbilityHolder abilityHolder = abilityHolderObject.AddComponent<AbilityHolder>();
            abilities.Add(abilityHolder);
        }
    }

    private void Update()
    {
        Movement();
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * _shootDistance, Color.blue);

        if (Input.GetButtonDown("Fire2") && !_isShooting)
        {
            StartCoroutine(shootRay());
        }
    }

    private void Movement()
    {

        _pushBack = Vector3.Lerp(_pushBack, Vector3.zero, _pushBackResolution * Time.deltaTime);

        if (_controller.isGrounded)
        {
            _jumpCount = 0;
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


    IEnumerator shootProjectile(GameObject obj)
    {
        _isShooting = true;

        GameObject instObj = Instantiate(obj, shootPos.position, Camera.main.transform.rotation);

        yield return new WaitForSeconds(1);
        _isShooting = false;
    }

    IEnumerator shootRay()
    {
        _isShooting = true;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, _shootDistance))
        {
            targetObjPosition = hit.point;
        }
        yield return new WaitForSeconds(1);

        _isShooting = false;
    }

    void updatePlayerUI()
    {
        GameManager.instance.playerHPBar.fillAmount = (float)_HP / _HPOrig;
    }

    public void respawn()
    {
        _pushBack = Vector3.zero;
        _HP = _HPOrig;
        updatePlayerUI();

        _controller.enabled = false;
        transform.position = GameManager.instance.playerSpawnPos.transform.position;
        _controller.enabled = true;
    }


    public void TakeDamage(int amount)
    {
        _HP -= amount;
        updatePlayerUI();
        StartCoroutine(flashDamage());

        if (_Lives <= 0)
        {
            GameManager.instance.youLose();
        }

    }

    public void PhysicsDir(Vector3 dir)
    {
        _pushBack += dir;
    }
    public Vector3 getMoveVec() { return _move; }
    


  

    IEnumerator flashDamage()
    {
        GameManager.instance.playerDamageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.playerDamageFlash.SetActive(false);
    }
     public Vector3 getMoveVec() { return _move; }
    


}
