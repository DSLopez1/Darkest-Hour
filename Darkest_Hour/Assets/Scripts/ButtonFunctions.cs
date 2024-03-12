using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    
    private KeyCode[] keyCodes =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4
    };
    public void Resume()
    {
        GameManager.instance.StateUnpaused();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.instance.StateUnpaused();
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void BuyMeteor()
    {
        BuyAbility("Meteor");
    }

    public void BuyFireBall()
    {
        BuyAbility("FireBall");
    }

    public void BuyReposition()
    {
        BuyAbility("Reposition");
    }

    public void BuyFireBeam()
    {
        BuyAbility("FireBeam");
    }

    public void BuyDash()
    {
        BuyAbility("Dash");
    }

    public void BuyBlast()
    {
        BuyAbility("Blast");
    }

    public void BuyAbility(string name)
    {
        Ability tempAbility = null;

        
        foreach (var t in GameManager.instance.abilities)
        {
            Debug.Log(t.name);

            if (t.name == "Meteor")
            {
                tempAbility = t;
                break;
            }
        }

        if (tempAbility == null)
        {
            Debug.Log("No ability found");
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            if (GameManager.instance.playerScript.abilities[i].ability == null)
            {
                GameManager.instance.playerScript.abilities[i].ability = tempAbility;
                GameManager.instance.playerScript.abilities[i].key = keyCodes[i];
                break;
                //GameManager.instance.abilityImages[i].sprite = GameManager.instance.playerScript.abilities[i].ability.cooldownImage;
                //GameManager.instance.UpdateAbilityUI();
            }
        }
    }

    public void BuyFireBall()
    {
        Ability tempAbility = null;

        for (int i = 0; i < GameManager.instance.abilities.Count; i++)
        {
            if (GameManager.instance.abilities[i].name == "FireBall")
            {
                tempAbility = GameManager.instance.abilities[i];
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (GameManager.instance.playerScript.abilities[i].ability == null)
            {
                GameManager.instance.playerScript.abilities[i].ability = tempAbility;
                GameManager.instance.playerScript.abilities[i].key = keyCodes[i];
                break;
                //GameManager.instance.abilityImages[i].sprite = GameManager.instance.playerScript.abilities[i].ability.cooldownImage;
            }
        }
    }
}
