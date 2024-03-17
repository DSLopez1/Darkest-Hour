using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class armCall : MonoBehaviour
{
    [SerializeField] private Transform _shootPos;
    [SerializeField] private GameObject _fireBall;
    [SerializeField] private GameObject _auto;
    [SerializeField] private float _fireRate;
    public Animator _anim;

    private bool _isShooting;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Attack") && !_isShooting)
        {
            _anim.SetTrigger("Auto");
        }
        
    }

    public void CallShoot()
    {
        StartCoroutine(ShootAuto());
    }

    IEnumerator ShootAuto()
    {
        _isShooting = true;
        GameManager.instance.PlayerCam.StartCoroutine(GameManager.instance.PlayerCam.shootRay());

        GameObject instObj = Instantiate(_auto, _shootPos.position, Camera.main.transform.rotation);

        yield return new WaitForSeconds(_fireRate);
        _isShooting = false;
    }

    IEnumerator ShootFireBall()
    {
        _isShooting = true;

        GameObject instObj = Instantiate(_fireBall, _shootPos.position, Camera.main.transform.rotation);

        yield return new WaitForSeconds(.1f);
        _isShooting = false;
    }
}
