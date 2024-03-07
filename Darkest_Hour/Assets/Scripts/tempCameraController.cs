using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCameraController : MonoBehaviour
{
    [SerializeField] private int _sensitivity;
    [SerializeField] private int _lockVertMin;
    [SerializeField] private int _lockVertMax;
    [SerializeField] private bool _invertY;

    private float _rotX;

    private void Start()
    {
        // Lock & hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Get input
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * _sensitivity;
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * _sensitivity;

        // Invert look
        if (_invertY)
        { _rotX += mouseY; }
        else
        { _rotX -= mouseY; }

        // Clamp the rot on the x-axis
        _rotX = Mathf.Clamp(_rotX, _lockVertMin, _lockVertMax);

        // Rotate the cam on the x-axis
        transform.localRotation = Quaternion.Euler(_rotX, 0, 0);

        // Rotate the player on the y-axis
        transform.parent.Rotate(Vector3.up * mouseX);

    }
}