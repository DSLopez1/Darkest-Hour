using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireTotem : MonoBehaviour
{
    [SerializeField] private float _pulseFrequency;
    [SerializeField] private int _pulseDamage;

    private bool _isPulsing;

    void OnTriggerStay(Collider other)
    {
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null)
        {
            if (!_isPulsing)
            {
                StartCoroutine(FirePulse(dmg));
            }
        }
    }

    IEnumerator FirePulse(IDamage dmg)
    {
        _isPulsing = true;

        dmg.TakeDamage(_pulseDamage);
        yield return new WaitForSeconds(_pulseFrequency);

        _isPulsing = false;
    }
}
