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
    [SerializeField] float _moveSpeed;

    IPhysics phys;

    public override void Casting()
    {
        phys = GameManager.instance.player.GetComponent<IPhysics>();
        _velocity.y = 1;

        if (phys != null)
        {
            GameManager.instance.PlayerCam.cameraLocked = false;
            phys.PhysicsDir(_velocity * force);
            tempGrav = GameManager.instance.playerScript.gravity;
            GameManager.instance.playerScript.gravity = 0;
        }
    }

    public override void Activate()
    {
        cooldownImage = GameManager.instance.ability4Image;
        GameManager.instance.PlayerCam.cameraLocked = true;

        phys.PhysicsDir((GameManager.instance.player.transform.position - GameManager.instance.playerScript.targetLocation.position).normalized * _moveSpeed);
        
    }
}
