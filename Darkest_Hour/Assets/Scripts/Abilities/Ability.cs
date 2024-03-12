using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : ScriptableObject
{

    public string name;
    public float cooldownTime;
    public float activeTime;
    public float castTime;
    public Image cooldownImage;
    public Sprite sprite;

    public bool _onCooldown;

    public virtual void Activate()
    {
    }

    public virtual void Casting()
    {
    }
}