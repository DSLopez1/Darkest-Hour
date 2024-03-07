using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{

    public string _name;
    public float _cooldownTime;
    public float _activeTime;

    public bool _onCooldown;

    public virtual void Activate()
    {
    }



}
