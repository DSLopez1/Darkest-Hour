using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/OBow")]

public class Obow : Item
{
    private armCall armScript;


    public override void Initialize()
    {
        base.Initialize();
        GameObject playerArm = script.arm;
        armScript = playerArm.GetComponent<armCall>();
    }

    public override void addStats()
    {
        armScript.fireRate /= 2;
    }
}
