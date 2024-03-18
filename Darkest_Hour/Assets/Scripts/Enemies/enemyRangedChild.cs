using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRangedChild : EnemyAI
{
    [Header("----- Ranged Components -----")]
    [SerializeField] GameObject _projectile;
    [SerializeField] Transform _shootPos;
    [SerializeField] GameObject _muzzleEffect;

    override protected IEnumerator Attack()
    {
        // Check if Raycast hits player or something else
        RaycastHit hit;
        if (Physics.Raycast(_headPosC.position, _playerDir, out hit))
        {
            // Did we hit both the player & the player is in the cone
            if (hit.collider.CompareTag("Player") && _angleToPlayer <= _viewConeC)
            {
                // Ensure more only one attack at a time
                _isAttacking = true;

                // Trigger shoot animation
                _animC.SetTrigger("Attack");

                // Audio for shoot animation

                // Delay before next attack can trigger
                yield return new WaitForSeconds(_timeBetweenAttacksC);


                _isAttacking = false;
            }
        }
    }

    public IEnumerator InstantiateProjectile()
    {
        // Check if there is a muzzle effect
        if (_muzzleEffect != null)
        {
            // Creates effect and buffers
            Instantiate(_muzzleEffect, _shootPos.position, transform.rotation);
            yield return new WaitForSeconds(_attackDelayC);
        }

        // Instiate projectile at shoot position timed w/ animation
        Instantiate(_projectile, _shootPos.position, transform.rotation);
    }
}
