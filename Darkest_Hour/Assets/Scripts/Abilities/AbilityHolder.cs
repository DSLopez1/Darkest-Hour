using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] private Ability _ability;
    private float _cooldown;
    private float _activeTime;
    private float _castTime;

    enum abilityState
    {
        ready,
        cast,
        casting,
        active,
        cooldown
    }

    public KeyCode key;

    private abilityState state = abilityState.ready;

    void Update()
    {

        switch (state)
        {
            case abilityState.ready:
                if (Input.GetKeyDown(key))
                {
                    _ability.Casting();
                    state = abilityState.casting;
                }
                break;
            case abilityState.casting:
                if (_castTime > 0)
                {
                    _castTime -= Time.deltaTime;
                }
                else
                {
                    state = abilityState.cast;
                    _castTime = _ability.castTime;
                }
                break;
            case abilityState.cast:

                _ability.Activate();
                state = abilityState.active;
                _activeTime = _ability.activeTime;
                break;
            case abilityState.active:
                if (_activeTime > 0)
                {
                    _activeTime -= Time.deltaTime;
                }
                else
                {
                    state = abilityState.cooldown;
                    _cooldown = _ability.cooldownTime;
                }
                break;
            case abilityState.cooldown:
                if (_cooldown > 0)
                {
                    _cooldown -= Time.deltaTime;
                    _ability.cooldownImage.fillAmount = _cooldown / _ability.cooldownTime;
                }
                else
                {
                    _ability.cooldownImage.fillAmount = 0;
                    state = abilityState.ready;
                }
                break;
        }
        
    }
}
