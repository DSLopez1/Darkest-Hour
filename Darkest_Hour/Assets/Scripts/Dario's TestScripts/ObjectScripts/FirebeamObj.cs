using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebeamObj : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] public float _pulseInterval;
    public float activeTime;

    private bool isTicking;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = GameManager.instance.playerScript.firePos.position;

        Vector3 currentRotation = gameObject.transform.rotation.eulerAngles;

        Vector3 newRotation = new Vector3(currentRotation.x,
            GameManager.instance.player.transform.rotation.eulerAngles.y,
            GameManager.instance.player.transform.rotation.eulerAngles.z);

        gameObject.transform.rotation = Quaternion.Euler(newRotation);
        if (activeTime > 0)
        {
            activeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.isTrigger)
            return;
       IDamage dmg = other.GetComponent<IDamage>();

       if (dmg != null && !other.CompareTag("Player"))
       {
           if (!isTicking)
           {
               Debug.Log("starting coroutine");
               StartCoroutine(burn(dmg));
           }
       }
    }

    IEnumerator burn(IDamage dmg)
    {
        isTicking = true;
        
        dmg.TakeDamage(damage);
        yield return new WaitForSeconds(_pulseInterval);

        isTicking = false;

    }
}
