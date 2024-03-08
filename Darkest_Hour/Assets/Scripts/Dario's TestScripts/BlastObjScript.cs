using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastObjScript : MonoBehaviour
{

    public int damage;
    public float activeTime;
    public float pushBack;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = GameManager.instance.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTime > 0)
        {
            activeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        IPhysics phys = other.GetComponent<IPhysics>();
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null)
        {
            dmg.TakeDamage(damage);
        }

        if (phys != null)
        {
            phys.PhysicsDir((gameObject.transform.position - other.transform.position).normalized * pushBack);
        }
    }
}
