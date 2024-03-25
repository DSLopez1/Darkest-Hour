using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingBullet : MonoBehaviour
{
    [SerializeField] private GameObject _smallBullets;
    [SerializeField] private GameObject impactVFX;
    public float spreadAngle;
    public float delay;
    [SerializeField] int _damage;
    [SerializeField] float _speed;
    private bool _collided;

    private Player _playerScript;
    private armCall _arm;
    private Rigidbody _rb;

    void Start()
    {
        _playerScript = GameManager.instance.playerScript;
        _rb = GetComponent<Rigidbody>();
        _arm = GameManager.instance.player.GetComponent<armCall>();

        _rb.velocity = Camera.main.transform.forward * _speed;
        StartCoroutine(split());

    }

    IEnumerator split()
    {
        yield return new WaitForSeconds(delay);
        InstantiateSplitBullets();
        Destroy(gameObject);
    }

    private void InstantiateSplitBullets()
    {
        Quaternion leftRotation = Quaternion.Euler(0, -spreadAngle, 0);
        Quaternion rightRotation = Quaternion.Euler(0, spreadAngle, 0);

        Instantiate(_smallBullets, transform.position, transform.rotation * leftRotation);
        Instantiate(_smallBullets, transform.position, transform.rotation * rightRotation);

        Instantiate(_smallBullets, transform.position, transform.rotation);
    }

    void OnCollisionEnter(Collision co)
    {
        IDamage dmg = co.gameObject.GetComponent<IDamage>();

        if (co.gameObject.tag != "Player" && !_collided && co.gameObject.tag != "Bullet")
        {
            _collided = true;
            if (dmg != null)
            {
                dmg.TakeDamage(_damage);
            }
            GameObject impact = Instantiate(impactVFX, co.contacts[0].point, Quaternion.identity);

            Destroy(impact, 1);
            Destroy(gameObject);

        }
    }
}
