using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleObj : MonoBehaviour
{
    private Vector3 _dirToHole;
    public float pullStrength;
    public float interval;
    private bool _isPulling;
    

    void OnTriggerStay(Collider other)
    {
        if (other.isTrigger)
            return;

        IPhysics phys = other.GetComponent<IPhysics>();

        if (phys != null && !_isPulling)
        {
            _dirToHole = (transform.position - other.transform.position).normalized;

            StartCoroutine(pull(phys, _dirToHole));
        }
    }

    IEnumerator pull(IPhysics phys, Vector3 pullDir)
    {
        _isPulling = true;

        phys.PhysicsDir(pullDir * pullStrength);

        yield return new WaitForSeconds(interval);

        _isPulling = false;
    }
}
