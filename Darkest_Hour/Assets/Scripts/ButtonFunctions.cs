using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public void Respawn()
    {
        GameManager.instance.player.GetComponent<Player>().respawn();
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

            if (t.name == name)
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
                GameManager.instance.abilityImages[i].GetComponent<Image>().sprite =
                    GameManager.instance.playerScript.abilities[i].ability.sprite;
                GameManager.instance.playerScript.abilities[i].ability.cooldownImage =
                    GameManager.instance.coolDownImages[i];
                GameManager.instance.UpdateAbilityUI();
                break;
            }
        }
    }
}
