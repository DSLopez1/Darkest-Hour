using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]

public class Meteor : Ability
{
    [SerializeField] private int _damage;
    [SerializeField] private float _acceleration;
    [SerializeField] private GameObject _meteor;
    [SerializeField] private float _spawnHeight;

    private Vector3 _startPos;
    private MeteorObject _meteorScript;

    public override void Activate()
    {
        _meteorScript = _meteor.GetComponent<MeteorObject>();
        GameManager.instance.PlayerCam.StartCoroutine(GameManager.instance.PlayerCam.shootRay());

        _meteorScript.damage =_damage;
        _meteorScript.acceleration =_acceleration;

        _startPos = GameManager.instance.playerScript.targetObjPosition;
        _startPos.y += _spawnHeight;

        GameObject meteor = Instantiate(_meteor,_startPos, Quaternion.Euler(90, 0, 0));
    }
}
