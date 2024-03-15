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

    private Rigidbody _rb;
    private Vector3 startPos;
    IPhysics phys;

    public override void Casting()
    {
        phys = GameManager.instance.player.GetComponent<IPhysics>();

        _rb = GameManager.instance.player.GetComponent<Rigidbody>();
        _velocity.y = 1;

        startPos = GameManager.instance.player.transform.position;

        if (phys != null)
        {
            phys.PhysicsDir(_velocity * force);
        }
    }

    public override void Activate()
    {
        
        if (GameManager.instance.playerScript.targetPos == Vector3.zero)
        {
            GameManager.instance.playerScript.targetPos = startPos;
        }
        
    }
}
