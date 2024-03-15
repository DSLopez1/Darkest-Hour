using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDoTAttack : MonoBehaviour
{
    [SerializeField] int _damageAmount;
    [SerializeField] Collider _col;
    [SerializeField] int _damageFreq;
    private bool _canHit = true;
    IDamage dmg;

    private void OnTriggerStay(Collider other)
    {
        // Make sure triggers don't trigger triggers
        if (other.isTrigger) return;

        // Check if it hit player
        if (other.CompareTag("Player"))
        {
            // Get IDamage to deal damage
            other.GetComponent<IDamage>();

            // Deal damage
            if (_canHit)
            {
                StartCoroutine(TickDamage());
            }
        }
    }

    private IEnumerator TickDamage()
    {
        // Deal damage
        _canHit = false;
        if (dmg != null)
        {
            // Deal damage
            dmg.TakeDamage(_damageAmount);
        }
        yield return new WaitForSeconds(_damageFreq);
        _canHit = true;
    }
}
