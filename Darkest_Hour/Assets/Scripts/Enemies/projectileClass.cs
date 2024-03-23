using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileClass : MonoBehaviour
{
    [Header("----- Base Components-----")]
    [SerializeField] Rigidbody rb;
    [SerializeField] int _damageAmount;
    [SerializeField] int _speed;
    [SerializeField] int _destroyTime;
    [SerializeField] GameObject _hitEffect;
    public bool statusEffect;

    [Header("----- Burn -----")]
    [SerializeField] bool Burn;
    [SerializeField] int _burnDmg;
    [SerializeField] float _burnDur;
    [SerializeField] float _burnTick;


    [Header("----- Slow -----")]
    [SerializeField] bool Slow;
    [SerializeField] float _slowAmount;
    [SerializeField] int _slowDur;

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
        if (dmg != null && other.CompareTag("Player"))
        {
            dmg.TakeDamage(_damageAmount);
            if(statusEffect)
            {
                if (Slow)
                {
                    EffectManager.instance.Slow(_slowAmount, _slowDur);
                }
                if (Burn)
                {
                    EffectManager.instance.Burn(_burnDmg, _burnDur, _burnTick);
                }
            }
        }

        // Destory object on collision
        if (_hitEffect != null)
        {
            Instantiate(_hitEffect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
