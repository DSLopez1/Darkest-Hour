using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    public float cooldown;
    public float activeTime;

    enum abilityState
    {
        ready,
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
                    ability.Activate();
                    state = abilityState.active;
                    activeTime = ability._activeTime;
                }
                break;
            case abilityState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = abilityState.cooldown;
                    cooldown = ability._cooldownTime;
                }

                break;

            case abilityState.cooldown:
                if (cooldown > 0)
                {
                    cooldown -= Time.deltaTime;
                    ability.cooldownImage.fillAmount = cooldown / ability._cooldownTime;
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
