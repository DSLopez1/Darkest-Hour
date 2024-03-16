using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathAttack : MonoBehaviour
{
    [SerializeField] int _damageAmount;
    [SerializeField] Collider _col;

    private void OnTriggerStay(Collider other)
    {
        // Make sure triggers don't trigger triggers
        if (other.isTrigger) return;

        // Check if it hit player
        if (other.CompareTag("Player"))
        {
            // Turn off collider to prevent instances of double damage
            _col.enabled = false;

            // Get IDamage from other
            IDamage dmg = other.GetComponent<IDamage>();
            if (dmg != null)
            {
                // Deal damage
                dmg.TakeDamage(_damageAmount);
            }
        }
    }
}
