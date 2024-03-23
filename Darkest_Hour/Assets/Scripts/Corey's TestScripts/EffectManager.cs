using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    [Header("-----Player------")]

    [SerializeField] public GameObject player;
    [SerializeField] public Player playerScript;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Slow(float amount, int duration)
    {
        // Capture original speed
        float org = playerScript.playerSpeed;
        // Set speed to slow speed
        playerScript.playerSpeed =  amount;
        // Wait time
        yield return new WaitForSeconds(duration);
        // Change speed back
        playerScript.playerSpeed = org;
    }

    public IEnumerator Burn(int damage, float duration, float tickRate)
    {
        // Continue running until 0
        while (duration > 0)
        {
            // Deal damage
            playerScript.TakeDamage(damage);
            // Wait for tick
            yield return new WaitForSeconds(tickRate);
            // Subtract tick to duration
            duration -= tickRate;
        } 
    }
}
