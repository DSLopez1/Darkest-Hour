using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Seeker : Ability
{

    [SerializeField] private GameObject _bullet;

    public override void Activate()
    {
        Vector3 pos = GameManager.instance.playerScript.shootPos.position;
        AudioManager.instance.PlaySoundEffect(16);
        GameObject intBullet = Instantiate(_bullet, pos, Camera.main.transform.rotation);
    }
}
