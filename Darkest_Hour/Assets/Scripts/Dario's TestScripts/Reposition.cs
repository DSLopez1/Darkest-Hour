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
    [SerializeField] private float _lerpSpeed;

    private CharacterController _controller;
    private Rigidbody _rb;
    IPhysics phys;

    public override void Casting()
    {
        phys = GameManager.instance.player.GetComponent<IPhysics>();
        _controller = GameManager.instance.player.GetComponent<CharacterController>();
        _rb = GameManager.instance.player.GetComponent<Rigidbody>();
        _velocity.y = 1;

        if (phys != null && _controller.isGrounded)
        {
            phys.PhysicsDir(_velocity * force);
            GameManager.instance.playerScript.gravOn = false;
        }
    }

    public override void Activate()
    {
        cooldownImage = GameManager.instance.ability4Image;
        if (!_controller.isGrounded)
        {
            _controller.enabled = false;
            Transform startPos = GameManager.instance.player.transform;
            GameManager.instance.playerScript.callLerp(startPos, GameManager.instance.playerScript.targetLocation,
                _lerpSpeed);
        }
    }
}
