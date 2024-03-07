using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, IntInteractable
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interact();
            Destroy(gameObject);
        }
    }

    public void Interact()
    {
        gameManager.instance.playerScript.playerSpeed += 20;
    }
}
