using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour, IDamage, IPhysics
{
    [Header("----- Componenets -----")]
    [SerializeField] public Transform firePos;
    [SerializeField] public Transform targetLocation;
    [SerializeField] public Transform shootPos;

    [Header("----- Player Stats -----")]
    [SerializeField] public float playerSpeed = 5f;
    [SerializeField] public float shootDistance;
    [SerializeField] private float _rotationSpeed = 500f;
    [SerializeField] private float physResolve;
    [SerializeField] public float gravity;
    [SerializeField] private int _hpMax;
    private int _HP;

    [Header("-----Abilities-----")] 
    public List<AbilityHolder> abilities = new List<AbilityHolder>();
    

    [Header("-----Abilities-----")]
    public List<AbilityHolder> abilities = new List<AbilityHolder>();


    Quaternion targetRotation;

    CameraController cameraController;
    Animator animator;
    CharacterController _controller;

    private Vector3 _moveDir;
    private Vector3 _pushBack;
    public Vector3 targetObjPosition;

    private bool _isShooting;
    public bool gravOn;

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
        
    }
    IEnumerator shootProjectile(GameObject obj)
    {
        _isShooting = true;
        GameObject instObj = Instantiate(obj, Camera.main.transform.position, Camera.main.transform.rotation);

        yield return new WaitForSeconds(3);
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

    void Movement()
    {
        _pushBack = Vector3.Lerp(_pushBack, Vector3.zero, physResolve * Time.deltaTime);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));

        var moveInput = (new Vector3(h, 0, v)).normalized;

        _moveDir = (cameraController.PlanarRotation * moveInput) * playerSpeed;

        //gravity
        if (!_controller.isGrounded)
        {
            _pushBack.y -= gravity * Time.deltaTime;
        }
        else
        {
            _pushBack.y = 0;
        }

        if (moveAmount > 0)
        {
            _controller.Move((_moveDir + _pushBack) * Time.deltaTime);

            targetRotation = Quaternion.LookRotation(_moveDir);
        }


        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);
    }
    public void callMove(Vector3 startPos, Vector3 endPos, float moveSpeed)
    {
        StartCoroutine(moveToPos(startPos, endPos, moveSpeed));
    }

    IEnumerator moveToPos(Vector3 startPos, Vector3 endPos, float moveSpeed)
    {
        float time = 0;
        Vector3 safeEndPos = endPos + (startPos - endPos).normalized * 3f;

        while (Vector3.Distance(transform.position, safeEndPos) > 0.01f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, safeEndPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = safeEndPos;

        if (_controller != null)
        {
            _controller.enabled = true;
            targetObjPosition = Vector3.zero;
            gravOn = true;
        }
    }
}