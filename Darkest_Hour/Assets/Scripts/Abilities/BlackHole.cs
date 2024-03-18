using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : Ability
{
    [SerializeField] private float _force;
    [SerializeField] private float _speed;
    [SerializeField] private float _interval;
    [SerializeField] private GameObject _blackHole;
    private BlackHoleObj _script;
    

    public override void Activate()
    {
        _script.pullStrength = _force;
        _script.interval = _interval;

    }
}
