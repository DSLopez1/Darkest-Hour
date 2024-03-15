using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargeterProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float _lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 movement = transform.forward * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
        if (_lifeTime > 0)
        {
            _lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.isTrigger || other.CompareTag("Player"))
            return;
        GameManager.instance.playerScript.targetPos = transform.position;
        speed = 0;
    }
}
