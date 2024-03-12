using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu]

public class Reposition : Ability
{
    public float force;
    private Vector3 _velocity;
    private float tempGrav;
    [SerializeField] private float _moveSpeed;

    private CharacterController _controller;
    private Rigidbody _rb;
    private Vector3 startPos;
    IPhysics phys;

    public override void Casting()
    {
        phys = GameManager.instance.player.GetComponent<IPhysics>();
        _controller = GameManager.instance.player.GetComponent<CharacterController>();
        _rb = GameManager.instance.player.GetComponent<Rigidbody>();
        _velocity.y = 1;

        startPos = GameManager.instance.player.transform.position;

        if (phys != null)
        {
            phys.PhysicsDir(_velocity * force);
            GameManager.instance.playerScript.gravOn = false;
        }
    }

    public override void Activate()
    {
        
        _controller.enabled = false;
        if (GameManager.instance.playerScript.targetObjPosition == Vector3.zero)
        {
            GameManager.instance.playerScript.targetObjPosition = startPos;
        }

        GameManager.instance.playerScript.callMove(startPos, GameManager.instance.playerScript.targetObjPosition, _moveSpeed);
    }
}
