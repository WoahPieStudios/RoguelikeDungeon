using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Parties;
using Game.Actions;

namespace Game.Interactions
{
    public interface IInteractable
    {
        Vector3 position { get; }

        bool canInteract { get; }
        
        void OnInteract(Party party);
    }
}