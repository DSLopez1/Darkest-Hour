using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/ZurvanPendant")]
public class ZurvanPendant : Item
{
    public override void Initialize()
    {
        base.Initialize();
        script.coolDownReduction = .5f;

    }
}
