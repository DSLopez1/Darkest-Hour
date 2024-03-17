using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Cinemachine;
using UnityEditor.Build.Content;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class AutoObj : MonoBehaviour
{
    public float speed;
    public int damage;
    public GameObject impactVFX;
    private Rigidbody _rb;
    private bool _collided;
    private float _life = 5f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        Vector3 directionVel = _rb.transform.TransformDirection(Vector3.forward) * speed;

        _rb.velocity = directionVel;

        
    }

    void Update()
    {
        if (_life > 0)
        {
            _life -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision co)
    {
        IDamage dmg = co.gameObject.GetComponent<IDamage>();

        if (co.gameObject.tag != "Player" && !_collided)
        {
            _collided = true;
            if (dmg != null)
            {
                dmg.TakeDamage(damage);
            }
            GameObject impact = Instantiate(impactVFX, co.contacts[0].point, Quaternion.identity);

            Destroy(impact, 1);
            Destroy(gameObject);

        }
    }
}
