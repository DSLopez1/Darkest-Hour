using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thirdpersoncamera : MonoBehaviour
{
    public float turnSpeed = 4.0f;

    public GameObject target;
    private float _targetDistance;

    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 0.0f;

    private float _rotx;

    // Start is called before the first frame update
    private void Start()
    {
        _targetDistance = Vector3.Distance(transform.position, target.transform.position);
    }

    // Update is called once per frame
    private void Update()
    {
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        _rotx += Input.GetAxis("Mouse Y") * turnSpeed;

        _rotx = Mathf.Clamp( _rotx, minTurnAngle, maxTurnAngle );

        transform.eulerAngles = new Vector3(-_rotx, transform.eulerAngles.y + y, 0);

        transform.position = target.transform.position - (transform.forward * _targetDistance);
    }
}
