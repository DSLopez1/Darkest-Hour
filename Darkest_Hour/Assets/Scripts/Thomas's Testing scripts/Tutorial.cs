using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private bool isDashing = false;
    private bool isAttacking = false;
    private int currentStep = 0;
    private int movementCount = 0;
    private int dashCount = 0;
    private int attackCount = 0;

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private GameObject[] tutorialPanels;

    void Start()
    {
        ShowTutorialPanel(currentStep);
    }

    void Update()
    {
        switch (currentStep)
        {
            case 0:
                HandleMovement();
                break;
            case 1:
                HandleDash();
                break;
            case 2:
                HandleAttack();
                break;
        }
    }

    void ShowTutorialPanel(int step)
    {
        for (int i = 0; i < tutorialPanels.Length; i++)
        {
            tutorialPanels[i].SetActive(i == step);
        }
    }

    void HandleMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            movementCount++;
            if (movementCount >= 2)
            {
                currentStep++;
                ShowTutorialPanel(currentStep);
                movementCount = 0;
            }
        }
    }

    void HandleDash()
    {
        if (isDashing && Input.GetKeyDown(KeyCode.Space))
        {
            dashCount++;
            if (dashCount >= 2)
            {
                currentStep++;
                ShowTutorialPanel(currentStep);
                dashCount = 0;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isDashing = true;
        }
    }

    void HandleAttack()
    {
        if (isAttacking && Input.GetKeyDown(KeyCode.Alpha1))
        {
            attackCount++;
            if (attackCount >= 2)
            {
                currentStep++;
                ShowTutorialPanel(currentStep);
                attackCount = 0;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            isAttacking = true;
        }
    }
}
