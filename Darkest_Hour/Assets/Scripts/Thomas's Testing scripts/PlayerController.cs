using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour, IPhysics
{
    [SerializeField] public float playerSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;
    [SerializeField] public Transform firePos;
    [SerializeField] private float physResolve;
    [SerializeField] public float gravity;

    Quaternion targetRotation;

    CameraController cameraController;
    Animator animator;
    CharacterController characterController;

    private Vector3 _moveDir;
    private Vector3 _pushBack;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {

        _pushBack = Vector3.Lerp(_pushBack, Vector3.zero, physResolve * Time.deltaTime);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));

        var moveInput = (new Vector3(h, 0, v)).normalized;

        _moveDir = (cameraController.PlanarRotation * moveInput) * playerSpeed;

        //Gravity
        if (!characterController.isGrounded)
        {
            _pushBack.y -= gravity * Time.deltaTime;
        }
        else
        {
            _pushBack.y = 0;
        }

        if (moveAmount > 0)
        {
            characterController.Move((_moveDir + _pushBack) * Time.deltaTime);

            targetRotation = Quaternion.LookRotation(_moveDir);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        animator.SetFloat("moveAmount", moveAmount, 0.2f, Time.deltaTime);
    }

    public Vector3 getMoveVec()
    {
        return _moveDir;
    }

    public void PhysicsDir(Vector3 dir)
    {
        _pushBack += dir;
    }
}