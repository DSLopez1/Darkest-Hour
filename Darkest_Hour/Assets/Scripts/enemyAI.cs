using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAi : MonoBehaviour
{
    [Header("----- Componenets -----")]
    [SerializeField]
    private Animator _anim;
    [SerializeField] private Renderer _model;
    [SerializeField] private NavMeshAgent _agent;
    //[SerializeField] Transform shootPos;
    //[SerializeField] Transform headPos;
    //[SerializeField] AudioSource aud;

    [Header("----- Enemy Stats -----")]
    [SerializeField]
    private int _hp;
    [SerializeField] private int _viewCone;
    //[SerializeField] int shootCone;
    [SerializeField] private int _targetFaceSpeed;
    [SerializeField] private int _animSpeedTrans;
    [SerializeField] private int _roamPauseTime;
    [SerializeField] private int _roamDis;
    [SerializeField] private int _pointsGiven;
    [SerializeField] private int _physicsResolve;

    //[Header("----- UI-----")]
    //[SerializeField] Image HPBar;

    //[Header("Audio")]
    //[SerializeField] AudioClip[] soundsSteps;
    //[Range(0, 1)][SerializeField] float soundStepVol;
    //[SerializeField] AudioClip soundsShoot;
    //[Range(0, 1)][SerializeField] float soundShootVol;
    //[SerializeField] AudioClip hurtSound;
    //[Range(0, 1)][SerializeField] float hurtVol;
    //[SerializeField] AudioClip deathSound;
    //[Range(0, 1)][SerializeField] float deathVol;

    private bool _targetInRange;
    //bool isPlayingSteps;

    // Player dest info
    private float _angleToPlayer;
    private Vector3 _playerDir;

    private int _hpOrig;
    private Color _color;
    private Vector3 _startingPos;
    private bool _destChosen;
    private float _stoppingDistanceOrig;


    private void Start()
    {
        // Initialize 
        _startingPos = transform.position;
        _hpOrig = _hp;
        //updateUI();
        _color = _model.material.color;
        _stoppingDistanceOrig = _agent.stoppingDistance;
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
            //canSeePlayer();
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

    //void canSeePlayer()
    //{
    //    // Finds where player is
    //    playerDir = gameManager.instance.player.transform.position - headPos.position;
    //    angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);

    //    // Check if Raycast hits player or something else
    //    RaycastHit hit;
    //    if (Physics.Raycast(headPos.position, playerDir, out hit))
    //    {
    //        // Did we hit both the player & the player is in the cone
    //        if (hit.collider.CompareTag(targetChoice))
    //        {
    //            // Moves to player
    //            agent.SetDestination(gameManager.instance.player.transform.position);
    //            // Starts shooting if not already shooting & in cone of gun
    //            if (!isShooting && angleToPlayer <= shootCone)
    //            {
    //                StartCoroutine(shoot());
    //            }

    //            if (agent.remainingDistance < agent.stoppingDistance) { faceTarget(); }

    //            // Reset stopping distance to original number
    //            agent.stoppingDistance = stoppingDistanceOrig;
    //        }
    //    }
    //}

    private void FaceTarget()
    {
        // Rotates to player if they're within stopping range
        // Makes rot ignore player's Y pos
        //Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, transform.position.y, playerDir.z));
        //transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * targetFaceSpeed);
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

       // updateUI();
    }

    private IEnumerator FlashMat()
    {
        _model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _model.material.color = _color;
    }

   
    //void updateUI()
    //{
    //    // Updates HP bar
    //    HPBar.fillAmount = (float)HP / HPOrig;
    //}
}
