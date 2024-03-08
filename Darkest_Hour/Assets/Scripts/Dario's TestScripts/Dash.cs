using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Assertions.Must;

[CreateAssetMenu]

public class Dash : Ability
{
    public float dashVelocity;
    private Vector3 _velocityVector;

    public override void Activate()
    {
        cooldownImage = GameManager.instance.ability1Image;
        Debug.Log("Dashing");
        IPhysics phys = GameManager.instance.player.GetComponent<IPhysics>();
        _velocityVector = GameManager.instance.playerScript.getMoveVec().normalized;
        phys.PhysicsDir(_velocityVector * dashVelocity);
    }
}
