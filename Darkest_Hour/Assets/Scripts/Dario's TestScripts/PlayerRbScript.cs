using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerRbScript : MonoBehaviour, IPhysics
{

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
    }


    private void GetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
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
