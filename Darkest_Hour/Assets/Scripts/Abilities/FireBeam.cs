using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class FireBeam : Ability
{
    [SerializeField] private GameObject beam;

    [SerializeField] private int _damage;
    private FirebeamObj beamScript;

    public override void Activate()
    {
        cooldownImage = GameManager.instance.ability2Image;
        Debug.Log("Activating");
        GameObject instantiatedBeam = Instantiate(beam, GameManager.instance.playerScript.firePos);
        FirebeamObj beamScript = instantiatedBeam.GetComponent<FirebeamObj>();
        beamScript.activeTime = _activeTime;
        beamScript.damage = _damage;
    }

}
