using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastObjScript : MonoBehaviour
{

    public int damage;
    public float activeTime;
    public float pushBack;
    public float radius;

    [SerializeField] GameObject impactVFX;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = GameManager.instance.player.transform.position;
        GameObject impact = Instantiate(impactVFX, transform.position, Quaternion.identity);

        Destroy(impact, 1);
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
        if (other.isTrigger)
            return;

        IPhysics phys = other.GetComponent<IPhysics>();
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null && !other.CompareTag("Player"))
        {
            dmg.TakeDamage(damage);
        }

        if (phys != null && !other.CompareTag("Player"))
        {
            Vector3 vel = (other.transform.position - transform.position).normalized;
            phys.PhysicsDir(vel * pushBack);
        }
    }
}
