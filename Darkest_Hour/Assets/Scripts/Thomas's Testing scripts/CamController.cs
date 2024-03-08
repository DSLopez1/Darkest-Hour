using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float distance = 5f;
    [SerializeField] float heightOffset = 2f;
    [SerializeField] float minVerticalAngle = -10f;
    [SerializeField] float maxVerticalAngle = 45f;
    [SerializeField] Vector2 framingOffset;
    [SerializeField] bool invertX;
    [SerializeField] bool invertY;

    float rotationX;
    float rotationY;
    float invertXval;
    float invertYval;

    public bool cameraLocked = true;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        transform.position = followTarget.position + new Vector3(0, heightOffset, -distance);
        transform.rotation = followTarget.rotation;
    }

    private void Update()
    {
        invertXval = invertX ? -1 : 1;
        invertYval = invertY ? -1 : 1;

        rotationX += Input.GetAxis("Mouse Y") * invertYval * rotationSpeed;
        if (cameraLocked)
        {
            rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);
        }

        if (transform.eulerAngles.x < 180 && rotationX > 180)
        {
            rotationX = Mathf.Min(rotationX, 180);
        }

        rotationY += Input.GetAxis("Mouse X") * invertXval * rotationSpeed;

        var targetRotation = Quaternion.Euler(-rotationX, rotationY, 0);

        Vector3 targetPosition = followTarget.position + Vector3.up * heightOffset;
        targetPosition += followTarget.right * framingOffset.x + followTarget.up * framingOffset.y;

        // Apply camera distance
        targetPosition -= targetRotation * Vector3.forward * distance;

        // Update camera position and rotation
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }

    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
}

