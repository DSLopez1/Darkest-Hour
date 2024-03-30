using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public Sprite sprite;

    private Image image;
    private AudioSource src;
    private Rigidbody _rb;
    void Start()
    {
        if (GameManager.instance.itemCopy.Count > 0)
        {
            int randomNum = Random.Range(0, GameManager.instance.itemCopy.Count);
            item = GameManager.instance.itemCopy[randomNum];
            GameManager.instance.itemCopy.RemoveAt(randomNum);
            _rb = GetComponent<Rigidbody>();
            image = GetComponentInChildren<Image>();
            src = GetComponent<AudioSource>();
            sprite = item.image;
            image.sprite = sprite;
        }
        else 
            Destroy(gameObject);

    }

    void Update()
    {
        transform.LookAt(GameManager.instance.player.transform);
        if (transform.position.y <= 1)
        {
            _rb.isKinematic = true;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        
        if (other.isTrigger)
            return;

        if (other.tag == "Player")
        {
            AudioManager.instance.PlaySoundEffect(6);
            DescriptionController.instance.StartCoroutine(DescriptionController.instance.callDesc(item.name));
            GameManager.instance.buttons.BuyItem(item.name);
            Destroy(gameObject);
        }
    }

    
}
