using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    //publics
    public float force;

    //components
    private Player _script;
    private Vector3 _velocity;
    private IPhysics phys;
    [SerializeField] private KeyCode _key;

    //bools
    private bool _isDashing;
    void Start()
    {
        phys = GetComponent<IPhysics>();
        _script = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_key) && !_isDashing)
        {
            AudioManager.instance.PlaySoundEffect(1);
            StartCoroutine(dash());
        }

    }

    IEnumerator dash()
    {
        _isDashing = true;

        _velocity = _script.getMoveVec().normalized * force;
        if (_velocity != Vector3.zero)
        {
            _velocity.y += 1;
            phys.PhysicsDir(_velocity);
        }
        else
        {
            _velocity = GameManager.instance.PlayerCam.transform.forward.normalized * force;
            _velocity.y += 1;
            phys.PhysicsDir(_velocity);
        }

        yield return new WaitForSeconds(_script.dashCooldown);
        _isDashing = false;
    }
}
