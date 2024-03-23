using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainOn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.playerScript.RainOn();
        }
    }
}
