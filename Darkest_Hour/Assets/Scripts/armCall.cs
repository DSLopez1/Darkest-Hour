using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class armCall : MonoBehaviour
{
    [SerializeField] private Transform _shootPos;
    [SerializeField] private GameObject _fireBall;
    [SerializeField] public GameObject auto;
    [SerializeField] public float fireRate;
    public Animator _anim;

    private bool _isShooting;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButton("Attack") && !_isShooting)
        {
            AudioManager.instance.PlaySoundEffect(3);
            _isShooting = true;
            _anim.SetTrigger("Auto");
        }
        
    }

    public void CallShoot()
    {
        StartCoroutine(ShootAuto());
    }

    IEnumerator ShootAuto()
    {
        GameManager.instance.PlayerCam.StartCoroutine(GameManager.instance.PlayerCam.shootRay());

        GameObject instObj = Instantiate(auto, _shootPos.position, Camera.main.transform.rotation);

        yield return new WaitForSeconds(fireRate);
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
