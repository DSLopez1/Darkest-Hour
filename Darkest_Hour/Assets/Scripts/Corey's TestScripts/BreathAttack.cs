using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathAttack : MonoBehaviour
{
    [SerializeField] int _damageAmount;
    [SerializeField] Collider _col;
    [SerializeField] float _tickRate;
    private bool canDamage = true;
    IDamage dmg;

    private void OnTriggerStay(Collider other)
    {
        // Make sure triggers don't trigger triggers
        if (other.isTrigger) return;

        // Check if it hit player
        if (other.CompareTag("Player"))
        {
            // Get IDamage from other
            dmg = other.GetComponent<IDamage>();
            // If can damage deal damage
            if (canDamage)
            {
                StartCoroutine(TickDamage());
            }
        }
    }

    private IEnumerator TickDamage()
    {
        // Turn off damage
        canDamage = false;
        // Deal damage
        if (dmg != null)
        {
            // Deal damage
            dmg.TakeDamage(_damageAmount);
        }
        // Wait
        yield return new WaitForSeconds(_tickRate);
        // Turn on damage
        canDamage = true;
    }
}


