using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempPlayer : MonoBehaviour
{
    [Header("----- Componenets -----")]
    [SerializeField] CharacterController controller;

    [Header("----- Player Stats -----")]
    [SerializeField] float playerSpeed;
    [SerializeField] public int jumpMax;
    [SerializeField] float jumpForce;
    [SerializeField] float gravity;
    [SerializeField] float sprintMod;

    Vector3 move;
    Vector3 playerVelocity;
    int jumpCount;

    void Start()
    {
 
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            jumpCount = 0;
            playerVelocity = Vector3.zero;
        }
        move = Input.GetAxis("Horizontal") * transform.right
             + Input.GetAxis("Vertical") * transform.forward;

        controller.Move(move * playerSpeed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            playerVelocity.y = jumpForce;
            jumpCount++;
        }
        playerVelocity.y += gravity * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);
    }

}
