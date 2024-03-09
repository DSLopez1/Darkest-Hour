using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TempPlayer : MonoBehaviour, IDamage, IPhysics
{
    [Header("----- Componenets -----")]
    [SerializeField] private CharacterController _controller;

    [SerializeField] private GameObject _targeterObject;
    [SerializeField] private Transform _shootPos;

    [Header("----- Player Stats -----")] 
    [SerializeField] public int hp;
    [SerializeField] public float playerSpeed;
    [SerializeField] public int jumpMax;
    [SerializeField] private float _jumpForce;
    [SerializeField] public float gravity;
    [SerializeField] private float _sprintMod;
    [SerializeField] private float _pushBackResolution;

    [Header("-----AbilityPos-----")] 
    [SerializeField] public Transform firePos;

    private Vector3 _move;
    private Vector3 _playerVelocity;
    private Vector3 _pushBack;
    private int _jumpCount;
    private bool _isShooting;
    public bool gravOn = true;

    public Transform targetLocation;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();

        if (Input.GetButtonDown("Fire1") && !_isShooting && !_controller.isGrounded)
        {
            StartCoroutine(shootProjectile(_targeterObject));
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

        GameObject instObj = Instantiate(obj, _shootPos.position, Camera.main.transform.rotation);

        yield return new WaitForSeconds(1);
        _isShooting = false;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
    }

    public void PhysicsDir(Vector3 dir)
    {
        _pushBack += dir;
    }

    public void callLerp(Transform startPos, Transform endPos, float lerpSpeed)
    {
        StartCoroutine(lerpToPos(startPos, endPos, lerpSpeed));
    }

    IEnumerator lerpToPos(Transform startPos, Transform endPos, float lerpSpeed)
    {
        float time = 0;

        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPos.position, endPos.position, time);
            time += Time.deltaTime * lerpSpeed;
            yield return null;
        }

        transform.position = endPos.position;

        if (_controller != null)
        {
            _controller.enabled = true;
            targetLocation = null;
            gravOn = true;
        }
    }
    public Vector3 getMoveVec() { return _move; }
    

}
