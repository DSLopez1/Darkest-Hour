using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss_Dragon : EnemyAI
{
    [Header("----- Ability Information -----")]
    // Cooldown timers
    [SerializeField] int _biteCD;
    [SerializeField] int _breathCD;
    [SerializeField] int _screamCD;
    [SerializeField] int _gustCD;

    [Header("----- Extra Components -----")]
    [SerializeField] string _name;
    [SerializeField] TMP_Text _UI;
    [SerializeField] GameObject _bossHPBar;

    // Bools for turning CDs on and off
    private bool phaseTwo = false;
    private bool canBreathAttack = true;
    private bool canScreamAttack = true;
    private bool canGustAttack = true;
    private bool canWingAttack = true;
    private bool isGrounded;

    new void Start()
    {
        base.Start();
        // Turn on boss bar and set name
        _bossHPBar.SetActive(true);
        _UI.text = _name;
    }
    override protected IEnumerator Attack()
    {
        // Make sure not to trigger more than one attack
        _isAttacking = true;

        yield return new WaitForSeconds(_attackDelayC);

        // Create system of if statements that will call functions to do mechanics
        // All of which will be checked by a bool system to see if they can run or not
        if (canWingAttack)
        {
            // Do Wing Melee attack
        }
        else if (phaseTwo && canScreamAttack)
        {
            // Do Pillars (Scream)
        }
        else if (phaseTwo && canGustAttack)
        {
            // Do Gust
        }
        else if (canBreathAttack && isGrounded)
        {
            // Do Breath on ground
        }
        else if (canBreathAttack && !isGrounded)
        {
            // Do flying breath
        }
        else
        {
            // Do Bite (Normal attack)
            if (_agentC.remainingDistance <= _agentC.stoppingDistance)
                MeleeAttack();
        }

        // Space out attacks
        yield return new WaitForSeconds(_timeBetweenAttacksC);

        _isAttacking = false;
    }

    public override void TakeDamage(int amount)
    {
        // Take damage
        _hp -= amount;

        // Flash red
        StartCoroutine(FlashMat());

        // If boss is at or below 50% hp switch to phase two
        if (_hp <= (_hpMaxC / 2) && !phaseTwo)
        {
            phaseTwo = true;
        }

        // Die
        if (_hp <= 0)
        {
            // Disable boss bar
            _bossHPBar.SetActive(false);
            GameManager.instance.CompleteLevel(-1);
            Destroy(gameObject);

            // Win the game!
            GameManager.instance.YouWin();
        }

        // Lower HP on HP bar
        UpdateUI();
    }

}
