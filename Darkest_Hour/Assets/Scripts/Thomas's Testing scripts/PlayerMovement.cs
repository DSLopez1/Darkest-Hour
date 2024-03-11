using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;
    public float rotationSpeed = 100f;

    public float backwardThreshold = -0.5f;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
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

    bool IsInAttackAnimation()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    }
}