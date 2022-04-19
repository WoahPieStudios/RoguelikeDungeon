using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interactions
{
    public interface IInteractable
    {
        Vector3 position { get; }
        void OnInteract();
    }
}