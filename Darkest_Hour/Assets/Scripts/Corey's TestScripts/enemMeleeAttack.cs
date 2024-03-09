using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemMeleeAttack : MonoBehaviour
{
    [SerializeField] int _damageAmount;
    private void OnTriggerEnter(Collider other)
    {
        // Make sure triggers don't trigger triggers
        if (other.isTrigger) return;

        // Check if it hit player
        if (other.CompareTag("Player"))
        {
            // Create damage
            IDamage dmg = other.GetComponent<IDamage>();

            
            if (dmg != null)
            {
                // Deal damage
                dmg.TakeDamage(_damageAmount);
            }
        }
    }
}
