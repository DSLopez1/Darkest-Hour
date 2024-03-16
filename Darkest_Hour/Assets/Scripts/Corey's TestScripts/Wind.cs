using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] float _windSpeed;

    private void OnTriggerStay(Collider other)
    {
        IPhysics pushBack = other.GetComponent<IPhysics>();

        if (other.CompareTag("Player"))
        {
            // Makes sure whatever enters has IPushBack
            if (pushBack != null)
            {
                pushBack.PhysicsDir(transform.forward * _windSpeed * Time.deltaTime);
            }
        }
        
    }
}
