using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

[CreateAssetMenu]

public class Dash :MonoBehaviour
{
    [Header("Refs")]
    public Transform orientation;
    public PlayerRbScript script;
    public CineCam cam;
    public Rigidbody rb;

    [Header("Stats")]
    public float dashForce;
    public float activeTime;

    [Header("cooldown")] 
    public float cooldown;
    public float cooldownTime;

    public KeyCode key;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        script = GetComponent<PlayerRbScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(key))
            applyForce();

        if (cooldownTime > 0)
            cooldownTime -= Time.deltaTime;
    }

    void applyForce()
    {
        if (cooldownTime > 0) return;
        else
            cooldownTime = cooldown;
        tempSpeedHolder = script.moveSpeed;
        dashForce = script.dashSpeed;
        script.moveSpeed = dashForce;

        script.isdashing = true;
        script.moveSpeed = dashForce;
        Vector3 force;
        if (script.GetMoveDir() != Vector3.zero)
        {
            force = script.GetMoveDir().normalized * dashForce;
        }
        else
            force = orientation.forward.normalized * dashForce;
            
        

        delayedDash = force;
        Invoke(nameof(ApplyDelayedForce), 0.025f);

        Invoke(nameof(ResetDash), activeTime);
    }

    private Vector3 delayedDash;

    private void ApplyDelayedForce()
    {
        rb.AddForce(delayedDash, ForceMode.Impulse);
    }

    private float tempSpeedHolder;

    void ResetDash()
    {
        script.isdashing = false;
        script.moveSpeed = tempSpeedHolder;
    }

}
