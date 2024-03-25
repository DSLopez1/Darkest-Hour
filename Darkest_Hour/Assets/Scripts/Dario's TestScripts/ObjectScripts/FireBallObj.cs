using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class FireBallObj : MonoBehaviour
{
    private Rigidbody _rb;
    public int damage;
    public float force;

    public GameObject impactVFX;
    private Explosion exScript;
    private bool _collided;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = true;
        Launch();
    }
    void Launch()
    {

        _rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag != "Player" && !_collided)
        {
            _collided = true;
            impactVFX.SetActive(true);
            GameObject impact = Instantiate(impactVFX, co.contacts[0].point, Quaternion.identity);
            Debug.Log("fireobj");
            Debug.Log(damage);

            Destroy(impact, 1);
            Destroy(gameObject);
        }
    }

}
