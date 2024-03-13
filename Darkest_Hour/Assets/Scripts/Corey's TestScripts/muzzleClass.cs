using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muzzleClass : MonoBehaviour
{
    [SerializeField] int _destroyTime;

    void Start()
    {
        Destroy(gameObject, _destroyTime);
    }

}
