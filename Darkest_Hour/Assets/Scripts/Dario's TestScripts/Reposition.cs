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

    public override void Casting()
    {
        IPhysics phys;
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
    }
}
