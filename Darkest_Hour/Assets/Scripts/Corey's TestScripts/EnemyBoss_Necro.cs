using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss_Necro : enemyRangedChild
{
    [Header("----- Ability Information -----")]
    [SerializeField] int _summonCD;
    [SerializeField] int _scytheCD;
    [SerializeField] int _spellCD;

    [Header("----- Extra Components -----")]
    [SerializeField] string _name;
    [SerializeField] TMP_Text _UI;
    [SerializeField] GameObject _bossHPBar;
    [SerializeField] Collider _abilityCol;
    [SerializeField] int _spawnDis;
    [SerializeField] GameObject[] _enemies;
    [SerializeField] int _numToSpawn;
    [SerializeField] GameObject _spawnAnim;
    NavMeshHit hit;

    private bool phaseTwo = false;
    private bool canSummon = true;
    private bool canAbility = true;

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
        if (canSummon)
        {
            StartCoroutine(SummonAbility());
        }
        else if (!phaseTwo && canAbility)
        {
            StartCoroutine(ScytheAbility());
        }
        else if (phaseTwo && canAbility)
        {
            StartCoroutine(SpellAttack());
        }
        else
        {
            if (_agentC.remainingDistance <= _agentC.stoppingDistance)
                MeleeAttack();
        }

        // Space out attacks
        yield return new WaitForSeconds(_timeBetweenAttacksC);

        _isAttacking = false;
    }

    private IEnumerator SummonAbility()
    {
        // Start CD
        canSummon = false;

        // Trigger animation
        _animC.SetTrigger("Spawn");

        for (int i = 0; i < _numToSpawn; i++)
        {
            // Spawn enemy
            Spawn();
        }
        
        // Start cooldown
        yield return new WaitForSeconds(_summonCD);

        // Allow ability again
        canSummon = true;
    }
    private void Spawn()
    {
        // Randomizes spawn points
        Vector3 randomPos = Random.insideUnitSphere * _spawnDis;
        // Connects back to starting pos
        randomPos += transform.position;

        // Spawns enemies to random position on the layer selected (1 for this case)
        // Makes sure the point hits inside the NavMesh
        NavMesh.SamplePosition(randomPos, out hit, _spawnDis, 1);


        // Picks random enemy
        int _arrayPos = Random.Range(0, _enemies.Length);
        Instantiate(_spawnAnim, hit.position, transform.rotation);
        Instantiate(_enemies[_arrayPos], hit.position, transform.rotation);
    }

    private IEnumerator ScytheAbility()
    {
        // Start CD
        canAbility = false;

        // Trigger animation
        _animC.SetTrigger("Ability");

        // Start cooldown
        yield return new WaitForSeconds(_scytheCD);
        // Allow ability again
        canAbility = true;
    }
    private IEnumerator SpellAttack()
    {
        // Trigger animation
        _animC.SetTrigger("Spell");

        // Start cooldown
        yield return new WaitForSeconds(_spellCD);
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
        }

        // Lower HP on HP bar
        UpdateUI();
    }

    public void AbilityColliderOn()
    {
        // Animation turns on
        _abilityCol.enabled = true;
    }
    public void AbilityColliderOff()
    {
        if (_abilityCol.enabled)
        {
            // Animation turns off
            _abilityCol.enabled = false;
        }
    }
}
