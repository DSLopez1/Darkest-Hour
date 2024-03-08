using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    // Makes objects face camera
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
