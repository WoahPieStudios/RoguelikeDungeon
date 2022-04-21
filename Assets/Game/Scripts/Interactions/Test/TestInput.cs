using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Parties;
using Game.Characters;
using Game.Interactions;

public class TestInput : MonoBehaviour
{
    [SerializeField]
    Hero _Hero;

    Party _Party;

    Interact _Interact;

    private void Awake() 
    {
        _Party = GetComponent<Party>();

        _Interact = GetComponent<Interact>();   

        _Party.AddHero(_Hero);
        _Party.SwitchCurrentHero(0); 
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.F))
            _Interact.Use();
    }
}
