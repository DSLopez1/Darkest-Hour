using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class Blast : Ability
{
    [SerializeField] private float _pushBack;
    [SerializeField] private float _radius;
    [SerializeField] private int _damage;
    [SerializeField] private GameObject blastObj;
    [SerializeField] private BlastObjScript blastScript;

    public override void Activate()
    {

        GameObject instantiateObj = Instantiate(blastObj, GameManager.instance.player.transform);
        blastScript = instantiateObj.GetComponent<BlastObjScript>();
        blastScript.activeTime = activeTime;
        blastScript.damage = _damage;
        blastScript.radius = _radius;
        blastScript.pushBack = _pushBack;
    }
}
