using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu]

public class FireBall : Ability
{
    [SerializeField] private GameObject fireBall;

    public override void Casting()
    {
    }

    public override void Activate()
    {
        cooldownImage = GameManager.instance.ability3Image;
        Instantiate(fireBall, GameManager.instance.playerScript.shootPos.position, Camera.main.transform.rotation);
        FireBallObj script = fireBall.GetComponent<FireBallObj>();
    }
}
