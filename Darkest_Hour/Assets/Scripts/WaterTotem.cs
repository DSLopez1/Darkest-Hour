using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTotem : MonoBehaviour
{
    [SerializeField] private float _pulseFrequency;
    [SerializeField] private int _healAmount;

    private bool _isPulsing;

    void OnTriggerStay(Collider other)
    {
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null)
        {
            if (!_isPulsing)
            {
                StartCoroutine(HealPulse(dmg));
            }
        }
    }

    IEnumerator HealPulse(IDamage dmg)
    {
        _isPulsing = true;

        dmg.TakeDamage(-_healAmount);
        yield return new WaitForSeconds(_pulseFrequency);

        _isPulsing = false;
    }
}
