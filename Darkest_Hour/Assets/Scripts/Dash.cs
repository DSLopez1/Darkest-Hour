using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu]

public class Dash : Ability
{
    public float dashVelocity;

    public override void Activate()
    {
        IPhysics phys = GameManager.instance.player.GetComponent<IPhysics>();
        phys.PhysicsDir(GameManager.instance.playerScript.getMoveVec().normalized * dashVelocity);
    }
}
