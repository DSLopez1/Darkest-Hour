using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour, IDamage, IPhysics
{
    [Header("----- Componenets -----")]
    [SerializeField] private CharacterController _controller;

    [Header("----- Player Stats -----")] 
    [SerializeField] public int hp;
    [SerializeField] public float playerSpeed;
    [SerializeField] public int jumpMax;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity;
    [SerializeField] private float _sprintMod;
    [SerializeField] private float _pushBackResolution;

    [Header("-----AbilityPos-----")] 
    [SerializeField] public Transform firePos;

    private Vector3 _move;
    private Vector3 _playerVelocity;
    private Vector3 _pushBack;
    private int _jumpCount;

    private void Start()
    {
 
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
            _jumpCount = 0;
            _playerVelocity = Vector3.zero;
        }
        _move = Input.GetAxis("Horizontal") * transform.right
             + Input.GetAxis("Vertical") * transform.forward;

        _controller.Move(_move * playerSpeed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && _jumpCount < jumpMax)
        {
            _playerVelocity.y = _jumpForce;
            _jumpCount++;
        }

        _playerVelocity.y += _gravity * Time.deltaTime;
        _controller.Move((_playerVelocity + _pushBack) * Time.deltaTime);
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
    }

    public void PhysicsDir(Vector3 dir)
    {
        _pushBack += dir;
    }

    public Vector3 getMoveVec() { return _move; }
    
}
