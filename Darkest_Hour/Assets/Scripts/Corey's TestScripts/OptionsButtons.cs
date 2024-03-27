using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsButtons : MonoBehaviour
{
    public EventSystem sys;
    [SerializeField] GameObject firstOpt;
    private GameObject origOpt;

    private void Awake()
    {
        sys = EventSystem.current;
        origOpt = sys.firstSelectedGameObject;
        sys.firstSelectedGameObject = firstOpt;
        sys.SetSelectedGameObject(firstOpt);
    }
    public void OpenOptions()
    {
        sys.firstSelectedGameObject = firstOpt;
    }
}
