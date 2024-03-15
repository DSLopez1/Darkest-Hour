using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform cameraTarget;
    public float pLerp = 0.02f;
    public float rLerp = 1.0f;
    public Vector2 turn;
    public float sensitivity;

    private bool _isShooting;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);

        transform.position = Vector3.Lerp(transform.position, cameraTarget.position, pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraTarget.rotation, rLerp);
    }

    public IEnumerator shootRay()
    {
        _isShooting = true;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, 100))
        {
            Debug.Log("Shooting ray");
            GameManager.instance.playerScript.targetPos = hit.point;
        }
        yield return new WaitForSeconds(1);

        _isShooting = false;
    }
}
