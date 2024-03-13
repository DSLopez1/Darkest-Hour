using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyAI : MonoBehaviour, IDamage, IPhysics
{
    [Header("----- Componenets -----")]
    [SerializeField] private Animator _anim;
    [SerializeField] private Renderer[] _models;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _headPos;
    [SerializeField] private Collider _meleeCollider; 

    [Header("----- Enemy Stats -----")]
    [SerializeField] private int _hp;
    [SerializeField] private int _viewCone;
    [SerializeField] private int _targetFaceSpeed;
    [SerializeField] private int _animSpeedTrans;
    [SerializeField] private int _roamPauseTime;
    [SerializeField] private int _roamDis;
    [SerializeField] private int _physicsResolve;
    [SerializeField] private int _attackDelay;
    [SerializeField] private int _timeBetweenAttacks;

    [Header("----- UI-----")]
    [SerializeField] Image _HPBar;

    // Player dest info
    private float _angleToPlayer;
    private Vector3 _playerDir;
    private bool _targetInRange;

    private int _hpOrig;
    private List<Color> _colors;
    private Color _color;
    private Vector3 _startingPos;
    private bool _destChosen;
    private float _stoppingDistanceOrig;
    protected bool _isAttacking;

    // Children passes
    protected Animator _animC;
    protected NavMeshAgent _agentC;
    protected int _attackDelayC;
    protected int _timeBetweenAttacksC;


    private void Start()
    {
        // Initialize 
        _startingPos = transform.position;
        _hpOrig = _hp;
        UpdateUI();
        _stoppingDistanceOrig = _agent.stoppingDistance;
        _colors = new List<Color>();
        for (int i = 0; i < _models.Length; i++)
        {
            _color = _models[i].material.color;
            _colors.Add(_color);
        }

        // Pass variables for child
        _animC = _anim;
        _agentC = _agent;
        _attackDelayC = _attackDelay;
        _timeBetweenAttacksC = _timeBetweenAttacks;
    }

    private void Update()
    {
        // Capture velocity normalized to lerp animations as needed
        float animSpeed = _agent.velocity.normalized.magnitude;

        // Lerp animations
        _anim.SetFloat("Speed", Mathf.Lerp(_anim.GetFloat("Speed"), animSpeed, Time.deltaTime * _animSpeedTrans));

        // Checks if player is in range
        if (!_targetInRange)
        {
            // Roam because player isn't in range
            StartCoroutine(Roam());
        }
        else
        {
            // Player is in range check if it they're in the view cone
            CanSeePlayer();
        }
    }

    virtual protected IEnumerator Attack()
    {
        _isAttacking = true;

        // Check if player is in range
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            // Warn player attack is coming **ADD WARNING**


            // Delay attack
            yield return new WaitForSeconds(_attackDelay);

            // Attack
            // Start animation
            _anim.SetTrigger("Attack");
        }

        // Space out attacks
        yield return new WaitForSeconds(_timeBetweenAttacks);
        _isAttacking = false;
    }

    public void MeleeColliderOn()
    {
        // Animation turns on
        _meleeCollider.enabled = true;
    }

    public void MeleeColliderOff()
    {
        if (_meleeCollider.enabled)
        {
            // Animation turns off
            _meleeCollider.enabled = false;
        } 
    }

    public void PhysicsDir(Vector3 dir)
    {
        _agent.velocity += dir;
    }

    private IEnumerator Roam()
    {
        // Make sure reamining distance is very small, or on point, & destChosen is false
        if (_agent.remainingDistance < 0.05f && !_destChosen)
        {
            // Chooses destination, updates stopping distance to allow roam, pause time
            _destChosen = true;
            _agent.stoppingDistance = 0;
            yield return new WaitForSeconds(_roamPauseTime);

            // Randomizes roam points
            Vector3 randomPos = Random.insideUnitSphere * _roamDis;
            // Connects back to starting pos
            randomPos += _startingPos;

            // Roams enemy to random position on the layer selected (1 for this case)
            // Makes sure the point hits inside the NavMesh
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPos, out hit, _roamDis, 1);
            _agent.SetDestination(hit.position);

            _destChosen = false;
        }
    }

    void CanSeePlayer()
    {
        // Finds where player is
        _playerDir = GameManager.instance.player.transform.position - _headPos.position;
        _angleToPlayer = Vector3.Angle(new Vector3(_playerDir.x, 0, _playerDir.z), transform.forward);

        // Reset stopping distance to original number
        _agent.stoppingDistance = _stoppingDistanceOrig;

        // Moves to player
        _agent.SetDestination(GameManager.instance.player.transform.position);
        // Checks if player is in view cone
        if (!_isAttacking && _angleToPlayer <= _viewCone)
        {
            StartCoroutine(Attack());
        }
        // Turn to face player
        if (_agent.remainingDistance < _agent.stoppingDistance) { FaceTarget(); }

    }

    private void FaceTarget()
    {
        // Rotates to player if they're within stopping range
        // Makes rot ignore player's Y pos
        Quaternion rot = Quaternion.LookRotation(new Vector3(_playerDir.x, transform.position.y, _playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * _targetFaceSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _targetInRange = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _targetInRange = false;
            _agent.stoppingDistance = 0;
        }
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("Taking Damage");
        // Play damage animation
        _anim.SetTrigger("Damage");

        // Take damage
        _hp -= amount;

        // Flash red
        StartCoroutine(FlashMat());
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
        // Lower HP on HP bar
       UpdateUI();
    }

    private IEnumerator FlashMat()
    {
        for (int i = 0; i < _models.Length; i++)
        {
            _models[i].material.color = Color.red;
        }
            
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < _models.Length; i++)
        {
            _models[i].material.color = _colors[i];
        }
    }


    void UpdateUI()
    {
        // Updates HP bar
        _HPBar.fillAmount = (float)_hp / _hpOrig;
    }
}
