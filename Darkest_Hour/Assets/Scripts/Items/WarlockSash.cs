using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockSash : Item
{
    public GameObject bullet;
    public armCall playerArm;

    public override void Initialize()
    {
        playerArm = script.arm.GetComponent<armCall>();

        playerArm.auto = bullet;
    }
}
