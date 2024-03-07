using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, INtInteractable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Interact();
            Destroy(gameObject);
        }
    }

    public void Interact()
    {
        GameManager.instance.playerScript.playerSpeed += 20;
    }
}
