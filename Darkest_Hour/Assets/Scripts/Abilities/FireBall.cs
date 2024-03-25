using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class FireBall : Ability
{
    [SerializeField] private GameObject fireBall;
    [SerializeField] private int _damage;
    private FireBallObj script;

    public override void Casting()
    {
        GameManager.instance.armAnim.SetTrigger("fireBall");
    }

    public override void Activate()
    {
        GameManager.instance.PlayerCam.StartCoroutine(GameManager.instance.PlayerCam.shootRay());
        script = fireBall.GetComponent<FireBallObj>();
        script.damage = _damage;
        Instantiate(fireBall, GameManager.instance.playerScript.shootPos.position, Camera.main.transform.rotation);
    }
}
