using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : ScriptableObject
{

    public string _name;
    public float _cooldownTime;
    public float _activeTime;
    public Image cooldownImage;

    public bool _onCooldown;

    public virtual void Activate()
    {
    }



}
