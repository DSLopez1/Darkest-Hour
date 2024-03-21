using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public Sprite sprite;
    public AudioClip clip;

    private Image image;
    private AudioSource src;
    
    void Start()
    {
        image = GetComponentInChildren<Image>();
        src = GetComponent<AudioSource>();
        src.clip = clip;
        sprite = item.image;
        image.sprite = sprite;

    }

    void Update()
    {
        transform.LookAt(GameManager.instance.player.transform);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.tag == "Player")
        {
            ButtonFunctions button = new ButtonFunctions();

            button.BuyItem(item.name);
            src.Play();
            Destroy(gameObject);
        }
    }

    
}
