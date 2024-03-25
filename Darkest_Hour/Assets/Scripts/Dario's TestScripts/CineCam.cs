using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CineCam : MonoBehaviour
{

    [Header("Refs")]
    public Transform orientation;

    [Header("groundCheck")]

    public Transform player;
    public Transform playerObj;

    public float rotationSpeed;

    public Transform combatLookAt;

    private camState _camStyle;
    private bool _isShooting;

    enum camState
    {
        Basic,
        Combat,
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _camStyle = camState.Combat;
    }

    private void Update()
    {
        //orientation

        Vector3 viewPositionDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewPositionDirection.normalized;

        //rotate player
        if (_camStyle == camState.Basic)
        {
            float hInput = Input.GetAxis("Horizontal");
            float vInput = Input.GetAxis("Vertical");

            Vector3 inputDir = orientation.forward * vInput + orientation.right * hInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        else if (_camStyle == camState.Combat)
        {
            Vector3 combatDir = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = combatDir.normalized;

            playerObj.forward = combatDir.normalized;
        }
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
