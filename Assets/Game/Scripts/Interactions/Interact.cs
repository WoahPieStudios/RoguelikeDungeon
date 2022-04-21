using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Game.Actions;
using Game.Characters;
using Game.Interactions;
using Game.Parties;

namespace Game.Interactions
{
    public class Interact : Action, IInteractAction
    {
        [SerializeField]
        float _Range;

        Party _Party;

        IInteractable _ClosestInteractable;

        private void Awake() 
        {
            _Party = GetComponent<Party>();    
        }

        void Update() 
        {
            if(_Party == null || _Party.currentHero == null && _ClosestInteractable != null)
            {
                _ClosestInteractable = null;

                return;
            }

            _ClosestInteractable = Interactable.interactables.Where(i => i.canInteract).OrderBy(i => (_Party.currentHero.transform.position - i.position).magnitude).FirstOrDefault();
        }

        protected override void OnUse()
        {
            base.OnUse();

            if(_ClosestInteractable != null)
                _ClosestInteractable.OnInteract(_Party);

            End();
        }
    }
}