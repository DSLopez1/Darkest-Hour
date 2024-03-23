using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainOff : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.playerScript.RainOff();
        }
    }
}
