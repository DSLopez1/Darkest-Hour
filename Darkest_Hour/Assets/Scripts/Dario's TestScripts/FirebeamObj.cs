using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebeamObj : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _pulseInterval;

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
    }

    void OnTriggerStay(Collider other)
    {
        if (other.isTrigger)
            return;

       IDamage dmg = other.GetComponent<IDamage>();

       if (dmg != null)
       {
           if (!isTicking)
           {
               StartCoroutine(burn(dmg));
           }
       }
    }

    IEnumerator burn(IDamage dmg)
    {
        isTicking = true;
        
        dmg.TakeDamage(_damage);
        yield return new WaitForSeconds(_pulseInterval);

        isTicking = false;

    }
}
