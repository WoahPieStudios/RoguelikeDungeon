using System.Collections;
using System.Collections.Generic;
using Game.Parties;
using UnityEngine;

namespace Game.Interactions
{
    public abstract class Interactable : MonoBehaviour, IInteractable
    {
        public Vector3 position => transform.position;
        
        public abstract bool canInteract { get; }

        private static List<IInteractable> _Interactables = new List<IInteractable>();

        public static IInteractable[] interactables => _Interactables.ToArray();


        protected virtual void OnEnable()
        {
            _Interactables.Add(this);
        }

        protected virtual void OnDisable()
        {
            _Interactables.Remove(this);
        }

        public abstract void OnInteract(Party party);
    }
}