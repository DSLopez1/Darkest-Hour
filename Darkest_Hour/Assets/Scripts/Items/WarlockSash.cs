using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/WarlockSash")]
public class WarlockSash : Item
{
    public GameObject bullet;
    public armCall playerArm;

    public override void Initialize()
    {
        base.Initialize();
        playerArm = script.arm.GetComponent<armCall>();

        playerArm.auto = bullet;
    }
}
