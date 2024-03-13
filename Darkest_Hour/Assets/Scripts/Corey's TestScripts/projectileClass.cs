using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileClass : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] int _damageAmount;
    [SerializeField] int _speed;
    [SerializeField] int _destroyTime;
    public bool statusEffect;

    void Start()
    {
        // Set speed
        rb.velocity = transform.forward * _speed;
        Destroy(gameObject, _destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure triggers don't trigger triggers
        if (other.isTrigger) return;

        // Deal designated damage amount
        IDamage dmg = other.GetComponent<IDamage>();
        if (dmg != null)
        {
            dmg.TakeDamage(_damageAmount);
        }

        // Destory object on collision
        Destroy(gameObject);
    }
}
