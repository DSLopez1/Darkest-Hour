using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorObject : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _collided;
    private Vector3 _velocity;

    public int damage;
    public float acceleration;

    public GameObject impactVFX;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _velocity.y -= acceleration;

        _rb.velocity = (_velocity);
    }

    void OnCollisionEnter(Collision co)
    {
        if (!_collided)
        {
            _collided = true;

            GameObject impact = Instantiate(impactVFX, co.contacts[0].point, Quaternion.identity);
            Explosion exScript = impact.GetComponent<Explosion>();
            exScript.damage = damage;

            Destroy(impact, 1);
            Destroy(gameObject);
        }
    }

}
