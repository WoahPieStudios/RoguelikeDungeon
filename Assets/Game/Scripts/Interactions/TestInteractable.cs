using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Interactions;
using Game.Parties;

public class TestInteractable : Interactable
{
    public override bool canInteract => true;

    public override void OnInteract(Party party)
    {
        Debug.Log("Interacted!");
    }
}
