using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] GameObject _obj;
    [SerializeField] Collider _col;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _obj.SetActive(true);
            _col.enabled = false;
        }
    }
}
