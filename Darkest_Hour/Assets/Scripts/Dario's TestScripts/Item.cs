using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : ScriptableObject
{
    [SerializeField] public string name;
    [SerializeField] public Sprite image;
    public GameObject player;
    public Player script;

    public virtual void Initialize()
    {
        player = GameObject.FindWithTag("Player");
        script = player.GetComponent<Player>();
    }

    public virtual void addStats()
    {}

    public virtual void Passive()
    {}
}
