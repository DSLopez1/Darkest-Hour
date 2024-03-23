using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathBarrier : MonoBehaviour
{

    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        IDamage dmg = other.GetComponent<IDamage>();
        
        dmg?.TakeDamage(9999);
    }
}
