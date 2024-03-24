using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewItem", menuName = "Items/BansheesVeil")]
public class BansheesVeil : Item
{
    [Header("Stats to add")] 
    private int _maxHealth;
    private float _dmgMit;

    public override void addStats()
    {
        script.HP += 50;
        script._HPOrig += 50;
        script.damageMitigation += .1f;
    }
}
