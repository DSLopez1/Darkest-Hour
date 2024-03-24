using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour, IDamage
{
    [Header("----Components----")]
    [SerializeField] CharacterController controller;

    public Transform firePos;
    public Transform shootPos;
    public Vector3 targetPos;

    [Header("-----Player Stats")]
    [SerializeField] int _HP;
    [SerializeField] int _lives;
    
    [Header("-----Abilities-----")]
    public List<AbilityHolder> abilities = new List<AbilityHolder>();

    public float speed = 5f;
    private Animator animator;
    public float rotationSpeed = 100f;

    public float backwardThreshold = -0.5f;
    private bool isAttacking = false;

    int _HPOrig;

    void Start()
    {
        _HPOrig = 10;
        _lives = 3;
        animator = GetComponent<Animator>();
        respawn();
    }

    void Update()
    {
        playerAnimations();
    }

    bool IsInAttackAnimation()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    }

    public void playerAnimations()
    {

        bool isInAttackAnimation = IsInAttackAnimation();

        if (!isInAttackAnimation)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            float moveAmount = movementDirection.magnitude;

            bool isMovingBackward = verticalInput < backwardThreshold;

            if (isMovingBackward)
            {
                animator.SetBool("IsRunningBackward", true);
            }
            else
            {
                animator.SetBool("IsRunningBackward", false);
            }

            animator.SetFloat("moveAmount", moveAmount);

            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

            Vector3 movementAmount = movementDirection * speed * Time.deltaTime;
            transform.Translate(movementAmount, Space.Self);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isInAttackAnimation)
            {
                animator.SetTrigger("Attack");
                isAttacking = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isInAttackAnimation && !isAttacking)
            {
                animator.SetTrigger("Dodge");
            }
            else if (isAttacking)
            {
                animator.SetTrigger("Dodge");
                animator.ResetTrigger("Attack");
                isAttacking = false;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        _HP -= amount;
        updatePlayerUI();
        StartCoroutine(flashDamage());

        if(_lives <= 0)
        {
            GameManager.instance.youLose();
        }
    }

    IEnumerator flashDamage()
    {
        GameManager.instance.playerDamageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.playerDamageFlash.SetActive(false);
    }

    void updatePlayerUI()
    {
        GameManager.instance.playerHPBar.fillAmount = (float)_HP / _HPOrig;

    }

    public void respawn()
    {
        _HP = _HPOrig;
        _lives--;
        updatePlayerUI();

        controller.enabled = false;
        transform.position = LevelManager.instance.playerSpawnPos.transform.position;
        controller.enabled = true;
    }
}