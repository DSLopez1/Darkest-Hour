using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour, IDamage, IPhysics
{
    [Header("----- Componenets -----")]
    [SerializeField] public Transform firePos;
    [SerializeField] public Transform shootPos;
    [SerializeField] public Transform orientation;
    [SerializeField] private GameObject _auto;

    [Header("----- Player Stats -----")]
    [SerializeField] public float playerSpeed = 5f;
    [SerializeField] public float shootDistance;
    [SerializeField] public float gravity;
    [SerializeField] private float _rotationSpeed = 500f;
    [SerializeField] private float _physResolve;
    [SerializeField] private int _hpMax;
    public float attackSpeed;
    private int _HP;

    [Header("-----Abilities-----")] 
    public List<AbilityHolder> abilities = new List<AbilityHolder>();


    Quaternion targetRotation;


    CameraController cameraController;
    Animator animator;
    CharacterController _controller;

    private Vector3 _moveDir;
    private Vector3 _pushBack;
    private Vector3 _playerVel;
    public Vector3 targetObjPosition;

    private bool _isShooting;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        _HP = _hpMax;

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
        if (Input.GetButtonDown("Attack") && !_isShooting)
        {
            Debug.Log("Shooting");
            StartCoroutine(shootProjectile(_auto));
        }
    }
    IEnumerator shootProjectile(GameObject obj)
    {
        _isShooting = true;
        Quaternion rotation = Camera.main.transform.rotation;
        rotation.y = 0;
        Instantiate(_auto, shootPos.position, Camera.main.transform.rotation);
        yield return new WaitForSeconds(attackSpeed);
        _isShooting = false;
    }

    public void TakeDamage(int amount)
    {
        // Take damage
        _HP -= amount;

        // Update UI to reflect current HP

        if (_HP < 0)
        {
            // Talk to game manager to lose or respawn based on lives left
            GameManager.instance.youLose();

            Debug.Log("You died!"); // Delete once we implement the above code

        }
    }

    public Vector3 getMoveVec()
    {
        return _moveDir;
    }

    public void PhysicsDir(Vector3 dir)
    {
        _pushBack += dir;
    }

    private void Movement()
    {
        _pushBack = Vector3.Lerp(_pushBack, Vector3.zero, _physResolve);

        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        if (_controller.isGrounded)
        {
            _playerVel = Vector3.zero;
        }

        _playerVel.y += gravity;

        _moveDir = orientation.forward * vInput + orientation.right * hInput;

        _controller.Move(_moveDir.normalized * playerSpeed * Time.deltaTime);
        _controller.Move((_playerVel + _pushBack) * Time.deltaTime);

    }
   
}