using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu]

public class FireBall : Ability
{
    [SerializeField] private GameObject fireBall;
    [SerializeField] private int _damage;


    public override void Casting()
    {
        GameManager.instance.armAnim.SetTrigger("fireBall");
    }

    public override void Activate()
    {
        GameManager.instance.PlayerCam.StartCoroutine(GameManager.instance.PlayerCam.shootRay());
        Instantiate(fireBall, GameManager.instance.playerScript.shootPos.position, Camera.main.transform.rotation);
        FireBallObj script = fireBall.GetComponent<FireBallObj>();
        script.damage = _damage;
    }
}
