using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;

    [SerializeField] private int _hp;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private int _jumpMax;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity;


    private Vector3 _move;
    private Vector3 _playerVel;
    private int _jumpCount;
   

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    { 
        Movement();
    }

    private void Movement()
    {
        if (_controller.isGrounded)
        {
            _jumpCount = 0;
            _playerVel = Vector3.zero;
        }

        _move = Input.GetAxis("Horizontal") * transform.right
            + Input.GetAxis("Vertical") * transform.forward;

        _controller.Move(_move * _playerSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && _jumpCount < _jumpMax)
        {
            _playerVel.y = _jumpForce;
            _jumpCount++;
        }

        _playerVel.y += _gravity * Time.deltaTime;
        _controller.Move(_playerVel * Time.deltaTime);
    }

    

    public void TakeDamage(int amount)
    {
        _hp -= amount;

    }

}