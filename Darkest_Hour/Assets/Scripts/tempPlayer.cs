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
    [SerializeField] private float _shootDistance;
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

    public Vector3 targetObjPosition;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
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

        GameObject instObj = Instantiate(obj, _shootPos.position, Camera.main.transform.rotation);

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

    public void TakeDamage(int amount)
    {
        hp -= amount;
    }

    public void PhysicsDir(Vector3 dir)
    {
        _pushBack += dir;
    }

    public void callMove(Vector3 startPos, Vector3 endPos, float moveSpeed)
    {
        StartCoroutine(moveToPos(startPos, endPos, moveSpeed));
    }

    IEnumerator moveToPos(Vector3 startPos, Vector3 endPos, float moveSpeed)
    {
        float time = 0;
        Vector3 safeEndPos = endPos + (startPos - endPos).normalized * 3f;

        while (Vector3.Distance(transform.position, safeEndPos) > 0.01f) // 0.01f is a small threshold to avoid floating point imprecision
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, safeEndPos, moveSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        transform.position = safeEndPos;
        //while (time < 1)
        //{
        //    transform.position = Vector3.Lerp(startPos.position, safeEndPos, time);
        //    time += Time.deltaTime * lerpSpeed;
        //    yield return null;
        //}

        if (_controller != null)
        {
            _controller.enabled = true;
            targetObjPosition = Vector3.zero;
            gravOn = true;
        }
    }
    public Vector3 getMoveVec() { return _move; }
    

}
