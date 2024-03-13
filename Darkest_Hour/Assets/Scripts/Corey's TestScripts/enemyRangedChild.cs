using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRangedChild : EnemyAI
{
    [Header("----- Ranged Components -----")]
    [SerializeField] GameObject _projectile;
    [SerializeField] Transform _shootPos;

    override protected IEnumerator Attack()
    {
        _isAttacking = true;

        // Trigger warning animation

        // Audio for warning animation

        // Buffer before shooting
        yield return new WaitForSeconds(_attackDelayC);

        // Trigger shoot animation
        _animC.SetTrigger("Shoot");

        // Audio for shoot animation

        // Instiate projectile at shoot position
        Instantiate(_projectile, _shootPos.position, transform.rotation);

        // Delay before next attack can trigger
        yield return new WaitForSeconds(_timeBetweenAttacksC);


        _isAttacking = false;
    }
}
