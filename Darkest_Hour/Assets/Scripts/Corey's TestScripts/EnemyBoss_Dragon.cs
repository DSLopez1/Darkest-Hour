using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyBoss_Dragon : EnemyAI
{
    [Header("----- Ability Information -----")]
    // Cooldown timers
    [SerializeField] int _wingAttack;
    [SerializeField] int _breathCD;
    [SerializeField] int _screamCD;
    [SerializeField] int _gustCD;

    [Header("----- Extra Components -----")]
    [SerializeField] string _name;
    [SerializeField] TMP_Text _UI;
    [SerializeField] GameObject _bossHPBar;
    [SerializeField] Collider[] _wingColliders;

    [Header("----- Fire Breath -----")]
    [SerializeField] Transform _breathPos;
    [SerializeField] ParticleSystem _breathAttack;
    [SerializeField] Collider _breathCol;

    [Header("----- Rain of Fire -----")]
    [SerializeField] int _rofCount;
    [SerializeField] Transform _rofHeightRot;
    [SerializeField] Transform _warningRot;
    [SerializeField] int _rainOfFireDist;
    [SerializeField] GameObject _fireBall;
    [SerializeField] GameObject _warning;
    [SerializeField] GameObject _rofMuzzleEffect;

    // Bools for turning CDs on and off
    private bool phaseTwo = false;
    private bool canBreathAttack = true;
    private bool canScreamAttack = true;
    private bool canGustAttack = true;
    private bool canWingAttack = true;
    private bool isGrounded;
    NavMeshHit hit;

    new void Start()
    {
        base.Start();
        // Turn on boss bar and set name
        _bossHPBar.SetActive(true);
        _UI.text = _name;
        isGrounded = true;
        _breathAttack.Stop();
    }
    override protected IEnumerator Attack()
    {
        // Make sure not to trigger more than one attack
        _isAttacking = true;

        yield return new WaitForSeconds(_attackDelayC);

        if (_agentC.remainingDistance <= _agentC.stoppingDistance)
        {
            // Create system of if statements that will call functions to do mechanics
            // All of which will be checked by a bool system to see if they can run or not
            if (canWingAttack && isGrounded)
            {
                // Do Wing Melee attack
                StartCoroutine(WingAttack());
            }
            else if (canScreamAttack && isGrounded) //add Phase 2
            {
                // Do Pillars (Scream)
                StartCoroutine(Scream());
            }
            else if (canGustAttack && isGrounded && phaseTwo) //add Phase 2
            {
                // Do Gust
                _animC.SetTrigger("Gust");
                canGustAttack = false;
            }
            else if (canBreathAttack && isGrounded)
            {
                // Do Breath on ground
                StartCoroutine(BreathAttack());
            }
            else if (isGrounded)
            {
                // Do Bite (Normal attack)
                MeleeAttack();
            }
        }
        

        // Space out attacks
        yield return new WaitForSeconds(_timeBetweenAttacksC);

        _isAttacking = false;
    }

    private IEnumerator Scream()
    {
        // Disallow attack
        canScreamAttack = false;
        // Call animation
        _animC.SetTrigger("Scream");
        // Cooldown
        yield return new WaitForSeconds(_screamCD);
        // Allow attack
        canScreamAttack = true;
    }
    public void RainOfFire()
    {
        for (int i = 0; i < _rofCount; i++)
        {
            // Randomizes spawn points
            Vector3 randomPos = Random.insideUnitSphere * _rainOfFireDist;
            // Connects back to starting pos
            randomPos += transform.position;

            // Checks random position on the layer selected (1 for this case)
            // Makes sure the point hits inside the NavMesh
            NavMesh.SamplePosition(randomPos, out hit, _rainOfFireDist, 1);


            // Spawn black circle at hit
            Instantiate(_warning, hit.position, _warningRot.rotation);

            // Add x & z from hit to the y of child
            Vector3 rof;
            rof.x = hit.position.x;
            rof.y = _rofHeightRot.transform.position.y;
            rof.z = hit.position.z;

            // Spawn muzzle & object
            Instantiate(_rofMuzzleEffect, rof, _rofHeightRot.transform.rotation);
            Instantiate(_fireBall, rof, _rofHeightRot.transform.rotation);
        }
    }

    #region Breath Attack
    private IEnumerator BreathAttack()
    {
        // Disallow attack
        canBreathAttack = false;
        // Animation which instiates object
        _animC.SetTrigger("Breath");
        // Cooldown
        yield return new WaitForSeconds(_breathCD);
        // Allow attack
        canBreathAttack = true;
    }
    public void BreathOn()
    {
        // Animation instiates object
        _breathAttack.Play();
        _breathCol.enabled = true;
    }
    public void BreathOff()
    {
        // Animation deletes object
        _breathAttack.Stop();
        _breathCol.enabled = false;
    }
    #endregion

    #region Wing Attack Ability
    private IEnumerator WingAttack()
    {
        // Disallows more attacks
        canWingAttack = false;
        // Trigger animation which turns on colliders
        _animC.SetTrigger("Wing");
        // Cooldown
        yield return new WaitForSeconds(_wingAttack);
        // Allow attack again
        canWingAttack = true;
    }
    public void WingColOn()
    {
        // Animation turns on
        for (int i = 0; i < _wingColliders.Length; i++)
        {
            _wingColliders[i].enabled = true;
        }
    }
    public void WingColOff()
    {
        // Animation turns on
        for (int i = 0; i < _wingColliders.Length; i++)
        {
           if (_wingColliders[i].enabled)
            {
                _wingColliders[i].enabled = false;
            }
        }
    }
    #endregion

    #region Ground Check
    public void Flying()
    {
        isGrounded = false;
    }
    public void Grounded()
    {
        isGrounded = true;
    }
    #endregion
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
