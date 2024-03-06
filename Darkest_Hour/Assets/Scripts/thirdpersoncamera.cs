using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdpersoncamera : MonoBehaviour
{
    public float turnSpeed = 4.0f;

    public GameObject target;
    private float targetDistance;

    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 0.0f;

    private float rotx;

    // Start is called before the first frame update
    void Start()
    {
        targetDistance = Vector3.Distance(transform.position, target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotx += Input.GetAxis("Mouse Y") * turnSpeed;

        rotx = Mathf.Clamp( rotx, minTurnAngle, maxTurnAngle );

        transform.eulerAngles = new Vector3(-rotx, transform.eulerAngles.y + y, 0);

        transform.position = target.transform.position - (transform.forward * targetDistance);
    }
}
