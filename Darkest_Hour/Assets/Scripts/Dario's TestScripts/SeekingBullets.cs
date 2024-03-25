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
    private SphereCollider _col;

    private Rigidbody _rb;
    private bool _collided;

    public GameObject impactVFX;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<SphereCollider>();
        _col.radius = 1;
        _rb.useGravity = false;
        _rb.drag = 0;
        _rb.velocity = transform.forward * speed;
    }

    void Update()
    {
        if (_target != null)
        {
            transform.LookAt(new Vector3(_target.transform.position.x, _target.transform.position.y + 1, _target.transform.position.z));
            _rb.velocity = transform.forward * speed;
        }
        else
        {
            _col.radius += .1f;
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
            _col.isTrigger = false;
            _col.radius = 0.1f;
        }
    }

    void OnCollisionEnter(Collision co)
    {

        Debug.Log("Collision triggered");
        IDamage dmg = co.gameObject.GetComponent<IDamage>();

        if (!_collided && co.gameObject.tag != "Player")
        {
            _collided = true;
            GameObject impact = Instantiate(impactVFX, co.contacts[0].point, Quaternion.identity);

            if (dmg != null)
            {
                dmg.TakeDamage(damage);
            }
            Destroy(impact, 1);
            Destroy(gameObject);
        }
    }
}
