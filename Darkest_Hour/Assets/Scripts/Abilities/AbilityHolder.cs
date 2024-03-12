using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
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
                    ability.Casting();
                    state = abilityState.casting;
                    _castTime = ability.castTime;
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
                }
                break;
            case abilityState.cast:

                ability.Activate();
                state = abilityState.active;
                _activeTime = ability.activeTime;
                break;
            case abilityState.active:
                if (_activeTime > 0)
                {
                    _activeTime -= Time.deltaTime;
                }
                else
                {
                    state = abilityState.cooldown;
                    _cooldown = ability.cooldownTime;
                }
                break;
            case abilityState.cooldown:
                if (_cooldown > 0)
                {
                    _cooldown -= Time.deltaTime;
                    ability.cooldownImage.fillAmount = _cooldown / ability.cooldownTime;
                }
                else
                {
                    ability.cooldownImage.fillAmount = 0;
                    state = abilityState.ready;
                }
                break;
        }
    }
}
