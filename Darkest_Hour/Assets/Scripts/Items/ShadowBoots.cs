using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/ShadowBoots")]
public class ShadowBoots : Item
{
    private Dash _dashScript;

    public override void Initialize()
    {
        base.Initialize();
        _dashScript = player.GetComponent<Dash>();
    }

    public override void addStats()
    {
        _dashScript.force *= 2;
        script.dashCooldown /= 2;
    }
}
