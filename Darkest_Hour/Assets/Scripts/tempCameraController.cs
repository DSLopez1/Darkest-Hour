using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TempCameraController : MonoBehaviour
{
    [SerializeField] private int _sensitivity;
    [SerializeField] private int _lockVertMin;
    [SerializeField] private int _lockVertMax;
    [SerializeField] private bool _invertY;
    [SerializeField] private float _yOffset;

    private float _rotX;
    private bool _isShooting;
    private Vector3 _camPosition;



    private void Start()
    {
        // Lock & hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _camPosition = GameManager.instance.player.transform.position;
        _camPosition.y += _yOffset;
        transform.position = _camPosition;
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

    public IEnumerator shootRay()
    {
        _isShooting = true;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, 100))
        {
            GameManager.instance.playerScript.targetPos = hit.point;
        }
        yield return new WaitForSeconds(1);

        _isShooting = false;
    }
}