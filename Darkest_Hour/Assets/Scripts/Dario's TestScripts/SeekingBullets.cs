using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeekingBullets : MonoBehaviour
{

    public int damage;
    public float speed;
    public float offset;
    private Transform _target;

    private Rigidbody _rb;
    private bool _collided;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.drag = 0;

        _rb.velocity = transform.forward * speed;
    }

    void Update()
    {
        if (_target != null)
        {
            transform.LookAt(_target);
            _rb.velocity = transform.forward * speed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null && other.tag != "Player")
        {
            _target = other.transform;
            SphereCollider col = GetComponent<SphereCollider>();
            col.isTrigger = false;
            col.radius = 0.1f;
        }
    }

    void OnCollisionEnter(Collision co)
    {

        Debug.Log("Collision triggered");
        IDamage dmg = co.gameObject.GetComponent<IDamage>();

        if (!_collided && co.gameObject.tag != "Player")
        {
            _collided = true;

            if (dmg != null)
            {
                dmg.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
