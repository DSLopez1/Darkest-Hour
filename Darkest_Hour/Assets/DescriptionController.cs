using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DescriptionController : MonoBehaviour
{
    public static DescriptionController instance;
    [SerializeField] private float _activeTime;
    [SerializeField] public GameObject OBowDesc;
    [SerializeField] public GameObject ShadowBoots;
    [SerializeField] public GameObject BansheesVeil;

    Dictionary<string, GameObject> UIElements = new Dictionary<string, GameObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        UIElements.Add("OBow", OBowDesc);
        UIElements.Add("ShadowBoots", ShadowBoots);
        UIElements.Add("BansheesVeil", BansheesVeil);
    }

    public IEnumerator callDesc(string itemName)
    {
        UIElements[itemName].SetActive(true);

        yield return new WaitForSeconds(_activeTime);
        UIElements[itemName].SetActive(false);
    }
}
