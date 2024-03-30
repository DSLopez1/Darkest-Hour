using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]

public class FireBeam : Ability
{
    [SerializeField] private GameObject beam;

    [SerializeField] private int _damage;
    [SerializeField] private float _pulseInterval;
    private FirebeamObj beamScript;

    public override void Activate()
    {
        GameObject instantiatedBeam = Instantiate(beam, GameManager.instance.playerScript.firePos);
        beamScript = instantiatedBeam.GetComponent<FirebeamObj>();
        beamScript.activeTime = activeTime;
        beamScript.damage = _damage;
        beamScript._pulseInterval = _pulseInterval;
        AudioManager.instance.PlaySoundEffect(20);
    }

}
