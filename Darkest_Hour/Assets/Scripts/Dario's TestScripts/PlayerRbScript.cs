using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerRbScript : MonoBehaviour, IPhysics
{
    [Header("Stats")]


    public float speed = 5f;
    private Animator animator;
    public float rotationSpeed = 100f;

    public float backwardThreshold = -0.5f;
    private bool isAttacking = false;

    [Header("Movement")] 
    public float moveSpeed;
    public float drag;
    public bool limitSpeed = true;
    public float dashSpeed;

    [Header("GroundCheck")] 
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool isGrounded;

    [Header("Positions")]
    public Transform orientation;
    public Vector3 targetPos;
    public Transform firePos;
    public Transform shootPos;

    [Header("Abilities")]
    public List<AbilityHolder> abilities = new List<AbilityHolder>();

    private float _horizontalInput;
    private float _verticalInput;

    private Vector3 _moveDir;
    private Vector3 _pushBack;

    public Rigidbody rb;

    public bool isdashing;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;

        for (int i = 0; i < 4; i++)
        {
            GameObject abilityHolderObject = new GameObject("AbilityHolderObject");
            AbilityHolder abilityHolder = abilityHolderObject.AddComponent<AbilityHolder>();
            abilities.Add(abilityHolder);
        }
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        GetInputs();
        SpeedControl();

        if (isGrounded && !isdashing)
            rb.drag = drag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        playerAnimations();
    }


    private void GetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void playerAnimations()
    {
        bool isInAttackAnimation = IsInAttackAnimation();

        if (!isInAttackAnimation)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            float moveAmount = movementDirection.magnitude;

            bool isMovingBackward = verticalInput < backwardThreshold;

            if (isMovingBackward)
            {
                animator.SetBool("IsRunningBackward", true);
            }
            else
            {
                animator.SetBool("IsRunningBackward", false);
            }

            animator.SetFloat("moveAmount", moveAmount);

            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

            Vector3 movementAmount = movementDirection * speed * Time.deltaTime;
            transform.Translate(movementAmount, Space.Self);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isInAttackAnimation)
            {
                animator.SetTrigger("Attack");
                isAttacking = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isInAttackAnimation && !isAttacking)
            {
                animator.SetTrigger("Dodge");
            }
            else if (isAttacking)
            {
                animator.SetTrigger("Dodge");
                animator.ResetTrigger("Attack");
                isAttacking = false;
            }
        }
    }

    bool IsInAttackAnimation()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    }

    private void MovePlayer()
    {
        if (isdashing) return;

        _moveDir = orientation.forward * _verticalInput + orientation.right * _horizontalInput;

        rb.AddForce((_moveDir.normalized * moveSpeed * 10f) + _pushBack, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void PhysicsDir(Vector3 dir)
    {
        _pushBack += dir;
    }

    public Vector3 GetMoveDir()
    {
        return _moveDir;
    }
}
