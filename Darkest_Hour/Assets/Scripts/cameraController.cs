using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;

    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _distance = 5;

    [SerializeField] private float _minVerticalAngle = -45;
    [SerializeField] private float _maxVerticalAngle = 45;

    [SerializeField] private Vector2 _framingOffset;

    [SerializeField] private bool _invertX;
    [SerializeField] private bool _invertY;

    private float _rotationX;
    private float _rotationY;

    private float _invertXVal;
    private float _invertYVal;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _invertXVal = (_invertX) ? -1 : 1;
        _invertYVal = (_invertY) ? -1 : 1;

        _rotationX += Input.GetAxis("Mouse Y") * _invertYVal * _rotationSpeed;
        _rotationX = Mathf.Clamp(_rotationX, _minVerticalAngle, _maxVerticalAngle);

        _rotationY += Input.GetAxis("Mouse X") * _invertXVal * _rotationSpeed;

        var targetRotation = Quaternion.Euler(_rotationX, _rotationY, 0);

        var focusPosition = _followTarget.position + new Vector3(_framingOffset.x, _framingOffset.y);

        transform.position = focusPosition - targetRotation * new Vector3(0, 0, _distance);
        transform.rotation = targetRotation;
    }
}
