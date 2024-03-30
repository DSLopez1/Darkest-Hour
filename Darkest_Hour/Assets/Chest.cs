using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Chest : MonoBehaviour
{
    public GameObject item;
    public Rigidbody rb;
    public float force;

    private Vector3 vel;
    private bool itemDispensed;
    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        vel = transform.right;
        vel.y += 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        if (other.tag == "Player" && !itemDispensed)
        {
            itemDispensed = true;
            anim.SetTrigger("open");

            AudioManager.instance.PlaySoundEffect(22);
            Invoke(nameof(open), 1.5f);

        }
    }

    void open()
    {
        Vector3 pos = transform.position;
        pos.y += 2;
        GameObject instItem = Instantiate(item, pos, Quaternion.identity);
        rb = instItem.GetComponent<Rigidbody>();
        rb.AddForce(vel * force, ForceMode.Impulse);

    }
}
