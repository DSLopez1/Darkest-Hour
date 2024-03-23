using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private bool isDashing = false;
    private int currentStep = 0;
    private int movementCount = 0;
    private int dashCount = 0;
    private int attackCount = 0;
    private int abilityCount = 0;
    private int abilityMenuCount = 0;

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
            case 3: 
                HandleAbilityMenu();
                break;
            case 4:
                HandleAbility1();
                break;
            case 5:
                HandleAbility2();
                break;
            case 6:
                HandleAbility3();
                break;
            case 7:
                HandleAbility4();
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

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attackCount++;
            if (attackCount >= 2)
            {
                currentStep++;
                ShowTutorialPanel(currentStep);
                attackCount = 0;
            }
        }
    }

    void HandleDash()
    {
        if (isDashing && Input.GetKeyDown(KeyCode.Space))
        {
            dashCount++;
            if (dashCount >= 1)
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

    void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            movementCount++;
            if (movementCount >= 3)
            {
                currentStep++;
                ShowTutorialPanel(currentStep);
                movementCount = 0;
            }
        }
    }

    void HandleAbilityMenu()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {   
            abilityMenuCount++;
            if (abilityMenuCount >= 1)
            {
                currentStep++;
                ShowTutorialPanel(currentStep);
                abilityMenuCount = 0;
            }
            
        }
    }

    void HandleAbility1()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            abilityCount++;
            if(abilityCount >= 1)
            {
                currentStep++;
                ShowTutorialPanel(currentStep);
                abilityCount = 0;
            }
        }
    }

    void HandleAbility2()
    {
        if (Input.GetKey(KeyCode.Alpha2))
        {
            abilityCount++;
            if (abilityCount >= 1)
            {
                currentStep++;
                ShowTutorialPanel(currentStep);
                abilityCount = 0;
            }
        }
    }

    void HandleAbility3()
    {
        if (Input.GetKey(KeyCode.Alpha3))
        {
            abilityCount++;
            if (abilityCount >= 1)
            {
                currentStep++;
                ShowTutorialPanel(currentStep);
                abilityCount = 0;
            }
        }
    }

    void HandleAbility4()
    {
        if (Input.GetKey(KeyCode.Alpha4))
        {
            abilityCount++;
            if (abilityCount >= 1)
            {
                currentStep++;
                ShowTutorialPanel(currentStep);
                abilityCount = 0;
            }
        }
    }

}
