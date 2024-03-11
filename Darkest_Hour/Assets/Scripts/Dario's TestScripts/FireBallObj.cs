using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class FireBallObj : MonoBehaviour
{
    private Rigidbody _rb;
    public Vector3 endPos;
    

    private float _height;
    [SerializeField] private float _grav;
    [SerializeField] private float _heightDisplacement;

    void Start()
    {
        if (GameManager.instance.playerScript.targetObjPosition != Vector3.zero)
        {
            _rb = GetComponent<Rigidbody>();
            endPos = GameManager.instance.playerScript.targetObjPosition;
            if (GameManager.instance.player.transform.position.y > endPos.y)
            {
                _height = GameManager.instance.player.transform.position.y + _heightDisplacement;
            }
            else
            {
                _height = endPos.y + _heightDisplacement;
            }
            _rb.useGravity = false;
            _rb.velocity = CalculateTrajectory();
            Launch();
        }
    }
    void Launch()
    {
        Debug.Log("Launch called");
        Physics.gravity = Vector3.up * _grav;
        _rb.useGravity = true;

    }

    private Vector3 CalculateTrajectory()
    {
        float displacementY = endPos.y - _rb.position.y;
        Vector3 displacementXZ = new Vector3(endPos.x -  _rb.position.x, 0, endPos.z - _rb.position.z);

        Vector3 velocityY = Vector3.up * MathF.Sqrt(-2 * _grav * _height);
        Vector3 velocityXZ = displacementXZ / (MathF.Sqrt(-2 *_height / _grav) + MathF.Sqrt(2 * (displacementY - _height) / _grav));

        return velocityXZ + velocityY;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger || other.CompareTag("Player"))
            return;

        Destroy(gameObject);
    }

}