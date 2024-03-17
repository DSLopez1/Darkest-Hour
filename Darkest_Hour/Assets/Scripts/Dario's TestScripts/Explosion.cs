using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage;
    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null && !other.CompareTag("Player"))
        {
            Debug.Log("Damage triggered");
            Debug.Log(damage);
            dmg.TakeDamage(damage);
        }
    }

}
