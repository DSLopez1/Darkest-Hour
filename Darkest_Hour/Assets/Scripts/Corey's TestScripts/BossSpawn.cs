using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] GameObject _obj;
    [SerializeField] Collider _col;
    [SerializeField] int _timeBeforeSpawn;
    [SerializeField] ParticleSystem partSpawn;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _col.enabled = false;
            StartCoroutine(Spawn());
        }
    }

    private IEnumerator Spawn()
    {
        partSpawn.Play();
        yield return new WaitForSeconds(_timeBeforeSpawn);
        _obj.SetActive(true);
        partSpawn.Stop();
    }
}
